using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public class CompraAlimentoService : ICompraAlimentoService
    {
        private readonly DatabseContext _context;

        public CompraAlimentoService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<List<CompraAlimento>> GetAllAsync()
        {
            return await _context.CompraAlimentos
                .Include(c => c.Alimento)
                    .ThenInclude(a => a.TipoAlimento)
                .Include(c => c.Alimento)
                    .ThenInclude(a => a.UnidadesDeMedidaAlimentos)
                .OrderByDescending(c => c.FechaRegistro)
                .ToListAsync();
        }

        public async Task<CompraAlimento?> GetByIdAsync(int id)
        {
            return await _context.CompraAlimentos
                .Include(c => c.Alimento)
                    .ThenInclude(a => a.TipoAlimento)
                .Include(c => c.Alimento)
                    .ThenInclude(a => a.UnidadesDeMedidaAlimentos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CompraAlimento> CreateAsync(CompraAlimentoCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var compra = new CompraAlimento
                {
                    AlimentoId = dto.AlimentoId,
                    CantidadComprada = dto.CantidadComprada,
                    FechaRegistro = DateTime.Now
                };

                _context.CompraAlimentos.Add(compra);
                await _context.SaveChangesAsync();

                var alimento = await _context.Alimentos.FindAsync(dto.AlimentoId);
                if (alimento != null && dto.CantidadComprada.HasValue)
                {
                    alimento.Stock += dto.CantidadComprada.Value;

                    var bitacora = new BitacoraAlimento
                    {
                        AlimentoId = alimento.Id,
                        Stock = alimento.Stock,
                        FechaRegistro = DateTime.Now
                    };

                    _context.BitacoraAlimentos.Add(bitacora);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                
                return await GetByIdAsync(compra.Id) ?? compra;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<CompraAlimento> UpdateAsync(int id, CompraAlimentoCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var existingCompra = await _context.CompraAlimentos.FindAsync(id);
                if (existingCompra == null)
                    throw new ArgumentException($"No se encontró la compra con ID {id}");

                var alimento = await _context.Alimentos.FindAsync(existingCompra.AlimentoId);
                if (alimento != null && existingCompra.CantidadComprada.HasValue)
                {
                    alimento.Stock -= existingCompra.CantidadComprada.Value;
                }

                existingCompra.AlimentoId = dto.AlimentoId;
                existingCompra.CantidadComprada = dto.CantidadComprada;

                if (dto.AlimentoId != existingCompra.AlimentoId)
                {
                    alimento = await _context.Alimentos.FindAsync(dto.AlimentoId);
                }

                if (alimento != null && dto.CantidadComprada.HasValue)
                {
                    alimento.Stock += dto.CantidadComprada.Value;

                    var bitacora = new BitacoraAlimento
                    {
                        AlimentoId = alimento.Id,
                        Stock = alimento.Stock,
                        FechaRegistro = DateTime.Now
                    };

                    _context.BitacoraAlimentos.Add(bitacora);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetByIdAsync(id) ?? existingCompra;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var compra = await _context.CompraAlimentos.FindAsync(id);
                if (compra == null) return false;

                var alimento = await _context.Alimentos.FindAsync(compra.AlimentoId);
                if (alimento != null && compra.CantidadComprada.HasValue)
                {
                    alimento.Stock -= compra.CantidadComprada.Value;

                    var bitacora = new BitacoraAlimento
                    {
                        AlimentoId = alimento.Id,
                        Stock = alimento.Stock,
                        FechaRegistro = DateTime.Now
                    };

                    _context.BitacoraAlimentos.Add(bitacora);
                }

                _context.CompraAlimentos.Remove(compra);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}