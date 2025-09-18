using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("compra_alimentos")]
[Index("AlimentoId", Name = "alimento_id")]
public partial class CompraAlimento
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("alimento_id", TypeName = "int(11)")]
    public int? AlimentoId { get; set; }

    [Column("cantidad_comprada")]
    [Precision(6)]
    public decimal? CantidadComprada { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [ForeignKey("AlimentoId")]
    [InverseProperty("CompraAlimentos")]
    public virtual Alimento? Alimento { get; set; }
}
