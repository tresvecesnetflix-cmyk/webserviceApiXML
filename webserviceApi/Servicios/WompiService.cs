using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using webserviceApi.DTOs;
using webserviceApi.Servicios;
using System.Net.Http.Headers;
using webserviceApi.Models.Wompi;

namespace webserviceApi.Servicios
{
    public class WompiService
    {
        private readonly HttpClient httpClient;
        private readonly WompiSetting settings;
        private string? _accesTokens;

        public WompiService(HttpClient httpClient, IOptions<WompiSetting> settings)
        {
            this.httpClient = httpClient;
            this.settings = settings.Value;
        }

        public async Task<WompiBitcointResponse> TransacctionBTC(BitcointTDO btc)
        {

            //crea el token 
            if (string.IsNullOrEmpty(_accesTokens))
                await ObtenerTokenAsync();

            //serealiza la informacion a JSON
            var content = new StringContent(JsonSerializer.Serialize(btc), Encoding.UTF8, "application/json");

            //Prepara el Token con el Bearer 
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accesTokens);

            //llama a la api

            var Response = await httpClient.PostAsync($"{settings.BaseUrl}/TransaccionCompra/Bitcoin", content);
            //leemos la respuesta
            var ResponseContent= await Response.Content.ReadAsStringAsync();

            //validamos si devuelve un estado diferente a Success. 
            if (!Response.IsSuccessStatusCode)
                throw new Exception($"Wompi BTC Error  {ResponseContent}   ");

            //deserealiszamos la respuesta json y la convertirmos a objeto 

            var result = JsonSerializer.Deserialize<WompiBitcointResponse>(ResponseContent);
            return result!;  
        }

        public async Task ObtenerTokenAsync()
        {
            var authContent = new StringContent(
                $"grant_type=client_credentials&client_id={settings.AppId}&client_secret={settings.ApiSecret}&audience=wompi_api",
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var response = await httpClient.PostAsync("https://id.wompi.sv/connect/token", authContent);

            var responseContect= await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Token Error: {responseContect}");

            var authResult = JsonSerializer.Deserialize<WompiAuthResponse>(responseContect);
            _accesTokens = authResult?.AccessToken;

            if (string.IsNullOrEmpty(_accesTokens))
                throw new Exception($"Token Vacìo");

        }
    }
}
