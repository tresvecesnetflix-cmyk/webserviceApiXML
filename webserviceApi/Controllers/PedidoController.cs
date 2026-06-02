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
        [Consumes("application/json")]
        public async Task<ActionResult<List<int>>> Post(PedidosRequest model)
        {


            var usuario = await servicioUsuarios.ObtenerUsuario();

            if (usuario is null)
            {
                return Unauthorized("Usuario no autenticado");

            }

            try
            {

                var resultXML = await pedidoServicio.Post(model,usuario.Id);

                if (!resultXML.Any())
                    return BadRequest();

                return Ok(resultXML);

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error del servidor{ex}");
            }

        }


        [HttpGet("{Id:int}")]
        [Authorize]

        public async Task<ActionResult<PedidosResponse>> Get(int  Id)
        {

            var usuario = await servicioUsuarios.ObtenerUsuario();


            try
            {
                var Resultado = await pedidoServicio.Get(Id,usuario.Id);

                if (Resultado==null)
                {

                    return NotFound();
                }

            

                return Ok(Resultado);

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
