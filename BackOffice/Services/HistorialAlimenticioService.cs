using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public class HistorialAlimenticioService : IHistorialAlimenticioService
    {
        private readonly DatabseContext _context;

        public HistorialAlimenticioService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<List<HistorialAlimenticio>> GetAllAsync()
        {
            return await _context.HistorialAlimenticios
                .Include(h => h.Animal)
                .Include(h => h.Alimento)
                    .ThenInclude(a => a.TipoAlimento)
                .Include(h => h.Alimento)
                    .ThenInclude(a => a.UnidadesDeMedidaAlimentos)
                .Include(h => h.Empleado)
                .OrderByDescending(h => h.FechaRegistro)
                .ToListAsync();
        }

        public async Task<HistorialAlimenticio?> GetByIdAsync(int id)
        {
            return await _context.HistorialAlimenticios
                .Include(h => h.Animal)
                .Include(h => h.Alimento)
                    .ThenInclude(a => a.TipoAlimento)
                .Include(h => h.Alimento)
                    .ThenInclude(a => a.UnidadesDeMedidaAlimentos)
                .Include(h => h.Empleado)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<HistorialAlimenticio> CreateAsync(HistorialAlimenticioCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var alimento = await _context.Alimentos.FindAsync(dto.AlimentoId);
                if (alimento == null)
                    throw new ArgumentException($"No se encontró el alimento con ID {dto.AlimentoId}");

                if (alimento.Stock < dto.Cantidad)
                    throw new InvalidOperationException($"Stock insuficiente. Disponible: {alimento.Stock:F2}, Solicitado: {dto.Cantidad:F2}");

                var historial = new HistorialAlimenticio
                {
                    AnimalId = dto.AnimalId,
                    AlimentoId = dto.AlimentoId,
                    EmpleadoId = dto.EmpleadoId,
                    Cantidad = dto.Cantidad,
                    FechaRegistro = DateTime.Now
                };

                _context.HistorialAlimenticios.Add(historial);
                await _context.SaveChangesAsync();

                alimento.Stock -= dto.Cantidad;

                var bitacora = new BitacoraAlimento
                {
                    AlimentoId = alimento.Id,
                    Stock = alimento.Stock,
                    FechaRegistro = DateTime.Now
                };

                _context.BitacoraAlimentos.Add(bitacora);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                
                return await GetByIdAsync(historial.Id) ?? historial;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<HistorialAlimenticio> UpdateAsync(int id, HistorialAlimenticioCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var existingHistorial = await _context.HistorialAlimenticios.FindAsync(id);
                if (existingHistorial == null)
                    throw new ArgumentException($"No se encontró el historial con ID {id}");

                var alimentoAnterior = await _context.Alimentos.FindAsync(existingHistorial.AlimentoId);
                if (alimentoAnterior != null)
                {
                    alimentoAnterior.Stock += existingHistorial.Cantidad;
                }

                var alimento = await _context.Alimentos.FindAsync(dto.AlimentoId);
                if (alimento == null)
                    throw new ArgumentException($"No se encontró el alimento con ID {dto.AlimentoId}");

                if (alimento.Stock < dto.Cantidad)
                    throw new InvalidOperationException($"Stock insuficiente. Disponible: {alimento.Stock:F2}, Solicitado: {dto.Cantidad:F2}");

                existingHistorial.AnimalId = dto.AnimalId;
                existingHistorial.AlimentoId = dto.AlimentoId;
                existingHistorial.EmpleadoId = dto.EmpleadoId;
                existingHistorial.Cantidad = dto.Cantidad;

                alimento.Stock -= dto.Cantidad;

                var bitacora = new BitacoraAlimento
                {
                    AlimentoId = alimento.Id,
                    Stock = alimento.Stock,
                    FechaRegistro = DateTime.Now
                };

                _context.BitacoraAlimentos.Add(bitacora);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetByIdAsync(id) ?? existingHistorial;
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
                var historial = await _context.HistorialAlimenticios.FindAsync(id);
                if (historial == null) return false;

                var alimento = await _context.Alimentos.FindAsync(historial.AlimentoId);
                if (alimento != null)
                {
                    alimento.Stock += historial.Cantidad;

                    var bitacora = new BitacoraAlimento
                    {
                        AlimentoId = alimento.Id,
                        Stock = alimento.Stock,
                        FechaRegistro = DateTime.Now
                    };

                    _context.BitacoraAlimentos.Add(bitacora);
                }

                _context.HistorialAlimenticios.Remove(historial);
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

        public async Task<List<Animale>> GetAnimalesAsync()
        {
            return await _context.Animales
                .Include(a => a.Raza)
                .Include(a => a.EstadoAnimal)
                .OrderBy(a => a.Id)
                .ToListAsync();
        }

        public async Task<List<Alimento>> GetAlimentosAsync()
        {
            return await _context.Alimentos
                .Include(a => a.TipoAlimento)
                .Include(a => a.UnidadesDeMedidaAlimentos)
                .Where(a => a.Stock > 0)
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await _context.Empleados
                .OrderBy(e => e.Id)
                .ToListAsync();
        }

        public async Task<List<BitacoraAlimento>> GetBitacoraAlimentoByIdAsync(int alimentoId)
        {
            return await _context.BitacoraAlimentos
                .Include(b => b.Alimento)
                .Where(b => b.AlimentoId == alimentoId)
                .OrderBy(b => b.FechaRegistro)
                .ToListAsync();
        }
    }
}