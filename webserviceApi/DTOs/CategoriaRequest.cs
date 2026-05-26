namespace webserviceApi.DTOs
{
    public class CategoriaRequest
    {
        public int Id { get; set; } 
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public IFormFile Foto { get; set; }

    }
}
