using Database.DTOS;
using Database.Models;

namespace BackOffice.Services;

public interface ICompraAlimentoService
{
    Task<List<CompraAlimento>> GetAllAsync();
    Task<CompraAlimento?> GetByIdAsync(int id);
    Task<CompraAlimento> CreateAsync(CompraAlimentoCreateDto dto);
    Task<CompraAlimento> UpdateAsync(int id, CompraAlimentoCreateDto dto);
    Task<bool> DeleteAsync(int id);
}