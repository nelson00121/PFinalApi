using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class HistorialAlimenticioCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El ID del animal es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un animal válido")]
    public int AnimalId { get; set; }

    [Required(ErrorMessage = "El ID del alimento es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un alimento válido")]
    public int AlimentoId { get; set; }

    [Required(ErrorMessage = "El ID del empleado es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un empleado válido")]

    public decimal Cantidad { get; set; }

    public int EmpleadoId { get; set; }
}