using System.ComponentModel.DataAnnotations;

namespace webserviceApi.DTOs
{
    public class BitcointTDO
    {
     
            public int id { get; set; }

            [Required]
            [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
            public decimal Monto { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio")]
            public string NombreCliente { get; set; }

            [Required(ErrorMessage = "El apellido es obligatorio")]
            public string ApellidoCliente { get; set; }

            [Required(ErrorMessage = "El correo electrónico es obligatorio")]
            [EmailAddress(ErrorMessage = "Correo no válido")]
            public string EmailCliente { get; set; }

            [Required(ErrorMessage = "Debe ingresar su número de documento")]
            public string DocumentoIdentidadCliente { get; set; }

            [Required(ErrorMessage = "Debe seleccionar una fecha de nacimiento")]
            public DateTime FechaNacimientoCliente { get; set; }

            [Required(ErrorMessage = "La dirección es obligatoria")]
            public string DireccionCliente { get; set; }

            [Required(ErrorMessage = "Debe seleccionar una región")]
            public string IdRegion { get; set; }

            [Required(ErrorMessage = "Debe seleccionar un territorio")]
            public string IdTerritorio { get; set; }

            public string IdExterno { get; set; }

            /* public  ConfiguracionWompi Configuracion { get; set; }*/ //esta campo es la configuracion y asgiando en el backend de servicio de wompi.


       
    }
}
