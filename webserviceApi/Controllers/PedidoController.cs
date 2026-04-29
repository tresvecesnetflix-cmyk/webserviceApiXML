using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Xml;
using webserviceApi.Servicios;
using System.Data;
using System.Xml.Linq;
using webserviceApi.DTOs;
using Microsoft.EntityFrameworkCore.Query;
using webserviceApi.Servicios.Externos;

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController:ControllerBase
    {
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IConfiguration configuration;
        private readonly IPedidoServicio pedidoServicio;

        public PedidoController(IServicioUsuarios servicioUsuarios, IConfiguration configuration, IPedidoServicio pedidoServicio)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.configuration = configuration;
            this.pedidoServicio = pedidoServicio;
        }

        [HttpPost]
        [Authorize]
        [Consumes("application/xml")]
        public async Task<ActionResult> Post(XmlDocument PedidoDetalle)
        {



            var xmlString = PedidoDetalle.OuterXml;

            var usuario = await servicioUsuarios.ObtenerUsuario();

            if (usuario is null)
            {
                return Unauthorized("Usuario no autenticado");

            }

            try
            {

                var resultXML = await pedidoServicio.Post(xmlString,usuario.Id);

                if (string.IsNullOrEmpty(resultXML))
                    return BadRequest();

                return Content(resultXML, "application/xml");

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error del servidor{ex}");
            }

        }


        [HttpGet]
        [Authorize]

        public async Task<ActionResult> Get(int  Id)
        {
            var connection = configuration.GetConnectionString("ConnectionString");

            using var con = new SqlConnection(connection);

            var usuario = await servicioUsuarios.ObtenerUsuario();


            try
            {

                await con.OpenAsync();

                var Resultado = await pedidoServicio.Get(Id,usuario.Id,usuario.Email);

                if (!Resultado.Any())
                {

                    return NotFound();
                }

            

                return Content(Resultado,"application/xml");

            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error departe del servidor: {ex}");

            }

        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(int Id)
        {




            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario == null)
                return Unauthorized();
            Console.Write(usuario.Id);
            try
            {
                var ResultadoXml = await pedidoServicio.Delete(Id,usuario.Id);
                return Content(ResultadoXml, "application/xml");

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Su saldo es insuficiente para realisar esta llamada {ex}");
            }


        }


    }
}
