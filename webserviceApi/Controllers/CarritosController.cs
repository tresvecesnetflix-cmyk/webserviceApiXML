using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Xml;
using webserviceApi.Servicios;
using System.Data;

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/Carrito")]
    public class CarritosController: ControllerBase
    {
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IConfiguration configuration;

        public CarritosController(IServicioUsuarios servicioUsuarios, IConfiguration configuration)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(XmlDocument Carrito)
        {

            var connection = configuration.GetConnectionString("ConnectionString");

            using var con =  new SqlConnection(connection);

            var xmlString = Carrito.OuterXml;

            var usuario = await servicioUsuarios.ObtenerUsuario();
            if(usuario is null)
            {

                return Unauthorized();
            }


            try
            {
               await con.OpenAsync();

                var ResultadoXML = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_carritoByUser]",
                                             new {Carrito= xmlString, UsuarioId=usuario.Id }, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(ResultadoXML))
                    return BadRequest();

                return Content(ResultadoXML, "application/xml");
            }catch(SqlException ex)
            {

                return StatusCode(500,$"Error departe del servidor {ex}");

            }

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get(XmlDocument Carrito)
        {
            var connection = configuration.GetConnectionString("ConnectionString");

            var xmlString = Carrito.OuterXml;

            using var con = new SqlConnection(connection);

            var usuario = await servicioUsuarios.ObtenerUsuario();
            if(usuario is null)
            {

                return Unauthorized();
            }

            try
            {
                await con.OpenAsync();

                var ResultadoXML = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_getCarritoByUser]",
                                             new {Carrito= xmlString, UsuarioId=usuario.Id}, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(ResultadoXML))
                    return BadRequest();

                return Content(ResultadoXML,"application/xml");


            }catch(SqlException ex)
            {

                return StatusCode(500,$"Error departe del servidor {ex}");
            }


        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(XmlDocument Carrito)
        {
            var connection = configuration.GetConnectionString("ConnectionString");

            var stringXml = Carrito.OuterXml;

            using var con = new SqlConnection(connection);

            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {
                return Unauthorized();
            }

            try
            {
                await con.OpenAsync();

                var respuestaXML = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_deleteCarritoByUser]",
                                           new {Carrito= stringXml, UsuarioId= usuario.Id}, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(respuestaXML))
                    return BadRequest();

                return Content(respuestaXML,"application/xml");

            }
            catch(SqlException ex)
            {

                return StatusCode(500, $"Error departe del servidor {ex}");
            }

        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put(XmlDocument Carrito)
        {
            var connection = configuration.GetConnectionString("ConnectionString");

            var stringXML= Carrito.OuterXml;

            using var con = new SqlConnection(connection);

            var usuario = await servicioUsuarios.ObtenerUsuario();
            if(usuario  is null)
                return Unauthorized();

            try
            {
                await con.OpenAsync();

                var resultadoXML = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_UpdateCarritoByUser]",
                                         new {Carrito= stringXML, UsuarioId=usuario.Id},commandType: CommandType.StoredProcedure);


                if (string.IsNullOrEmpty(resultadoXML))
                    return BadRequest();

                return Content(resultadoXML,"application/xml");


            }catch(SqlException ex)
            {


                return StatusCode(500,$"Error departe del servidor {ex}");
            }




        }
    }
}
