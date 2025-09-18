using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("tipos_alimentos")]
[Index("NombreTipo", Name = "nombre_tipo", IsUnique = true)]
public partial class TiposAlimento
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre_tipo")]
    [StringLength(50)]
    public string NombreTipo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(200)]
    public string? Descripcion { get; set; }

    [InverseProperty("TipoAlimento")]
    public virtual ICollection<Alimento> Alimentos { get; set; } = new List<Alimento>();
}
