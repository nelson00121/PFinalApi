using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class ReporteCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El ID del animal es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un animal válido")]
    public int AnimalId { get; set; }

    [Required(ErrorMessage = "El ID del empleado es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un empleado válido")]
    public int EmpleadoId { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 1000 caracteres")]
    public string Descripcion { get; set; } = null!;

}