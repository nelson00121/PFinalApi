using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public class AlimentoService : IAlimentoService
    {
        private readonly DatabseContext _context;

        public AlimentoService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<Alimento> alimentos, int totalCount)> GetAlimentosPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.Alimentos.CountAsync();
            
            var alimentos = await _context.Alimentos
                .Include(a => a.TipoAlimento)
                .Include(a => a.UnidadesDeMedidaAlimentos)
                .OrderByDescending(a => a.FechaRegistro)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (alimentos, totalCount);
        }

        public async Task<Alimento?> GetAlimentoByIdAsync(int id)
        {
            return await _context.Alimentos
                .Include(a => a.TipoAlimento)
                .Include(a => a.UnidadesDeMedidaAlimentos)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Alimento> CreateAlimentoAsync(AlimentoCreateDto alimentoDto)
        {
            var alimento = new Alimento
            {
                Nombre = alimentoDto.Nombre,
                TipoAlimentoId = alimentoDto.TipoAlimentoId,
                UnidadesDeMedidaAlimentosId = alimentoDto.UnidadesDeMedidaAlimentosId,
                Stock = alimentoDto.Stock,
                FechaRegistro = DateTime.Now
            };

            _context.Alimentos.Add(alimento);
            await _context.SaveChangesAsync();

            return await GetAlimentoByIdAsync(alimento.Id) ?? alimento;
        }

        public async Task<Alimento?> UpdateAlimentoAsync(int id, AlimentoCreateDto alimentoDto)
        {
            var existingAlimento = await _context.Alimentos.FindAsync(id);
            if (existingAlimento == null) return null;

            existingAlimento.Nombre = alimentoDto.Nombre;
            existingAlimento.TipoAlimentoId = alimentoDto.TipoAlimentoId;
            existingAlimento.UnidadesDeMedidaAlimentosId = alimentoDto.UnidadesDeMedidaAlimentosId;
            existingAlimento.Stock = alimentoDto.Stock;

            await _context.SaveChangesAsync();
            return await GetAlimentoByIdAsync(id);
        }

        public async Task<bool> DeleteAlimentoAsync(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);
            if (alimento == null) return false;

            _context.Alimentos.Remove(alimento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TiposAlimento>> GetTiposAlimentoAsync()
        {
            return await _context.TiposAlimentos
                .OrderBy(t => t.NombreTipo)
                .ToListAsync();
        }

        public async Task<List<UnidadesDeMedidaAlimento>> GetUnidadesDeMedidaAsync()
        {
            return await _context.UnidadesDeMedidaAlimentos
                .OrderBy(u => u.Nombre)
                .ToListAsync();
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excludeId = null)
        {
            var query = _context.Alimentos.Where(a => a.Nombre == nombre);
            
            if (excludeId.HasValue)
            {
                query = query.Where(a => a.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}