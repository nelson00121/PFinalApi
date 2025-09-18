using Database.Data;
using Database.Models;
using Database.DTOS;
using Microsoft.EntityFrameworkCore;
using Bucket;

namespace BackOffice.Services
{
    public interface IReporteService
    {
        Task<(List<Reporte> reportes, int totalCount)> GetReportesPaginadosAsync(int page, int pageSize);
        Task<Reporte?> GetReporteByIdAsync(int id);
        Task<Reporte> CreateReporteAsync(ReporteCreateDto reporteDto);
        Task<Reporte?> UpdateReporteAsync(int id, ReporteCreateDto reporteDto);
        Task<bool> DeleteReporteAsync(int id);
        Task<List<Animale>> GetAnimalesAsync();
        Task<List<Empleado>> GetEmpleadosAsync();
        Task<string> UploadImageAsync(Stream imageStream, string fileName, string contentType);
        Task<Reporte> CreateReporteWithImageAsync(ReporteCreateDto reporteDto, string imageUrl);
        Task<Reporte?> UpdateReporteWithImageAsync(int id, ReporteCreateDto reporteDto, string? imageUrl);
    }

    public class ReporteService : IReporteService
    {
        private readonly DatabseContext _context;
        private readonly IMinioService _minioService;

        public ReporteService(DatabseContext context, IMinioService minioService)
        {
            _context = context;
            _minioService = minioService;
        }

        public async Task<(List<Reporte> reportes, int totalCount)> GetReportesPaginadosAsync(int page, int pageSize)
        {
            var totalCount = await _context.Reportes.CountAsync();
            
            var reportes = await _context.Reportes
                .Include(r => r.Animal)
                    .ThenInclude(a => a.Raza)
                .Include(r => r.Empleado)
                .OrderByDescending(r => r.FechaRegistro)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (reportes, totalCount);
        }

        public async Task<Reporte?> GetReporteByIdAsync(int id)
        {
            return await _context.Reportes
                .Include(r => r.Animal)
                    .ThenInclude(a => a.Raza)
                .Include(r => r.Empleado)
                .Include(r => r.Archivos)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reporte> CreateReporteAsync(ReporteCreateDto reporteDto)
        {
            var reporte = new Reporte
            {
                AnimalId = reporteDto.AnimalId,
                EmpleadoId = reporteDto.EmpleadoId,
                Descripcion = reporteDto.Descripcion,
                FechaRegistro = DateTime.Now
            };

            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();

            return await GetReporteByIdAsync(reporte.Id) ?? reporte;
        }

        public async Task<Reporte?> UpdateReporteAsync(int id, ReporteCreateDto reporteDto)
        {
            var existingReporte = await _context.Reportes.FindAsync(id);
            if (existingReporte == null) return null;

            existingReporte.AnimalId = reporteDto.AnimalId;
            existingReporte.EmpleadoId = reporteDto.EmpleadoId;
            existingReporte.Descripcion = reporteDto.Descripcion;

            await _context.SaveChangesAsync();
            return await GetReporteByIdAsync(id);
        }

        public async Task<bool> DeleteReporteAsync(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null) return false;

            _context.Reportes.Remove(reporte);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Animale>> GetAnimalesAsync()
        {
            return await _context.Animales
                .Include(a => a.Raza)
                .OrderBy(a => a.CodigoRfid)
                .ToListAsync();
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await _context.Empleados
                .OrderBy(e => e.PrimerNombre)
                .ThenBy(e => e.PrimerApellido)
                .ToListAsync();
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName, string contentType)
        {
            return await _minioService.UploadImageAsync(imageStream, fileName, contentType);
        }

        public async Task<Reporte> CreateReporteWithImageAsync(ReporteCreateDto reporteDto, string imageUrl)
        {
            var reporte = new Reporte
            {
                AnimalId = reporteDto.AnimalId,
                EmpleadoId = reporteDto.EmpleadoId,
                Descripcion = reporteDto.Descripcion,
                FechaRegistro = DateTime.Now
            };

            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();

            var archivo = new Archivo
            {
                ReporteId = reporte.Id,
                Enlance = imageUrl
            };
            
            _context.Add(archivo);
            await _context.SaveChangesAsync();

            return await GetReporteByIdAsync(reporte.Id) ?? reporte;
        }

        public async Task<Reporte?> UpdateReporteWithImageAsync(int id, ReporteCreateDto reporteDto, string? imageUrl)
        {
            var existingReporte = await _context.Reportes.FindAsync(id);
            if (existingReporte == null) return null;

            existingReporte.AnimalId = reporteDto.AnimalId;
            existingReporte.EmpleadoId = reporteDto.EmpleadoId;
            existingReporte.Descripcion = reporteDto.Descripcion;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                var archivo = new Archivo
                {
                    ReporteId = id,
                    Enlance = imageUrl
                };
                
                _context.Add(archivo);
            }

            await _context.SaveChangesAsync();
            return await GetReporteByIdAsync(id);
        }
    }
}