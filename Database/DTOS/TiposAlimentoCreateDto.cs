using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;
public class TiposAlimentoCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre del tipo es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre del tipo debe tener entre 2 y 100 caracteres")]
    public string NombreTipo { get; set; } = null!;

    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    public string? Descripcion { get; set; }
}