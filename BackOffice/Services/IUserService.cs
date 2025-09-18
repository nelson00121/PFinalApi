using Database.Models;
using Database.DTOS;

namespace BackOffice.Services
{
    public interface IUserService
    {
        Task<(List<Usuario> usuarios, int totalCount)> GetUsuariosPaginadosAsync(int page, int pageSize);
        Task<Usuario?> GetUsuarioByIdAsync(int id);
        Task<Usuario> CreateUsuarioAsync(UsuarioCreateDto usuarioDto);
        Task<Usuario?> UpdateUsuarioAsync(int id, UsuarioUpdateDto usuarioDto);
        Task<bool> DeleteUsuarioAsync(int id);
        Task<List<Empleado>> GetEmpleadosAsync();
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
        Task<Usuario?> AuthenticateAsync(string email, string password);
    }
}