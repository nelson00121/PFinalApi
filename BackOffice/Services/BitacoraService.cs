using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services;

public class BitacoraService : IBitacoraService
{
    private readonly DatabseContext _context;

    public BitacoraService(DatabseContext context)
    {
        _context = context;
    }

    public async Task<List<BitacoraPeso>> GetBitacoraPesosByAnimalIdAsync(int animalId)
    {
        return await _context.BitacoraPesos
            .Where(bp => bp.AnimalId == animalId)
            .OrderBy(bp => bp.FechaRegistro)
            .ToListAsync();
    }

    public async Task<List<BitacorasVacuna>> GetBitacorasVacunasByAnimalIdAsync(int animalId)
    {
        return await _context.BitacorasVacunas
            .Include(bv => bv.Vacuna)
            .Where(bv => bv.AnimalId == animalId)
            .OrderByDescending(bv => bv.FechaRegistro)
            .ToListAsync();
    }
}