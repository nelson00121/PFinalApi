using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("usuarios")]
[Index("Email", Name = "email", IsUnique = true)]
[Index("EmpleadoId", Name = "empleado_id", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(50)]
    public string Password { get; set; } = null!;

    [Column("isAdmin")]
    public bool IsAdmin { get; set; }

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("empleado_id", TypeName = "int(11)")]
    public int EmpleadoId { get; set; }

    [ForeignKey("EmpleadoId")]
    [InverseProperty("Usuario")]
    public virtual Empleado Empleado { get; set; } = null!;
}
