using System.Text.Json.Serialization;

namespace webserviceApi.Models.Wompi
{
    public class WompiBitcointResponse
    {
        [JsonPropertyName("datosBitcoin")]
        public DatosBitcoin? DatosBitcoin { get; set; }

        [JsonPropertyName("idTransaccion")]
        public string? IdTransaccion { get; set; }

        [JsonPropertyName("esReal")]
        public bool EsReal { get; set; }

        [JsonPropertyName("esAprobada")]
        public bool EsAprobada { get; set; }

        [JsonPropertyName("codigoAutorizacion")]
        public string? CodigoAutorizacion { get; set; }

        [JsonPropertyName("mensaje")]
        public string? Mensaje { get; set; }

        [JsonPropertyName("formaPago")]
        public string? FormaPago { get; set; }

        [JsonPropertyName("monto")]
        public decimal Monto { get; set; }

        [JsonPropertyName("idExterno")]
        public string? IdExterno { get; set; }
    }
    public class DatosBitcoin
    {
        [JsonPropertyName("urlQR")]
        public string? UrlQR { get; set; }

        [JsonPropertyName("qrData")]
        public string? QrData { get; set; }

        [JsonPropertyName("ammountInBitcoins")]
        public decimal AmmountInBitcoins { get; set; }

        [JsonPropertyName("ammountInDollars")]
        public decimal AmmountInDollars { get; set; }

        [JsonPropertyName("fechaVencimiento")]
        public DateTime FechaVencimiento { get; set; }
    }
}
