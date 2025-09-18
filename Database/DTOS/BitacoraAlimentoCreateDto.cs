using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class BitacoraAlimentoCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El ID del alimento es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un alimento válido")]
    public int AlimentoId { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, 999999.99, ErrorMessage = "El stock debe estar entre 0 y 999,999.99")]
    public decimal Stock { get; set; }
}