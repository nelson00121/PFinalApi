using Database.DTOS;
using Database.Models;

namespace BackOffice.Services;

public interface IBitacoraPesoService
{
    Task<List<BitacoraPeso>> GetAllAsync();
    Task<BitacoraPeso?> GetByIdAsync(int id);
    Task<List<BitacoraPeso>> GetByAnimalIdAsync(int animalId);
    Task<BitacoraPeso> CreateAsync(BitacoraPesoCreateDto dto);
}