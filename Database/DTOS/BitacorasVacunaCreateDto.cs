using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class BitacorasVacunaCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El ID del animal es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un animal válido")]
    public int AnimalId { get; set; }

    [Required(ErrorMessage = "El ID de la vacuna es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una vacuna válida")]
    public int VacunaId { get; set; }
}