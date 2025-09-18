using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("bitacora_pesos")]
[Index("AnimalId", Name = "animal_id")]
public partial class BitacoraPeso
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("animal_id", TypeName = "int(11)")]
    public int AnimalId { get; set; }

    [Column("peso")]
    [Precision(6)]
    public decimal Peso { get; set; }

    [Column("alto")]
    [Precision(6)]
    public decimal Alto { get; set; }

    [Column("largo")]
    [Precision(6)]
    public decimal Largo { get; set; }

    [Column("diametro")]
    [Precision(6)]
    public decimal Diametro { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [ForeignKey("AnimalId")]
    [InverseProperty("BitacoraPesos")]
    public virtual Animale Animal { get; set; } = null!;
}
