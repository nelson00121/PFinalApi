using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public class BitacoraVacunaService : IBitacoraVacunaService
    {
        private readonly DatabseContext _context;

        public BitacoraVacunaService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<BitacorasVacuna> bitacoras, int totalCount)> GetBitacorasVacunaPaginadasAsync(int page, int pageSize, string searchTerm = "")
        {
            var query = _context.BitacorasVacunas
                .Include(b => b.Animal)
                    .ThenInclude(a => a.Raza)
                .Include(b => b.Vacuna)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => 
                    (b.Animal.CodigoRfid != null && b.Animal.CodigoRfid.Contains(searchTerm)) ||
                    (b.Animal.Raza.Nombre != null && b.Animal.Raza.Nombre.Contains(searchTerm)) ||
                    (b.Vacuna.Nombre != null && b.Vacuna.Nombre.Contains(searchTerm))
                );
            }

            var totalCount = await query.CountAsync();
            
            var bitacoras = await query
                .OrderByDescending(b => b.FechaRegistro)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (bitacoras, totalCount);
        }

        public async Task<BitacorasVacuna?> GetBitacoraVacunaByIdAsync(int id)
        {
            return await _context.BitacorasVacunas
                .Include(b => b.Animal)
                    .ThenInclude(a => a.Raza)
                .Include(b => b.Vacuna)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BitacorasVacuna> CreateBitacoraVacunaAsync(BitacorasVacunaCreateDto bitacoraDto)
        {
            var bitacora = new BitacorasVacuna
            {
                AnimalId = bitacoraDto.AnimalId,
                VacunaId = bitacoraDto.VacunaId,
                FechaRegistro = DateTime.Now
            };

            _context.BitacorasVacunas.Add(bitacora);
            await _context.SaveChangesAsync();

            return await GetBitacoraVacunaByIdAsync(bitacora.Id) ?? bitacora;
        }

        public async Task<BitacorasVacuna?> UpdateBitacoraVacunaAsync(int id, BitacorasVacunaCreateDto bitacoraDto)
        {
            var existingBitacora = await _context.BitacorasVacunas.FindAsync(id);
            if (existingBitacora == null) return null;

            existingBitacora.AnimalId = bitacoraDto.AnimalId;
            existingBitacora.VacunaId = bitacoraDto.VacunaId;

            await _context.SaveChangesAsync();
            return await GetBitacoraVacunaByIdAsync(id);
        }

        public async Task<bool> DeleteBitacoraVacunaAsync(int id)
        {
            var bitacora = await _context.BitacorasVacunas.FindAsync(id);
            if (bitacora == null) return false;

            _context.BitacorasVacunas.Remove(bitacora);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Animale>> GetAnimalesAsync()
        {
            return await _context.Animales
                .Include(a => a.Raza)
                .OrderBy(a => a.CodigoRfid)
                .ToListAsync();
        }

        public async Task<List<Vacuna>> GetVacunasAsync()
        {
            return await _context.Vacunas
                .OrderBy(v => v.Nombre)
                .ToListAsync();
        }

        public async Task<bool> VacunaYaAplicadaAsync(int animalId, int vacunaId, int? excludeId = null)
        {
            var query = _context.BitacorasVacunas
                .Where(b => b.AnimalId == animalId && b.VacunaId == vacunaId);
            
            if (excludeId.HasValue)
            {
                query = query.Where(b => b.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}