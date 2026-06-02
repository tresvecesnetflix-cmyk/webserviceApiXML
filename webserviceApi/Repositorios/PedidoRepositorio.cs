using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly string _configuration;
        public PedidoRepositorio(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("ConnectionString")!;
        }


        public async Task<List<int>> Post(PedidosRequest model, string usuarioId)
        {
               var xmlDocument = $@"<PedidosDetalles>
                              <PedidoDetalle>
                              <DireccionId>{model.DireccionId}</DireccionId>
                              <itemCarritoId>{model.itemCarritoId}</itemCarritoId>
                              </PedidoDetalle>
                              </PedidosDetalles>";

            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_PedidoDetallePostByUser]",
                    new { PedidoDetalle = xmlDocument, UsuarioId = usuarioId }, commandType: CommandType.StoredProcedure);


            var doc = XDocument.Parse(xmlResult);

            var respuesta = doc.Descendants("Respuesta").FirstOrDefault().Value;

            var estado = doc.Descendants("Resultado").Elements("Resultado").FirstOrDefault().Value;

            if (estado == "Error")
            {
                throw new Exception(respuesta);

            }

            var ids = new List<int>();

            ids = doc.Descendants("Pedido").Select(x => (int)x.Element("Id")).ToList();


            return ids;

        }

        public async Task<PedidosResponse> Get(int Id, string usuarioId )
        {
            var con = new SqlConnection(_configuration);

            var xmlString = $@"<PedidoDetalles>
                              <PedidoDetalle>
                              <Id>{Id}</Id>
                              </PedidoDetalle>
                              </PedidoDetalles>";

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetPedidoDetalleById]",
                    new { PedidoDetalle = xmlString, UsuarioId = usuarioId }, commandType: CommandType.StoredProcedure);

            var doc = XDocument.Parse(xmlResult);


            var resultado = doc.Descendants("Resultado").FirstOrDefault();


            if (resultado!=null)
            {
                string estado = (string)resultado.Element("Error");
                
                    throw new Exception(estado);

            }

            var pedido = doc.Descendants("PedidoDetalle").FirstOrDefault();


            var pedidoDetalle = new PedidosResponse()
                {
                    Id = (int)pedido.Element("Id"),
                    Cantidad = (int)pedido.Element("Cantidad"),
                    PrecioUnitario = (decimal)pedido.Element("PrecioUnitario"),
                    Total = (decimal)pedido.Element("Total"),
                    Direccions = (string)pedido.Element("Direccions")
                };

            return pedidoDetalle;

        }

        public async Task<int> Delete(int Id , string usuarioId)
        {

            var con = new SqlConnection(_configuration);

            var xmlString = $@"<PedidosDetalles>
                              <PedidoDetalle>
                              <Id>{Id}</Id>
                              </PedidoDetalle>
                              </PedidosDetalles>";
            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_deletePedidoDetalleByUser]",
                    new { DetallePedido = xmlString, UsuarioId = usuarioId }, commandType: CommandType.StoredProcedure);

            var doc = XDocument.Parse(xmlResult);

            var resultado = doc.Descendants("RESULTADO").FirstOrDefault().Value;

            int id;

            if (resultado == "1")
            {

                id= Convert.ToInt32(resultado);


                return id;
                    
                    }


            return 0;

        }
    }
}
