using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimaAPI.Models;

public class RegistroClima
{
    public int Id { get; set; }

    [Required(ErrorMessage ="La fecha es obligatoria.")]
    public DateTime? Fecha { get; set; }

    [Required(ErrorMessage ="La temperatura es obligatoria.")]
    [Range(-60, 60, ErrorMessage = "La temperatura debe estar entre -60 y 60 grados.")]
    [Column(TypeName = "decimal(3,1)")]
    public decimal? TemperaturaC { get; set; }

    [Required(ErrorMessage ="La humedad es obligatoria.")]
    [Range(0, 100, ErrorMessage = "La humedad debe estar entre 0 y 100.")]
    public int? Humedad { get; set; }

    [MaxLength(200, ErrorMessage = "La descripcion no debe superar los {1} caracteres. ")]
    public string? Descripcion { get; set; }

    public int CiudadId { get; set; }
    public Ciudad? Ciudad { get; set;}

}
