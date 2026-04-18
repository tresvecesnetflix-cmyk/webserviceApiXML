namespace webserviceApi.DTOs
{
    public class ArticuloFotoDTO
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio  { get; set; }

        public int CategoriaId { get; set; }

        public int Sotck { get; set; }

        public string ColoresDisponibles { get; set; }

        public string TallasDisponibles { get; set; }

        public IFormFile? Foto { get; set; }
    }
}
