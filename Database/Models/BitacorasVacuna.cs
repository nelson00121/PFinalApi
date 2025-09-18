using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("bitacoras_vacunas")]
[Index("AnimalId", Name = "animal_id")]
[Index("VacunaId", Name = "vacuna_id")]
public partial class BitacorasVacuna
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("animal_id", TypeName = "int(11)")]
    public int AnimalId { get; set; }

    [Column("vacuna_id", TypeName = "int(11)")]
    public int VacunaId { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [ForeignKey("AnimalId")]
    [InverseProperty("BitacorasVacunas")]
    public virtual Animale Animal { get; set; } = null!;

    [ForeignKey("VacunaId")]
    [InverseProperty("BitacorasVacunas")]
    public virtual Vacuna Vacuna { get; set; } = null!;
}
