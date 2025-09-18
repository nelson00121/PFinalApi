using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Table("animales")]
[Index("CodigoRfid", Name = "codigo_rfid", IsUnique = true)]
[Index("EstadoAnimalId", Name = "estado_animal_id")]
[Index("RazaId", Name = "raza_id")]
public partial class Animale
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("codigo_rfid")]
    [StringLength(100)]
    public string CodigoRfid { get; set; } = null!;

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [Column("peso_actual_libras")]
    [Precision(6)]
    public decimal PesoActualLibras { get; set; }

    [Column("estado_animal_id", TypeName = "int(11)")]
    public int EstadoAnimalId { get; set; }

    [Column("raza_id", TypeName = "int(11)")]
    public int RazaId { get; set; }

    [InverseProperty("Animal")]
    public virtual ICollection<BitacoraPeso> BitacoraPesos { get; set; } = new List<BitacoraPeso>();

    [InverseProperty("Animal")]
    public virtual ICollection<BitacorasVacuna> BitacorasVacunas { get; set; } = new List<BitacorasVacuna>();

    [ForeignKey("EstadoAnimalId")]
    [InverseProperty("Animales")]
    public virtual EstadosAnimale EstadoAnimal { get; set; } = null!;

    [InverseProperty("Animal")]
    public virtual ICollection<HistorialAlimenticio> HistorialAlimenticios { get; set; } = new List<HistorialAlimenticio>();

    [ForeignKey("RazaId")]
    [InverseProperty("Animales")]
    public virtual Raza Raza { get; set; } = null!;

    [InverseProperty("Animal")]
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}
