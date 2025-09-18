using Database.Data;
using Database.Models;
using Database.DTOS;

namespace BackOffice.Services
{
    public interface IAlimentoService
    {
        Task<(List<Alimento> alimentos, int totalCount)> GetAlimentosPaginadosAsync(int page, int pageSize);
        Task<Alimento?> GetAlimentoByIdAsync(int id);
        Task<Alimento> CreateAlimentoAsync(AlimentoCreateDto alimentoDto);
        Task<Alimento?> UpdateAlimentoAsync(int id, AlimentoCreateDto alimentoDto);
        Task<bool> DeleteAlimentoAsync(int id);
        Task<List<TiposAlimento>> GetTiposAlimentoAsync();
        Task<List<UnidadesDeMedidaAlimento>> GetUnidadesDeMedidaAsync();
        Task<bool> NombreExistsAsync(string nombre, int? excludeId = null);
    }
}