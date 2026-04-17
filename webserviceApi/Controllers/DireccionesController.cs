using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Runtime.Serialization.DataContracts;
using System.Xml;
using webserviceApi.Servicios;

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/direcciones")]
    public class DireccionesController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IServicioUsuarios _servicioUsuarios;
        private readonly string _ConnectionString;

        public DireccionesController(IConfiguration configuration, IServicioUsuarios servicioUsuarios)
        {
            _configuration = configuration;
            _servicioUsuarios = servicioUsuarios;
            _ConnectionString = _configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("No se encontro 'Connection String' en configuracion");
  
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var connection = _configuration.GetConnectionString("ConnectionString");

            using var con = new SqlConnection(connection);

            try {

                await con.OpenAsync();

                var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetAllDirecciones]",
                    commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(xmlResult))
                    return NotFound("No se encontrar elementos");

                return Content(xmlResult, "application/xml");
            } catch (SqlException ex)
            {

                return StatusCode(500, $"error con el servidor {ex}");
            }

        }

        [HttpGet("Id", Name = "ExtraerDireccion")]
        [Consumes("application/xml")]
        public async Task<IActionResult> GetById(XmlDocument xmlDirecciones)
        {
            var connection = _configuration.GetConnectionString("ConnectionString");

            var xmlString = xmlDirecciones.OuterXml;
            using var con = new SqlConnection(connection);

            try
            {
                await con.OpenAsync();

                var xmlResult = await con.QuerySingleOrDefaultAsync<string>("[dbo].[spu_GetDireccionById]",
                     new { xmlDirecciones = xmlString }, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(xmlResult))
                    return NotFound("No encontro resultado");

                return Content(xmlResult, "application/xml");

            } catch (SqlException ex)
            {

                return StatusCode(500, $"error con el server :{ex}");
            }


        }

        [HttpDelete]
        public async Task<IActionResult> Delete(XmlDocument xmlDirecciones)
        {
            var connection = _configuration.GetConnectionString("ConnectionString");

            var xmlString = xmlDirecciones.OuterXml;

            var con = new SqlConnection(connection);

            try
            {
                await con.OpenAsync();

                var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_DeleteDirecciones]",
                    new { xmlDirecciones = xmlString }, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(xmlResult))
                    return NotFound("Erriririririrorororororor");

                return Content(xmlResult, "application/xml");

            } catch (SqlException ex)
            {

                return StatusCode(500, $"Error del servidor,{ex}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Pots(XmlDocument xmlDirecciones)
        {
            var connection = _configuration.GetConnectionString("ConnectionString");

            var xmlString = xmlDirecciones.OuterXml;

            var con = new SqlConnection(connection);

            try {
                await con.OpenAsync();

                var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].[spu_postDirecciones]",
                       new { xmlDirecciones = xmlString }, commandType: CommandType.StoredProcedure);

                if (xmlResult == 0)
                    return NotFound("ERORORORORORORORO");

                return CreatedAtRoute("ExtraerDireccion", new { id = xmlResult }, xmlResult);

            } catch (SqlException ex)
            {
                return StatusCode(500, $"Error por parte del servidor{ex}");

            }

        }

        [HttpPost("usuarioId")]
        [Authorize]
        public async Task<IActionResult>PostUser([FromBody]XmlDocument xmlDirecciones)
        {
            var connection = _configuration.GetConnectionString("ConnectionString");

            var con = new SqlConnection(connection);

            //obtenemos el usuario autenticado

            var usuario = await _servicioUsuarios.ObtenerUsuario();
            if(usuario is null)
            {

                return Unauthorized("usuario no autenticado");
            }

            var xmlString = xmlDirecciones.OuterXml;

            
            try
            {
                await con.OpenAsync();

                var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_postDireccionesUser]",
                    new { xmlDirecciones = xmlString, UsuarioId = usuario.Id }, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(xmlResult))
                    return BadRequest("No se pudo insertar la direccion");

                return Content(xmlResult, "application/xml");

            }
            catch(SqlException ex)
            {
                return StatusCode(500, $"Error de servidor{ex.Message}");

            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult>Put([FromBody]XmlDocument xmlDirecciones)
        {
            using var con = new SqlConnection(_ConnectionString);

            var xmlString = xmlDirecciones.OuterXml;

            var usuario  = await _servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {
                return Unauthorized("Usuario no encontrado");

            }
            try
            {
                await con.OpenAsync();

                var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_PutDireccionesByUser]",
                    new { xmlDirecciones = xmlString, UsuarioId = usuario.Id }, commandType: CommandType.StoredProcedure);

                

                if (string.IsNullOrEmpty(xmlResult))
                    return BadRequest("No se pudo ingresa la direcciones al usuario correspondiente");




                return Content(xmlResult, "application/xml");
            }catch(SqlException ex)
            {

                return StatusCode(500, $"Error Departe del servidor {ex}");
            }

        }

    }
}
