using System.ComponentModel.DataAnnotations;

namespace Tecnicos.Data.Models;

public class Clientes
{
    [Key]
    public int CompraId { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El campo Descripcion es obligatorio Seleccione una de las opciones Disponibles")]
    public Double Monto { get; set; }
}
