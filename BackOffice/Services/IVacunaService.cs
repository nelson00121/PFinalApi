using Database.Models;
using Database.DTOS;

namespace BackOffice.Services
{
    public interface IVacunaService
    {
        Task<(List<Vacuna> vacunas, int totalCount)> GetVacunasPaginadosAsync(int page, int pageSize);
        Task<Vacuna?> GetVacunaByIdAsync(int id);
        Task<Vacuna> CreateVacunaAsync(VacunaCreateDto vacunaDto);
        Task<Vacuna?> UpdateVacunaAsync(int id, VacunaCreateDto vacunaDto);
        Task<bool> DeleteVacunaAsync(int id);
        Task<bool> NombreExistsAsync(string nombre, int? excludeId = null);
    }
}