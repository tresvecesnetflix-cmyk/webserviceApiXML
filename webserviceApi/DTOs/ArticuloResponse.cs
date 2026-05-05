namespace webserviceApi.DTOs
{
    public class ArticuloResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int CategoriaId { get; set; }

        public int Sotck { get; set; }

        public string ColoresDisponibles { get; set; }

        public string TallasDisponbles { get; set; }

        public string Foto { get; set; }
    }
}
