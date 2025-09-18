using Database.Data;
using Database.Models;
using Database.DTOS;

namespace BackOffice.Services
{
    public interface IBitacoraVacunaService
    {
        Task<(List<BitacorasVacuna> bitacoras, int totalCount)> GetBitacorasVacunaPaginadasAsync(int page, int pageSize, string searchTerm = "");
        Task<BitacorasVacuna?> GetBitacoraVacunaByIdAsync(int id);
        Task<BitacorasVacuna> CreateBitacoraVacunaAsync(BitacorasVacunaCreateDto bitacoraDto);
        Task<BitacorasVacuna?> UpdateBitacoraVacunaAsync(int id, BitacorasVacunaCreateDto bitacoraDto);
        Task<bool> DeleteBitacoraVacunaAsync(int id);
        Task<List<Animale>> GetAnimalesAsync();
        Task<List<Vacuna>> GetVacunasAsync();
        Task<bool> VacunaYaAplicadaAsync(int animalId, int vacunaId, int? excludeId = null);
    }
}