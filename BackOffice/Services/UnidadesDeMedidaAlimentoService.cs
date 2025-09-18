using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public class UnidadesDeMedidaAlimentoService : IUnidadesDeMedidaAlimentoService
    {
        private readonly DatabseContext _context;

        public UnidadesDeMedidaAlimentoService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<UnidadesDeMedidaAlimento> unidades, int totalCount)> GetUnidadesDeMedidaAlimentoPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.UnidadesDeMedidaAlimentos.CountAsync();
            
            var unidades = await _context.UnidadesDeMedidaAlimentos
                .OrderBy(u => u.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (unidades, totalCount);
        }

        public async Task<UnidadesDeMedidaAlimento?> GetUnidadesDeMedidaAlimentoByIdAsync(int id)
        {
            return await _context.UnidadesDeMedidaAlimentos.FindAsync(id);
        }

        public async Task<UnidadesDeMedidaAlimento> CreateUnidadesDeMedidaAlimentoAsync(UnidadesDeMedidaAlimentoCreateDto unidadDto)
        {
            var unidad = new UnidadesDeMedidaAlimento
            {
                Nombre = unidadDto.Nombre
            };

            _context.UnidadesDeMedidaAlimentos.Add(unidad);
            await _context.SaveChangesAsync();

            return unidad;
        }

        public async Task<UnidadesDeMedidaAlimento?> UpdateUnidadesDeMedidaAlimentoAsync(int id, UnidadesDeMedidaAlimentoCreateDto unidadDto)
        {
            var existingUnidad = await _context.UnidadesDeMedidaAlimentos.FindAsync(id);
            if (existingUnidad == null) return null;

            existingUnidad.Nombre = unidadDto.Nombre;

            await _context.SaveChangesAsync();
            return existingUnidad;
        }

        public async Task<bool> DeleteUnidadesDeMedidaAlimentoAsync(int id)
        {
            var unidad = await _context.UnidadesDeMedidaAlimentos
                .Include(u => u.Alimentos)
                .FirstOrDefaultAsync(u => u.Id == id);
            
            if (unidad == null) return false;

            if (unidad.Alimentos.Any())
            {
                throw new InvalidOperationException("No se puede eliminar la unidad de medida porque tiene alimentos asociados");
            }

            _context.UnidadesDeMedidaAlimentos.Remove(unidad);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excludeId = null)
        {
            var query = _context.UnidadesDeMedidaAlimentos.Where(u => u.Nombre == nombre);
            
            if (excludeId.HasValue)
            {
                query = query.Where(u => u.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}