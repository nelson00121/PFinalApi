using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("vacunas")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class Vacuna
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(200)]
    public string? Descripcion { get; set; }

    [InverseProperty("Vacuna")]
    public virtual ICollection<BitacorasVacuna> BitacorasVacunas { get; set; } = new List<BitacorasVacuna>();
}
