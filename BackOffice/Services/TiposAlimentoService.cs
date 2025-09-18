using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public class TiposAlimentoService : ITiposAlimentoService
    {
        private readonly DatabseContext _context;

        public TiposAlimentoService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<TiposAlimento> tiposAlimento, int totalCount)> GetTiposAlimentoPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.TiposAlimentos.CountAsync();
            
            var tiposAlimento = await _context.TiposAlimentos
                .OrderBy(t => t.NombreTipo)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (tiposAlimento, totalCount);
        }

        public async Task<TiposAlimento?> GetTiposAlimentoByIdAsync(int id)
        {
            return await _context.TiposAlimentos.FindAsync(id);
        }

        public async Task<TiposAlimento> CreateTiposAlimentoAsync(TiposAlimentoCreateDto tiposAlimentoDto)
        {
            var tiposAlimento = new TiposAlimento
            {
                NombreTipo = tiposAlimentoDto.NombreTipo,
                Descripcion = tiposAlimentoDto.Descripcion
            };

            _context.TiposAlimentos.Add(tiposAlimento);
            await _context.SaveChangesAsync();

            return tiposAlimento;
        }

        public async Task<TiposAlimento?> UpdateTiposAlimentoAsync(int id, TiposAlimentoCreateDto tiposAlimentoDto)
        {
            var existingTiposAlimento = await _context.TiposAlimentos.FindAsync(id);
            if (existingTiposAlimento == null) return null;

            existingTiposAlimento.NombreTipo = tiposAlimentoDto.NombreTipo;
            existingTiposAlimento.Descripcion = tiposAlimentoDto.Descripcion;

            await _context.SaveChangesAsync();
            return existingTiposAlimento;
        }

        public async Task<bool> DeleteTiposAlimentoAsync(int id)
        {
            var tiposAlimento = await _context.TiposAlimentos
                .Include(t => t.Alimentos)
                .FirstOrDefaultAsync(t => t.Id == id);
            
            if (tiposAlimento == null) return false;

            if (tiposAlimento.Alimentos.Any())
            {
                throw new InvalidOperationException("No se puede eliminar el tipo de alimento porque tiene alimentos asociados");
            }

            _context.TiposAlimentos.Remove(tiposAlimento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excludeId = null)
        {
            var query = _context.TiposAlimentos.Where(t => t.NombreTipo == nombre);
            
            if (excludeId.HasValue)
            {
                query = query.Where(t => t.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}