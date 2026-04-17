using System.ComponentModel.DataAnnotations;

namespace webserviceApi.DTOs
{
    public class CredencialesUsuarioDTO
    {
        [Required]
        [EmailAddress]

        public required string email {  get; set; }

        [Required]

        public string password { get; set; }


    }
}
