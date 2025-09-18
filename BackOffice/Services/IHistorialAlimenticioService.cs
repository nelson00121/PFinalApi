using Database.DTOS;
using Database.Models;

namespace BackOffice.Services;

public interface IHistorialAlimenticioService
{
    Task<List<HistorialAlimenticio>> GetAllAsync();
    Task<HistorialAlimenticio?> GetByIdAsync(int id);
    Task<HistorialAlimenticio> CreateAsync(HistorialAlimenticioCreateDto dto);
    Task<HistorialAlimenticio> UpdateAsync(int id, HistorialAlimenticioCreateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<Animale>> GetAnimalesAsync();
    Task<List<Alimento>> GetAlimentosAsync();
    Task<List<Empleado>> GetEmpleadosAsync();
    Task<List<BitacoraAlimento>> GetBitacoraAlimentoByIdAsync(int alimentoId);
}