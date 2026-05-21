namespace webserviceApi.DTOs
{
    public class DireccionesRequest
    {
        public int Id { get; set; }

        public string? Correo { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }
        public string? Telefono { get; set; }

        public string? TipoDocumento { get; set; }

        public string? NumeroDocumneto { get; set; }
        public string? Pais { get; set; }

        public string? Departamento { get; set; }

        public string? CiudadMunicipio { get; set; }

        public string? Colonia { get; set; }

        public string? Direccions { get; set; }
    }
}
