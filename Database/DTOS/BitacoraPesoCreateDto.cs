using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class BitacoraPesoCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El ID del animal es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un animal válido")]
    public int AnimalId { get; set; }

    [Required(ErrorMessage = "El peso es obligatorio")]
    [Range(0.1, 9999.99, ErrorMessage = "El peso debe estar entre 0.1 y 9,999.99")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "El alto es obligatorio")]
    [Range(0.1, 999.99, ErrorMessage = "El alto debe estar entre 0.1 y 999.99")]
    public decimal Alto { get; set; }

    [Required(ErrorMessage = "El largo es obligatorio")]
    [Range(0.1, 999.99, ErrorMessage = "El largo debe estar entre 0.1 y 999.99")]
    public decimal Largo { get; set; }

    [Required(ErrorMessage = "El diámetro es obligatorio")]
    [Range(0.1, 999.99, ErrorMessage = "El diámetro debe estar entre 0.1 y 999.99")]
    public decimal Diametro { get; set; }
}