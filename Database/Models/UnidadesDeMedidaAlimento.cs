using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("unidades_de_medida_alimentos")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class UnidadesDeMedidaAlimento
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("UnidadesDeMedidaAlimentos")]
    public virtual ICollection<Alimento> Alimentos { get; set; } = new List<Alimento>();
}
