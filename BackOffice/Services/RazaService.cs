using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public class RazaService : IRazaService
    {
        private readonly DatabseContext _context;

        public RazaService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<Raza> razas, int totalCount)> GetRazasPaginadasAsync(int page, int pageSize)
        {
            var totalCount = await _context.Razas.CountAsync();
            
            var razas = await _context.Razas
                .OrderBy(r => r.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (razas, totalCount);
        }

        public async Task<Raza?> GetRazaByIdAsync(int id)
        {
            return await _context.Razas.FindAsync(id);
        }

        public async Task<Raza> CreateRazaAsync(Raza raza)
        {
            _context.Razas.Add(raza);
            await _context.SaveChangesAsync();
            return raza;
        }

        public async Task<Raza?> UpdateRazaAsync(int id, Raza raza)
        {
            var existingRaza = await _context.Razas.FindAsync(id);
            if (existingRaza == null) return null;

            existingRaza.Nombre = raza.Nombre;

            await _context.SaveChangesAsync();
            return existingRaza;
        }

        public async Task<bool> DeleteRazaAsync(int id)
        {
            var raza = await _context.Razas.FindAsync(id);
            if (raza == null) return false;

            var hasAnimales = await _context.Animales.AnyAsync(a => a.RazaId == id);
            if (hasAnimales) return false;

            _context.Razas.Remove(raza);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excludeId = null)
        {
            var query = _context.Razas.Where(r => r.Nombre.ToLower() == nombre.ToLower());
            if (excludeId.HasValue)
                query = query.Where(r => r.Id != excludeId.Value);
            
            return await query.AnyAsync();
        }
    }
}