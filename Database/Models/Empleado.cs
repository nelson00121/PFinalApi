using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("empleado")]
public partial class Empleado
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("primer_nombre")]
    [StringLength(50)]
    public string PrimerNombre { get; set; } = null!;

    [Column("segundo_nombre")]
    [StringLength(50)]
    public string? SegundoNombre { get; set; }

    [Column("primer_apellido")]
    [StringLength(50)]
    public string PrimerApellido { get; set; } = null!;

    [Column("segundo_apellido")]
    [StringLength(50)]
    public string? SegundoApellido { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("Empleado")]
    public virtual ICollection<HistorialAlimenticio> HistorialAlimenticios { get; set; } = new List<HistorialAlimenticio>();

    [InverseProperty("Empleado")]
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();

    [InverseProperty("Empleado")]
    public virtual Usuario? Usuario { get; set; }
}
