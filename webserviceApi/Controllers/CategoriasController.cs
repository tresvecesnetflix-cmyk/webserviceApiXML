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
        public async Task<ActionResult<List<CategoriaResponse>>> GetAll()
        {

            try
            {
                var xml = await categoriaServicio.ListaCategoria();


                if (xml is null) 
                {
                    return NotFound();
                }

             return Ok(xml);
            }
            catch (SqlException ex)
            {

                return StatusCode(500, $"Error departe del servidor: {ex}");
            }
          
        }

        [HttpGet("{Id:int}", Name = "ObtenerCategoria")]
        public async Task<ActionResult> ById(int Id)
        {

    
            var xml =await categoriaServicio.GetById(Id);

            try
            {
                if (xml is null)
                    return NotFound("no se encotro categoria");

                return Ok(xml);

            }
            catch (SqlException e)
            {
                return StatusCode(500, $"Error:{e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Pots(CategoriaRequest model)

        {
            try
            {
                var xmlResult = await categoriaServicio.Post(model);
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
        public async Task<ActionResult> put([FromBody] CategoriaRequest model)
        {

            
            try
            {


                var xml = await categoriaServicio.Put(model);

                if (xml == 0)
                    return NotFound("categoria no encontrada");

                return Ok(xml);


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
                if (xml != 0)
                {
                    return Ok(xml);

                }

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
        public async Task<ActionResult> PostFoto([FromForm]CategoriaRequest model)
        {
      
            try
            {

                var xmlFoto = await categoriaServicio.PostFoto(model);

                if (xmlFoto == null)
                {
                    return NotFound();

                }
                return CreatedAtRoute("ObtenerCategoria", new {id=xmlFoto},xmlFoto);

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
