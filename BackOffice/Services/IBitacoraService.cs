using Database.Models;

namespace BackOffice.Services;

public interface IBitacoraService
{
    Task<List<BitacoraPeso>> GetBitacoraPesosByAnimalIdAsync(int animalId);
    Task<List<BitacorasVacuna>> GetBitacorasVacunasByAnimalIdAsync(int animalId);
}