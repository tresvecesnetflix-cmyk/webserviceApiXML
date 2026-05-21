namespace webserviceApi.DTOs
{
    public class PedidosRequest
    {
        public int Id { get; set; }

  
        public int itemCarritoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int DireccionId {  get; set; }
    }
}
