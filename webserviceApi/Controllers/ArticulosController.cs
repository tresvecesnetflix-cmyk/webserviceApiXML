using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Xml;
using webserviceApi.DTOs;
using webserviceApi.Servicios;
using webserviceApi.Servicios.Externos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/articulo")]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloServicio articuloServicio;
        private readonly IAlmacenadorDeArchivos almacenadorDeArchivos;

        public ArticulosController(IArticuloServicio articuloServicio, IAlmacenadorDeArchivos almacenadorDeArchivos)
        {
            this.articuloServicio = articuloServicio;
            this.almacenadorDeArchivos = almacenadorDeArchivos;
        }


        [HttpGet]
        public async Task<ActionResult<List<ArticuloResponse>>> GetAll()
        {
           
            try
            {
                var XML = await articuloServicio.ObtenerTodos();
           

                if (XML is null)
                    return NotFound("No se encotraron categorias");

                return Ok(XML);

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error al obtener respuesta del servidor: {ex}");
            }


        }

        [HttpGet("{Id:int}", Name = "ObtenerArticulo")]
        public async Task<ActionResult<ArticuloResponse>> GetById(int Id)
        {
            var request = new ArticuloRequest
            {
                Id = Id
            };

            try
            {
                var xmlResult = await articuloServicio.GetById(request.Id);

                if (xmlResult is null)
                    return NotFound("no se encotro articulo");

                return Ok(xmlResult);


            }
            catch (Exception ex)
            {


                return StatusCode(500, $"Error de parte del servidor {ex}");
            }

        }


        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]

        public async Task<ActionResult> Post([FromBody] ArticuloRequest model)
        {
   
            try
            {

                var xmlResult = await articuloServicio.Post(model);

                if (xmlResult == 0)
                {
                    return NotFound("NO SE INGRESO ARTICULO");
                }

                return CreatedAtRoute("ObtenerArticulo", new { Id = xmlResult }, xmlResult);

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error de parte del server: {ex}");
            }
        }


        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
       
            try
            {
                var xmlResult = await articuloServicio.Delete(Id);

                if (string.IsNullOrEmpty(xmlResult))
                    return NotFound("No se pudo encotrar la categoria");

                return Content(xmlResult, "application/xml");

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error departe del server: {ex}");
            }

        }

        [HttpPost("foto")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> PostFoto([FromForm] ArticuloRequest model)
        {


            try
            {
                var xmlResult = await articuloServicio.PostFoto(model);


                if (xmlResult == 0)
                    return NotFound("No se ingreso el articulo");

                return CreatedAtRoute("ObtenerArticulo", new { id = xmlResult }, xmlResult);

            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error Conectar con el servidor:{ex}");
            }

        }

    }

}
