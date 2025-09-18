using Database.Data;
using Database.DTOS;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services;

public class BitacoraPesoService : IBitacoraPesoService
{
    private readonly DatabseContext _context;

    public BitacoraPesoService(DatabseContext context)
    {
        _context = context;
    }

    public async Task<List<BitacoraPeso>> GetAllAsync()
    {
        return await _context.BitacoraPesos
            .Include(bp => bp.Animal)
                .ThenInclude(a => a.Raza)
            .Include(bp => bp.Animal)
                .ThenInclude(a => a.EstadoAnimal)
            .OrderByDescending(bp => bp.FechaRegistro)
            .ToListAsync();
    }

    public async Task<BitacoraPeso?> GetByIdAsync(int id)
    {
        return await _context.BitacoraPesos
            .Include(bp => bp.Animal)
                .ThenInclude(a => a.Raza)
            .Include(bp => bp.Animal)
                .ThenInclude(a => a.EstadoAnimal)
            .FirstOrDefaultAsync(bp => bp.Id == id);
    }

    public async Task<List<BitacoraPeso>> GetByAnimalIdAsync(int animalId)
    {
        return await _context.BitacoraPesos
            .Where(bp => bp.AnimalId == animalId)
            .OrderByDescending(bp => bp.FechaRegistro)
            .ToListAsync();
    }

    public async Task<BitacoraPeso> CreateAsync(BitacoraPesoCreateDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var bitacoraPeso = new BitacoraPeso
            {
                AnimalId = dto.AnimalId,
                Peso = dto.Peso,
                Alto = dto.Alto,
                Largo = dto.Largo,
                Diametro = dto.Diametro,
                FechaRegistro = DateTime.Now
            };

            _context.BitacoraPesos.Add(bitacoraPeso);
            await _context.SaveChangesAsync();

            var animal = await _context.Animales.FindAsync(dto.AnimalId);
            if (animal != null)
            {
                animal.PesoActualLibras = dto.Peso;
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return await GetByIdAsync(bitacoraPeso.Id) ?? bitacoraPeso;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}