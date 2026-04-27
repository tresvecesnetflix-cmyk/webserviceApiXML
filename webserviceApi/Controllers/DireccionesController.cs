using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Net;
using System.Runtime.Serialization.DataContracts;
using System.Xml;
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
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
          
            try
            {

                var xmlResult = await direccionesServicio.GetAll();

                if (string.IsNullOrEmpty(xmlResult))
                    return NotFound("No se encontrar elementos");

                return Content(xmlResult, "application/xml");
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"error con el servidor {ex}");
            }

        }

        [Authorize]
        [HttpGet("Id", Name = "ExtraerDireccion")]
        [Consumes("application/xml")]
        
        public async Task<ActionResult> GetById(int Id)
        {
            var usuario = await _servicioUsuarios.ObtenerUsuario();
            if(usuario == null)
            {

               return Unauthorized();
            }

            try
            {

                var xmlResult = await direccionesServicio.GetById(Id,usuario.Id);

                if (string.IsNullOrEmpty(xmlResult))
                    return NotFound("No encontro resultado");

                return Content(xmlResult, "application/xml");

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
        [Consumes("application/xml")]
        [Authorize]
        public async Task<IActionResult> PostByUser(XmlDocument xmlDirecciones)
        {

            var xmlString = xmlDirecciones.OuterXml;

            var usuario = await _servicioUsuarios.ObtenerUsuario();
            if(usuario == null)
            {
                return Unauthorized();
            }
            try
            {
                var xmlResult = await direccionesServicio.PostById(xmlString,usuario.Id);

                if (xmlResult == 0)
                    return NotFound();

                return CreatedAtRoute("ExtraerDireccion", new { id = xmlResult }, xmlResult);

            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error por parte del servidor{ex}");

            }

        }

        [HttpPost]
        [Consumes("application/xml")]
        public async Task<ActionResult> Post([FromBody] XmlDocument xmlDirecciones)
        {

           

            var xmlString = xmlDirecciones.OuterXml;


            try
            {

                var xmlResult = await direccionesServicio.Post(xmlString);
                if (xmlResult == 0)
                    return NotFound();

                return CreatedAtRoute("ExtraerDireccion", new { id = xmlResult }, xmlResult);

            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error de servidor{ex.Message}");

            }
        }

        [HttpPut]
        [Consumes("application/xml")]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] XmlDocument xmlDirecciones)
        {

            var xmlString = xmlDirecciones.OuterXml;

            var usuario = await _servicioUsuarios.ObtenerUsuario();

            if (usuario is null)
            {
                return Unauthorized("Usuario no encontrado");

            }
            try
            {

                var xmlResult = await direccionesServicio.Put(xmlString, usuario.Id);
                if (string.IsNullOrEmpty(xmlResult))
                    return BadRequest("No se pudo ingresa la direcciones al usuario correspondiente");




                return Content(xmlResult, "application/xml");
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error Departe del servidor {ex}");
            }

        }

    }
}
