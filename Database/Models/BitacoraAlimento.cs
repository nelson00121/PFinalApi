using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("bitacora_alimento")]
[Index("AlimentoId", Name = "alimento_id")]
public partial class BitacoraAlimento
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("alimento_id", TypeName = "int(11)")]
    public int AlimentoId { get; set; }

    [Column("stock")]
    [Precision(6)]
    public decimal Stock { get; set; }

    [ForeignKey("AlimentoId")]
    [InverseProperty("BitacoraAlimentos")]
    public virtual Alimento Alimento { get; set; } = null!;
}
