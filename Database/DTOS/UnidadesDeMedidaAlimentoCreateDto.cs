using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class UnidadesDeMedidaAlimentoCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre de la unidad es obligatorio")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 50 caracteres")]
    public string Nombre { get; set; } = null!;
}