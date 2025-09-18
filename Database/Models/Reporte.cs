using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("reportes")]
[Index("AnimalId", Name = "animal_id")]
[Index("EmpleadoId", Name = "empleado_id")]
public partial class Reporte
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("animal_id", TypeName = "int(11)")]
    public int AnimalId { get; set; }

    [Column("empleado_id", TypeName = "int(11)")]
    public int EmpleadoId { get; set; }

    [Column("descripcion")]
    [StringLength(200)]
    public string Descripcion { get; set; } = null!;

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [ForeignKey("AnimalId")]
    [InverseProperty("Reportes")]
    public virtual Animale Animal { get; set; } = null!;

    [InverseProperty("Reporte")]
    public virtual ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();

    [ForeignKey("EmpleadoId")]
    [InverseProperty("Reportes")]
    public virtual Empleado Empleado { get; set; } = null!;
}
