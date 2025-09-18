using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public interface IAnimalService
    {
        Task<(List<Animale> animales, int totalCount)> GetAnimalesPaginadosAsync(int page, int pageSize, string? searchTerm = null);
        Task<Animale?> GetAnimalByIdAsync(int id);
        Task<Animale> CreateAnimalAsync(Animale animal);
        Task<Animale?> UpdateAnimalAsync(int id, Animale animal);
        Task<bool> DeleteAnimalAsync(int id);
        Task<List<Raza>> GetRazasAsync();
        Task<List<EstadosAnimale>> GetEstadosAsync();
        Task<bool> CodigoRfidExistsAsync(string codigoRfid, int? excludeId = null);
    }

    public class AnimalService : IAnimalService
    {
        private readonly DatabseContext _context;

        public AnimalService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<Animale> animales, int totalCount)> GetAnimalesPaginadosAsync(int page, int pageSize, string? searchTerm = null)
        {
            var query = _context.Animales
                .Include(a => a.Raza)
                .Include(a => a.EstadoAnimal)
                .AsQueryable();

            // Aplicar filtro de búsqueda si se proporciona
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a => 
                    a.CodigoRfid.Contains(searchTerm) ||
                    a.Raza!.Nombre.Contains(searchTerm) ||
                    a.EstadoAnimal!.Nombre.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            
            var animales = await query
                .OrderByDescending(a => a.FechaRegistro)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (animales, totalCount);
        }

        public async Task<Animale?> GetAnimalByIdAsync(int id)
        {
            return await _context.Animales
                .Include(a => a.Raza)
                .Include(a => a.EstadoAnimal)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Animale> CreateAnimalAsync(Animale animal)
        {
            animal.FechaRegistro = DateTime.Now;
            _context.Animales.Add(animal);
            await _context.SaveChangesAsync();
            return await GetAnimalByIdAsync(animal.Id) ?? animal;
        }

        public async Task<Animale?> UpdateAnimalAsync(int id, Animale animal)
        {
            var existingAnimal = await _context.Animales.FindAsync(id);
            if (existingAnimal == null) return null;

            existingAnimal.CodigoRfid = animal.CodigoRfid;
            existingAnimal.PesoActualLibras = animal.PesoActualLibras;
            existingAnimal.EstadoAnimalId = animal.EstadoAnimalId;
            existingAnimal.RazaId = animal.RazaId;

            await _context.SaveChangesAsync();
            return await GetAnimalByIdAsync(id);
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var animal = await _context.Animales.FindAsync(id);
            if (animal == null) return false;

            _context.Animales.Remove(animal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Raza>> GetRazasAsync()
        {
            return await _context.Razas.OrderBy(r => r.Nombre).ToListAsync();
        }

        public async Task<List<EstadosAnimale>> GetEstadosAsync()
        {
            return await _context.EstadosAnimales.OrderBy(e => e.Nombre).ToListAsync();
        }

        public async Task<bool> CodigoRfidExistsAsync(string codigoRfid, int? excludeId = null)
        {
            var query = _context.Animales.Where(a => a.CodigoRfid == codigoRfid);
            if (excludeId.HasValue)
                query = query.Where(a => a.Id != excludeId.Value);
            
            return await query.AnyAsync();
        }
    }
}