using Database.Models;

namespace BackOffice.Services
{
    public interface IRazaService
    {
        Task<(List<Raza> razas, int totalCount)> GetRazasPaginadasAsync(int page, int pageSize);
        Task<Raza?> GetRazaByIdAsync(int id);
        Task<Raza> CreateRazaAsync(Raza raza);
        Task<Raza?> UpdateRazaAsync(int id, Raza raza);
        Task<bool> DeleteRazaAsync(int id);
        Task<bool> NombreExistsAsync(string nombre, int? excludeId = null);
    }
}