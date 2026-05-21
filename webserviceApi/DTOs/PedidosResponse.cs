namespace webserviceApi.DTOs
{
    public class PedidosResponse
    {
        public int Id { get; set; }


        public int itemCarritoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public string Direccions { get; set; }

        public decimal Total { get; set; }
    }
}
