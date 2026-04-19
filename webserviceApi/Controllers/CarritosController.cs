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
        [Consumes("application/xml")]
        public async Task<ActionResult> Post([FromBody] XmlDocument Carrito)
        {

            var connection = configuration.GetConnectionString("ConnectionString");

            using var con = new SqlConnection(connection);

            var xmlString = Carrito.OuterXml;

            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {

                return Unauthorized();
            }


            try
            {
                await con.OpenAsync();

                var ResultadoXML = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_carritoByUser]",
                                             new { Carrito = xmlString, UsuarioId = usuario.Id }, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(ResultadoXML))
                    return BadRequest();

                return Content(ResultadoXML, "application/xml");
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error departe del servidor {ex}");

            }

        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult> Get(int Id)
        {
            var connection = configuration.GetConnectionString("ConnectionString");

            var Carrito = $@"<CarritoItems>
                            <CarritoItem>
                            <Id>{Id}</Id>
                             </CarritoItem>
                            </CarritoItems>";

            using var con = new SqlConnection(connection);

            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {

                return Unauthorized();
            }

            try
            {
                await con.OpenAsync();

                var ResultadoXML = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_getCarritoByUser]",
                                             new { Carrito = Carrito, UsuarioId = usuario.Id }, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(ResultadoXML))
                    return BadRequest();

                return Content(ResultadoXML, "application/xml");


            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error departe del servidor {ex}");
            }


        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int Id)
        {
            var connection = configuration.GetConnectionString("ConnectionString");

            var Carrito = $@"<CarritoItems>
                            <CarritoItem>
                            <Id>{Id}</Id>
                             <CarritoItemItem>
                            </CarritoItems>";

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
                                           new { Carrito = Carrito, UsuarioId = usuario.Id }, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(respuestaXML))
                    return BadRequest();

                return Content(respuestaXML, "application/xml");

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error departe del servidor {ex}");
            }

        }
        [HttpPut]
        [Authorize]
        [Consumes("application/xml")]
        public async Task<ActionResult> Put([FromBody] string Carrito)
        {
            var connection = configuration.GetConnectionString("ConnectionString");

            using var con = new SqlConnection(connection);

            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
                return Unauthorized();

            try
            {
                await con.OpenAsync();

                var resultadoXML = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_UpdateCarritoByUser]",
                                         new { Carrito = Carrito, UsuarioId = usuario.Id }, commandType: CommandType.StoredProcedure);


                if (string.IsNullOrEmpty(resultadoXML))
                    return BadRequest();

                return Content(resultadoXML, "application/xml");


            }
            catch (SqlException ex)
            {


                return StatusCode(500, $"Error departe del servidor {ex}");
            }




        }
    }
}
