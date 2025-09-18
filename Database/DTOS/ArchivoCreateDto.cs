using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class ArchivoCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El enlace es obligatorio")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "El enlace debe tener entre 5 y 500 caracteres")]
    [Url(ErrorMessage = "Debe ser un enlace válido")]
    public string Enlance { get; set; } = null!;

    [Required(ErrorMessage = "El ID del reporte es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un reporte válido")]
    public int ReporteId { get; set; }
}