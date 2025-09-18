using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public interface IEstadoAnimalService
    {
        Task<(List<EstadosAnimale> estados, int totalCount)> GetEstadosPaginadosAsync(int page, int pageSize);
        Task<EstadosAnimale?> GetEstadoByIdAsync(int id);
        Task<EstadosAnimale> CreateEstadoAsync(EstadosAnimale estado);
        Task<EstadosAnimale?> UpdateEstadoAsync(int id, EstadosAnimale estado);
        Task<bool> DeleteEstadoAsync(int id);
        Task<bool> NombreExistsAsync(string nombre, int? excludeId = null);
    }

    public class EstadoAnimalService : IEstadoAnimalService
    {
        private readonly DatabseContext _context;

        public EstadoAnimalService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<EstadosAnimale> estados, int totalCount)> GetEstadosPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.EstadosAnimales.CountAsync();
            
            var estados = await _context.EstadosAnimales
                .OrderBy(e => e.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (estados, totalCount);
        }

        public async Task<EstadosAnimale?> GetEstadoByIdAsync(int id)
        {
            return await _context.EstadosAnimales
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EstadosAnimale> CreateEstadoAsync(EstadosAnimale estado)
        {
            _context.EstadosAnimales.Add(estado);
            await _context.SaveChangesAsync();
            return estado;
        }

        public async Task<EstadosAnimale?> UpdateEstadoAsync(int id, EstadosAnimale estado)
        {
            var existingEstado = await _context.EstadosAnimales.FindAsync(id);
            if (existingEstado == null) return null;

            existingEstado.Nombre = estado.Nombre;

            await _context.SaveChangesAsync();
            return existingEstado;
        }

        public async Task<bool> DeleteEstadoAsync(int id)
        {
            var estado = await _context.EstadosAnimales.FindAsync(id);
            if (estado == null) return false;

            _context.EstadosAnimales.Remove(estado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excludeId = null)
        {
            var query = _context.EstadosAnimales.Where(e => e.Nombre == nombre);
            if (excludeId.HasValue)
                query = query.Where(e => e.Id != excludeId.Value);
            
            return await query.AnyAsync();
        }
    }
}