using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("archivo")]
[Index("Enlance", Name = "enlance", IsUnique = true)]
[Index("ReporteId", Name = "reporte_id")]
public partial class Archivo
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("enlance")]
    [StringLength(300)]
    public string Enlance { get; set; } = null!;

    [Column("reporte_id", TypeName = "int(11)")]
    public int ReporteId { get; set; }

    [ForeignKey("ReporteId")]
    [InverseProperty("Archivos")]
    public virtual Reporte Reporte { get; set; } = null!;
}
