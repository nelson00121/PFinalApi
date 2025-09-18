using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class VacunaCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre de la vacuna es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Nombre { get; set; } = null!;

    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    public string? Descripcion { get; set; }
}