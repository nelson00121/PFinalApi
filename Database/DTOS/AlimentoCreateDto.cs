using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class AlimentoCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El nombre del alimento es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El tipo de alimento es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de alimento válido")]
    public int TipoAlimentoId { get; set; }

    [Required(ErrorMessage = "La unidad de medida es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una unidad de medida válida")]
    public int UnidadesDeMedidaAlimentosId { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, 999999.99, ErrorMessage = "El stock debe estar entre 0 y 999,999.99")]
    public decimal Stock { get; set; }
}