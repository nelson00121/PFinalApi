using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Services
{
    public interface IEmpleadoService
    {
        Task<(List<Empleado> empleados, int totalCount)> GetEmpleadosPaginadosAsync(int page, int pageSize);
        Task<Empleado?> GetEmpleadoByIdAsync(int id);
        Task<Empleado> CreateEmpleadoAsync(Empleado empleado);
        Task<Empleado?> UpdateEmpleadoAsync(int id, Empleado empleado);
        Task<bool> DeleteEmpleadoAsync(int id);
    }

    public class EmpleadoService : IEmpleadoService
    {
        private readonly DatabseContext _context;

        public EmpleadoService(DatabseContext context)
        {
            _context = context;
        }

        public async Task<(List<Empleado> empleados, int totalCount)> GetEmpleadosPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.Empleados.CountAsync();
            
            var empleados = await _context.Empleados
                .Where(e => e.Activo)
                .OrderByDescending(e => e.FechaRegistro)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (empleados, totalCount);
        }

        public async Task<Empleado?> GetEmpleadoByIdAsync(int id)
        {
            return await _context.Empleados
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Empleado> CreateEmpleadoAsync(Empleado empleado)
        {
            empleado.FechaRegistro = DateTime.Now;
            empleado.Activo = true;
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return empleado;
        }

        public async Task<Empleado?> UpdateEmpleadoAsync(int id, Empleado empleado)
        {
            var existingEmpleado = await _context.Empleados.FindAsync(id);
            if (existingEmpleado == null) return null;

            existingEmpleado.PrimerNombre = empleado.PrimerNombre;
            existingEmpleado.SegundoNombre = empleado.SegundoNombre;
            existingEmpleado.PrimerApellido = empleado.PrimerApellido;
            existingEmpleado.SegundoApellido = empleado.SegundoApellido;
            existingEmpleado.Activo = empleado.Activo;

            await _context.SaveChangesAsync();
            return existingEmpleado;
        }

        public async Task<bool> DeleteEmpleadoAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return false;

            empleado.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}