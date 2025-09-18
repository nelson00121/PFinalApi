using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public class VacunaService : IVacunaService
    {
        private readonly DatabseContext _context;

        public VacunaService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<Vacuna> vacunas, int totalCount)> GetVacunasPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.Vacunas.CountAsync();
            
            var vacunas = await _context.Vacunas
                .OrderBy(v => v.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (vacunas, totalCount);
        }

        public async Task<Vacuna?> GetVacunaByIdAsync(int id)
        {
            return await _context.Vacunas.FindAsync(id);
        }

        public async Task<Vacuna> CreateVacunaAsync(VacunaCreateDto vacunaDto)
        {
            var vacuna = new Vacuna
            {
                Nombre = vacunaDto.Nombre,
                Descripcion = vacunaDto.Descripcion
            };

            _context.Vacunas.Add(vacuna);
            await _context.SaveChangesAsync();

            return vacuna;
        }

        public async Task<Vacuna?> UpdateVacunaAsync(int id, VacunaCreateDto vacunaDto)
        {
            var existingVacuna = await _context.Vacunas.FindAsync(id);
            if (existingVacuna == null) return null;

            existingVacuna.Nombre = vacunaDto.Nombre;
            existingVacuna.Descripcion = vacunaDto.Descripcion;

            await _context.SaveChangesAsync();
            return existingVacuna;
        }

        public async Task<bool> DeleteVacunaAsync(int id)
        {
            var vacuna = await _context.Vacunas
                .Include(v => v.BitacorasVacunas)
                .FirstOrDefaultAsync(v => v.Id == id);
            
            if (vacuna == null) return false;

            if (vacuna.BitacorasVacunas.Any())
            {
                throw new InvalidOperationException("No se puede eliminar la vacuna porque tiene bitácoras asociadas");
            }

            _context.Vacunas.Remove(vacuna);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excludeId = null)
        {
            var query = _context.Vacunas.Where(v => v.Nombre == nombre);
            
            if (excludeId.HasValue)
            {
                query = query.Where(v => v.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}