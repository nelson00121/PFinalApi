using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("estados_animales")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class EstadosAnimale
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("EstadoAnimal")]
    public virtual ICollection<Animale> Animales { get; set; } = new List<Animale>();
}
