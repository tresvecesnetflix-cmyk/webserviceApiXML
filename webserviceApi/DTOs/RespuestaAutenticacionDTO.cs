namespace webserviceApi.DTOs
{
    public class RespuestaAutenticacionDTO
    {
        public required string Token { get; set; }

        public DateTime Expiracion { get; set; }

        public string? UsuarioId { get; set; }
    }
}
