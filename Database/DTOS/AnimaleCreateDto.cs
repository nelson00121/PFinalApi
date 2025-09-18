using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class AnimaleCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El código RFID es obligatorio")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "El código RFID debe tener entre 5 y 50 caracteres")]
    public string CodigoRfid { get; set; } = null!;

    [Required(ErrorMessage = "El peso actual es obligatorio")]
    [Range(0.1, 9999.99, ErrorMessage = "El peso debe estar entre 0.1 y 9,999.99 libras")]
    public decimal PesoActualLibras { get; set; }

    [Required(ErrorMessage = "El estado del animal es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado válido")]
    public int EstadoAnimalId { get; set; }

    [Required(ErrorMessage = "La raza es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una raza válida")]
    public int RazaId { get; set; }



}