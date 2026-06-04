using Dapper;
using Microsoft.Data.SqlClient;
using webserviceApi.Servicios.Externos;
using System.Data;
using webserviceApi.DTOs;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml;
namespace webserviceApi.Repositorios
{
    public class CarritoRepositorio:ICarritoRepositorio
    {
        private readonly string _configuration;


        public CarritoRepositorio(IConfiguration configuration )
        {
            _configuration =  configuration.GetConnectionString("ConnectionString")!;
        }

        public async Task<string> Post(CarritoRequest model, string Id)

        {
            var xmlString = $@"<CarritoItems>
                            <CarritoItem>
                            <ArticuloId>{model.ArticuloId}</ArticuloId>
                            <Cantidad>{model.Cantidad}</Cantidad>
                             </CarritoItem>
                            </CarritoItems>";
            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_carritoByUser]",
                                     new { Carrito = xmlString, UsuarioId = Id }, commandType: CommandType.StoredProcedure);


            var doc = XDocument.Parse(xmlRespuesta);

            var estado = doc.Descendants("Estado").FirstOrDefault().Value;


            var mensaje = doc.Descendants("Resultado").FirstOrDefault().Value;


            if (estado == "Error")
            {
                var resultado = estado;

                throw new Exception(mensaje);
            }



            return mensaje;
        }


        public async Task<CarritoResponse> GetById(int Id, string IdUsuario)
        {
            var xmlString = $@"<CarritoItems>
                            <CarritoItem>
                            <Id>{Id}</Id>
                             </CarritoItem>
                            </CarritoItems>"; 

            var con = new SqlConnection(_configuration);

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_getCarritoByUser]"
                                     , new {Carrito= xmlString, usuarioId= IdUsuario}, commandType: CommandType.StoredProcedure);



            var carr = new CarritoResponse();
      
                var doc = XDocument.Parse(xmlRespuesta);

                var carrito = doc.Descendants("CarritoItem").FirstOrDefault();



            if (carrito!=null)
            {

                carr.AriticuloId = (int)carrito.Element("ArituculoId");
                carr.Cantidad = (int)carrito.Element("Cantidad");
                carr.subtotal = (decimal)carrito.Element("subtotal");

                return carr;

            }

            return carr ?? new CarritoResponse();


        }

        public async Task<string> Delete(int id, string UsuarioId)
        {
            var xmlString = $@"<CarritoItems>
                            <CarritoItem>
                            <id>{id}</id>
                             </CarritoItem>
                            </CarritoItems>";

            var con = new SqlConnection(_configuration);

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_deleteCarritoByUser]"
                                     , new { Carrito = xmlString, usuarioId = UsuarioId }, commandType: CommandType.StoredProcedure);

            var doc = XDocument.Parse(xmlRespuesta);


            var mensaje = doc.Descendants("Mensaje").FirstOrDefault().Value;


            var estado = doc.Descendants("Estado").FirstOrDefault().Value;

            if (estado=="Error")
            {
                throw new Exception(mensaje);

            }

            return mensaje;

        }



        public async Task<string> Put(CarritoRequest Carrito,string Id )
        {
            var carrt = $@"<CarritoItems>
                            <CarritoItem>
                            <Id>{Carrito.Id}</Id>
                            <Cantidad>{Carrito.Cantidad}</Cantidad>
                             </CarritoItem>
                            </CarritoItems>";


            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_UpdateCarritoByUser]",
                                      new {Carrito= carrt, UsuarioId = Id}, commandType: CommandType.StoredProcedure);


            var doc = XDocument.Parse(xmlRespuesta);

            var Estado = doc.Descendants("Estado").FirstOrDefault().Value;

            var Mensaje = doc.Descendants("Mensaje").FirstOrDefault().Value;

            if (Estado=="Error")
            {

                throw new Exception(Mensaje);
            }

            return Mensaje;

        }
    }

 
}
