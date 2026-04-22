using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.CompilerServices;
using webserviceApi.Servicios;
using webserviceApi.Servicios.Externos;
using webserviceApi.Repositorios;
using Microsoft.EntityFrameworkCore.Query.Internal;
using webserviceApi.DTOs;

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/Cat")]
    public class CategoriasController : ControllerBase
    { 
    
        private readonly ICategoriaRepositorio categoriaServicio;
        private readonly IAlmacenadorDeArchivos _almacenadorDeArchivos;

        public CategoriasController(ICategoriaRepositorio categoriaServicio, IAlmacenadorDeArchivos almacenadorDeArchivos)
        {
            this.categoriaServicio = categoriaServicio;
            _almacenadorDeArchivos = almacenadorDeArchivos;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {

            try
            {
                var xml = await categoriaServicio.ListaCategoria();


                if (!string.IsNullOrEmpty(xml)) 
                {
                    return Content(xml);

                }

                return NotFound();
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error departe del servidor: {ex}");
            }
          
        }

        [HttpGet("{Id:int}", Name = "ObtenerCategoria")]
        public async Task<IActionResult> ById(int Id)
        {

    
            var xml =await categoriaServicio.GetById(Id);

            try
            {
                if (xml is null)
                    return NotFound("no se encotro articulo");

                return Content(xml, "application/xml");

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error:{e.Message}");
            }
        }

        [HttpPost]
        [Consumes("application/xml")]
        public async Task<ActionResult> Pots([FromBody] XmlDocument xmlCategoria)
        {
            try
            {
                var xmlString = xmlCategoria.OuterXml;

                var xmlResult = await categoriaServicio.Post(xmlString);
                if (xmlResult == 0) 
                {
                    return NotFound();
                }
                return CreatedAtRoute("ObtenerCategoria", new {Id= xmlResult }, xmlResult);
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error al insertar:  {ex.Message}");
            }
   
        }
        [HttpPut]
        [Consumes("application/xml")]

        public async Task<ActionResult> put([FromBody] XmlDocument xmlCategoria)
        {

    
            try
            {
                var stringXML = xmlCategoria.OuterXml;



                var xml = await categoriaServicio.Put(stringXML);

                if (xml == 0)
                    return NotFound("categoria no encontrada");

                return NoContent();


            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error a insertar: {ex}");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar: {ex}");

            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {

            try
            {
                var xml = await categoriaServicio.delete(Id);
                if(!string.IsNullOrEmpty(xml))
                return NoContent();

                return NotFound();
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"error al procesar la solicitud: {ex}");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"error al procesar la solicitud: {ex}");
            }

        }


        [HttpPost("potsFoto")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> PostFoto([FromForm]CategoriaFotoDTO model)
        {
            string? urlFoto = null;

            if (model.Foto != null)
            {
                urlFoto = await _almacenadorDeArchivos.Almacenar("categorias", model.Foto);

            }


            var xmlString = $@"<Categoria>
        <Titulo>{model.Titulo}</Titulo>
        <Descripcion>{model.Descripcion}</Descripcion>
        <Foto>{urlFoto}</Foto>
         </Categoria>";




            try
            {

                var xmlFoto = await categoriaServicio.PostFoto(xmlString);

                if (!string.IsNullOrEmpty(xmlFoto))
                {
                    return Content(xmlFoto, "application/xml");

                }
                return NotFound("No existe categoria ");

            }
            catch (SqlException ex)
            {
                return BadRequest($"<Error>{ex.Message}</Error>");
            }
            catch (Exception ex)
            {
                return BadRequest($"<Error>{ex.Message}</Error>");
            }
        }

    }
}
