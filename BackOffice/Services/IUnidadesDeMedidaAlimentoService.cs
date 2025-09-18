using Database.Models;
using Database.DTOS;

namespace BackOffice.Services
{
    public interface IUnidadesDeMedidaAlimentoService
    {
        Task<(List<UnidadesDeMedidaAlimento> unidades, int totalCount)> GetUnidadesDeMedidaAlimentoPaginadosAsync(int page, int pageSize);
        Task<UnidadesDeMedidaAlimento?> GetUnidadesDeMedidaAlimentoByIdAsync(int id);
        Task<UnidadesDeMedidaAlimento> CreateUnidadesDeMedidaAlimentoAsync(UnidadesDeMedidaAlimentoCreateDto unidadDto);
        Task<UnidadesDeMedidaAlimento?> UpdateUnidadesDeMedidaAlimentoAsync(int id, UnidadesDeMedidaAlimentoCreateDto unidadDto);
        Task<bool> DeleteUnidadesDeMedidaAlimentoAsync(int id);
        Task<bool> NombreExistsAsync(string nombre, int? excludeId = null);
    }
}