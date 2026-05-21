using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Net;
using System.Runtime.Serialization.DataContracts;
using System.Xml;
using webserviceApi.DTOs;
using webserviceApi.Servicios;
using webserviceApi.Servicios.Externos;

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/direcciones")]
    public class DireccionesController : ControllerBase
    {

        private readonly IDireccionesServicio direccionesServicio;
        private readonly IServicioUsuarios _servicioUsuarios;

        public DireccionesController(IDireccionesServicio direccionesServicio , IServicioUsuarios servicioUsuarios)
        {
            this.direccionesServicio = direccionesServicio;
            _servicioUsuarios = servicioUsuarios;

        }

        [HttpGet]
        public async Task<ActionResult<List<DireccionesResponse>>> GetAll()
        {
          
            try
            {

                var xmlResult = await direccionesServicio.GetAll();

                if (xmlResult is null)
                {
                    return NotFound();
                }

                return Ok(xmlResult);
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"error con el servidor {ex}");
            }

        }

        [Authorize]
        [HttpGet("Id", Name = "ExtraerDireccion")]
        [Consumes("application/xml")]
        
        public async Task<ActionResult<DireccionesResponse>> GetById(int Id)
        {
            var usuario = await _servicioUsuarios.ObtenerUsuario();
            if(usuario == null)
            {

               return Unauthorized();
            }

            try
            {

                var xmlResult = await direccionesServicio.GetById(Id,usuario.Id);

                if (xmlResult==null)
                    return NotFound("No encontro resultado");

                return Ok(xmlResult);

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"error con el server :{ex}");
            }


        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int Id)
        {

            var usuario = await _servicioUsuarios.ObtenerUsuario();
            if(usuario == null)
            {

                return Unauthorized();  
            }
            try
            {

                var xmlResult = await direccionesServicio.Delete(Id, usuario.Id);

                if (string.IsNullOrEmpty(xmlResult))
                    return NotFound();

                return Content(xmlResult, "application/xml");

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error del servidor,{ex}");
            }

        }

        [HttpPost("ByUser")]
        [Consumes("application/json")]
        [Authorize]
        public async Task<ActionResult> PostByUser([FromBody]DireccionesRequest model )
        {


            var usuario = await _servicioUsuarios.ObtenerUsuario();
            if(usuario == null)
            {
                return Unauthorized();
            }
            try
            {
                var xmlResult = await direccionesServicio.PostById(model, usuario.Id);

                if (xmlResult == 0)
                    return NotFound();

                return CreatedAtRoute("ExtraerDireccion", new { Id = xmlResult }, xmlResult);

            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error por parte del servidor{ex}");

            }

        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult> Post([FromBody] DireccionesRequest model)
        {

           
            try
            {

                var xmlResult = await direccionesServicio.Post(model);
                if (xmlResult == 0)
                    return NotFound();

                return CreatedAtRoute("ExtraerDireccion", new { Id = xmlResult }, xmlResult);

            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error de servidor{ex.Message}");

            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [Authorize]
        public async Task<ActionResult<int>> Put([FromBody] DireccionesRequest model)
        {


            var usuario = await _servicioUsuarios.ObtenerUsuario();

            if (usuario is null)
            {
                return Unauthorized("Usuario no encontrado");

            }
            try
            {

                var xmlResult = await direccionesServicio.Put(model, usuario.Id);
                if (xmlResult==null)
                    return BadRequest("No se pudo ingresa la direcciones al usuario correspondiente");




                return CreatedAtRoute("ExtraerDireccion", new {Id=xmlResult},xmlResult);
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error Departe del servidor {ex}");
            }

        }

    }
}
