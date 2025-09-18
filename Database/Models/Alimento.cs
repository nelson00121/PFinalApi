using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("alimentos")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
[Index("TipoAlimentoId", Name = "tipo_alimento_id")]
[Index("UnidadesDeMedidaAlimentosId", Name = "unidades_de_medida_alimentos_id")]
public partial class Alimento
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    public string? Nombre { get; set; }

    [Column("tipo_alimento_id", TypeName = "int(11)")]
    public int TipoAlimentoId { get; set; }

    [Column("unidades_de_medida_alimentos_id", TypeName = "int(11)")]
    public int UnidadesDeMedidaAlimentosId { get; set; }

    [Column("stock")]
    [Precision(10)]
    public decimal Stock { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("Alimento")]
    public virtual ICollection<BitacoraAlimento> BitacoraAlimentos { get; set; } = new List<BitacoraAlimento>();

    [InverseProperty("Alimento")]
    public virtual ICollection<CompraAlimento> CompraAlimentos { get; set; } = new List<CompraAlimento>();

    [InverseProperty("Alimento")]
    public virtual ICollection<HistorialAlimenticio> HistorialAlimenticios { get; set; } = new List<HistorialAlimenticio>();

    [ForeignKey("TipoAlimentoId")]
    [InverseProperty("Alimentos")]
    public virtual TiposAlimento TipoAlimento { get; set; } = null!;

    [ForeignKey("UnidadesDeMedidaAlimentosId")]
    [InverseProperty("Alimentos")]
    public virtual UnidadesDeMedidaAlimento UnidadesDeMedidaAlimentos { get; set; } = null!;
}
