using Database.Models;
using Database.DTOS;

namespace BackOffice.Services
{
    public interface ITiposAlimentoService
    {
        Task<(List<TiposAlimento> tiposAlimento, int totalCount)> GetTiposAlimentoPaginadosAsync(int page, int pageSize);
        Task<TiposAlimento?> GetTiposAlimentoByIdAsync(int id);
        Task<TiposAlimento> CreateTiposAlimentoAsync(TiposAlimentoCreateDto tiposAlimentoDto);
        Task<TiposAlimento?> UpdateTiposAlimentoAsync(int id, TiposAlimentoCreateDto tiposAlimentoDto);
        Task<bool> DeleteTiposAlimentoAsync(int id);
        Task<bool> NombreExistsAsync(string nombre, int? excludeId = null);
    }
}