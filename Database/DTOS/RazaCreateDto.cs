using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;
public class RazaCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre de la raza es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Nombre { get; set; } = null!;
}