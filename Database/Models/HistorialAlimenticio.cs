using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("historial_alimenticio")]
[Index("AlimentoId", Name = "alimento_id")]
[Index("AnimalId", Name = "animal_id")]
[Index("EmpleadoId", Name = "empleado_id")]
public partial class HistorialAlimenticio
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("animal_id", TypeName = "int(11)")]
    public int AnimalId { get; set; }

    [Column("alimento_id", TypeName = "int(11)")]
    public int AlimentoId { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("empleado_id", TypeName = "int(11)")]
    public int EmpleadoId { get; set; }

    [Column("cantidad")]
    [Precision(6)]
    public decimal Cantidad { get; set; }

    [ForeignKey("AlimentoId")]
    [InverseProperty("HistorialAlimenticios")]
    public virtual Alimento Alimento { get; set; } = null!;

    [ForeignKey("AnimalId")]
    [InverseProperty("HistorialAlimenticios")]
    public virtual Animale Animal { get; set; } = null!;

    [ForeignKey("EmpleadoId")]
    [InverseProperty("HistorialAlimenticios")]
    public virtual Empleado Empleado { get; set; } = null!;
}
