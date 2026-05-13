using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Xml;
using System.Data;
using webserviceApi.Servicios.Externos;
using webserviceApi.Servicios;
using webserviceApi.DTOs;

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/Carrito")]
    public class CarritosController: ControllerBase
    {
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly ICarritoServicio carritoServicio;

        public CarritosController(IServicioUsuarios servicioUsuarios, ICarritoServicio carritoServicio)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.carritoServicio = carritoServicio;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] CarritoRequest Carrito)
        {


            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {

                return Unauthorized();
            }


            try
            {

                var ResultadoXML = await carritoServicio.Post(Carrito,usuario.Id);

                if (ResultadoXML==null)
                    return BadRequest();

                return Ok(ResultadoXML);
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error departe del servidor {ex}");

            }

        }

        [HttpGet("{Id:int}")]
        [Authorize]
        public async Task<ActionResult<CarritoResponse>> Get( int Id)
        {
     
            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {

                return Unauthorized();
            }

            try
            {
                var ResultadoXML = await carritoServicio.GetById(Id,usuario.Id);

                if (ResultadoXML is null)
                    return BadRequest();

                return Ok(ResultadoXML);


            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error departe del servidor {ex}");
            }


        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
          


            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
            {
                return Unauthorized();
            }

            try
            {

                var respuestaXML = await carritoServicio.Delete(id,usuario.Id);

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
        public async Task<ActionResult> PutReducir([FromBody] CarritoRequest Carrito)
        {

            var usuario = await servicioUsuarios.ObtenerUsuario();
            if (usuario is null)
                return Unauthorized();

            try
            {
               var resultadoXML= await carritoServicio.Put(Carrito, usuario.Id);


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
