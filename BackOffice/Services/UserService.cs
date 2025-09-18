using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;
using Bycript;

namespace BackOffice.Services
{
    public class UserService : IUserService
    {
        private readonly DatabseContext _context;
        private readonly IBCryptService _bcryptService;

        public UserService(DatabseContext context, IBCryptService bcryptService)
        {
            _context = context;
            _bcryptService = bcryptService;
        }

        public async Task<(List<Usuario> usuarios, int totalCount)> GetUsuariosPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.Usuarios.CountAsync();
            
            var usuarios = await _context.Usuarios
                .Include(u => u.Empleado)
                .OrderByDescending(u => u.FechaRegistro)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (usuarios, totalCount);
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> CreateUsuarioAsync(UsuarioCreateDto usuarioDto)
        {
            var hashedPassword = _bcryptService.HashText(usuarioDto.Password);
            
            var usuario = new Usuario
            {
                Email = usuarioDto.Email,
                Password = hashedPassword,
                IsAdmin = usuarioDto.IsAdmin,
                Activo = usuarioDto.Activo,
                EmpleadoId = usuarioDto.EmpleadoId,
                FechaRegistro = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return await GetUsuarioByIdAsync(usuario.Id) ?? usuario;
        }

        public async Task<Usuario?> UpdateUsuarioAsync(int id, UsuarioUpdateDto usuarioDto)
        {
            var existingUsuario = await _context.Usuarios.FindAsync(id);
            if (existingUsuario == null) return null;

            existingUsuario.Email = usuarioDto.Email;
            existingUsuario.IsAdmin = usuarioDto.IsAdmin;
            existingUsuario.Activo = usuarioDto.Activo;
            existingUsuario.EmpleadoId = usuarioDto.EmpleadoId;

            await _context.SaveChangesAsync();
            return await GetUsuarioByIdAsync(id);
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await _context.Empleados
                .Where(e => e.Usuario == null)
                .OrderBy(e => e.PrimerNombre)
                .ThenBy(e => e.PrimerApellido)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var query = _context.Usuarios.Where(u => u.Email == email);
            
            if (excludeId.HasValue)
            {
                query = query.Where(u => u.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null) return false;

            if (!_bcryptService.VerifyText(changePasswordDto.CurrentPassword, usuario.Password))
            {
                return false;
            }

            usuario.Password = _bcryptService.HashText(changePasswordDto.NewPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> AuthenticateAsync(string email, string password)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Email == email && u.Activo);

            if (usuario == null) return null;

            if (!_bcryptService.VerifyText(password, usuario.Password))
            {
                return null;
            }

            return usuario;
        }
    }
}