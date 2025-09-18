using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("razas")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class Raza
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("Raza")]
    public virtual ICollection<Animale> Animales { get; set; } = new List<Animale>();
}
