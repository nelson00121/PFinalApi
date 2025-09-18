using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class CompraAlimentoCreateDto
{
    public int? Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un alimento válido")]
    public int? AlimentoId { get; set; }

    [Range(0.01, 999999.99, ErrorMessage = "La cantidad debe estar entre 0.01 y 999,999.99")]
    public decimal? CantidadComprada { get; set; }
}