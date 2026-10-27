using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimaAPI.Models;

public class Ciudad
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(100)]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El pais es obligatorio.")]
    [MaxLength(100)]
    public string? Pais { get; set; }

    [Required(ErrorMessage = "La latitud es obligatoria.")]
    [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90.")]
    [Column(TypeName = "decimal(9,6)")]
    public decimal? Latitud { get; set; }

    [Required(ErrorMessage = "La longitud es obligatoria.")]
    [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180.")]
    [Column(TypeName = "decimal(9,6)")]
    public decimal? Longitud { get; set; }

    public ICollection<RegistroClima>? RegistrosClima { get; set; }

}

