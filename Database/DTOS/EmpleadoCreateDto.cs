using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class EmpleadoCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El primer nombre es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El primer nombre debe tener entre 2 y 50 caracteres")]
    public string PrimerNombre { get; set; } = null!;

    [StringLength(50, MinimumLength = 2, ErrorMessage = "El segundo nombre debe tener entre 2 y 50 caracteres")]
    public string? SegundoNombre { get; set; }

    [Required(ErrorMessage = "El primer apellido es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El primer apellido debe tener entre 2 y 50 caracteres")]
    public string PrimerApellido { get; set; } = null!;

    [StringLength(50, MinimumLength = 2, ErrorMessage = "El segundo apellido debe tener entre 2 y 50 caracteres")]
    public string? SegundoApellido { get; set; }

    [Required(ErrorMessage = "El estado activo es obligatorio")]
    public bool Activo { get; set; }
}