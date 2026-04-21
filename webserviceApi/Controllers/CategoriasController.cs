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

    //    [HttpGet("{Id:int}", Name = "ObtenerCategoria")]
    //    public async Task<IActionResult> ById(int Id)
    //    {

    //        var connection = _configuration.GetConnectionString("ConnectionString");

    //        using var con = new SqlConnection(connection);

    //        try
    //        {
    //            await con.OpenAsync();
    //            var xml = await con.QueryFirstOrDefaultAsync<string>(
    //                "[dbo].[sp_GetCategoriaByid]",
    //                new { Id },
    //                commandType: CommandType.StoredProcedure);

    //            if (string.IsNullOrEmpty(xml))
    //                return NotFound("no se an agregado articulos");

    //            return Content(xml, "application/xml");
    //        }
    //        catch (Exception e)
    //        {
    //            return StatusCode(500, $"Error:{e.Message}");
    //        }
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Pots([FromBody] string xmlCategoria)
    //    {
    //        var connection = _configuration.GetConnectionString("ConnectionString");

    //        var con = new SqlConnection(connection);

    //        try
    //        {
    //            await con.OpenAsync();
    //            var nuevoId = await con.QueryFirstOrDefaultAsync<int>("[dbo].[sp_InsertarCategoria]",
    //                new { xmlCategoria = xmlCategoria }, commandType: CommandType.StoredProcedure);

    //            if (nuevoId == 0)
    //                return BadRequest("No se pudo insertar categoria");

    //            return CreatedAtRoute("ObtenerCategoria", new { id = nuevoId }, nuevoId);

    //        }
    //        catch (SqlException ex)
    //        {

    //            return StatusCode(500, $"Error al insertar:  {ex.Message}");
    //        }
    //        catch (Exception ex)
    //        {

    //            return StatusCode(500, $"Error al insertar:  {ex.Message}");
    //        }
    //    }
    //    [HttpPut]
    //    [Consumes("application/xml")]

    //    public async Task<IActionResult> put([FromBody] XmlDocument xmlCategoria)
    //    {

    //        var xmlString = xmlCategoria.OuterXml;

    //        var connection = _configuration.GetConnectionString("ConnectionString");

    //        try
    //        {
    //            var con = new SqlConnection(connection);

    //            await con.OpenAsync();


    //            var xml = await con.QueryFirstOrDefaultAsync<int>("[dbo].[sp_updateCategoria]",
    //               new { xmlCategoria = xmlString }, commandType: CommandType.StoredProcedure);

    //            if (xml == 0)
    //                return NotFound("categoria no encontrada");

    //            return NoContent();


    //        }
    //        catch (SqlException ex)
    //        {
    //            return StatusCode(500, $"Error a insertar: {ex}");

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"Error al insertar: {ex}");

    //        }
    //    }

    //    [HttpDelete("{Id:int}")]
    //    public async Task<IActionResult> Delete(int Id)
    //    {
    //        var xmlString = $@"<Categorias>
    //                <Categoria>
    //               <Id>{Id}</Id>
    //              </Categoria>
    //               </Categorias>";


    //        var connection = _configuration.GetConnectionString("ConnectionString");

    //        try
    //        {
    //            var con = new SqlConnection(connection);

    //            await con.OpenAsync();

    //            var xml = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_DeleteCategoria]",
    //                new { xmlCategoria = xmlString }, commandType: CommandType.StoredProcedure);

    //            if (string.IsNullOrEmpty(xml))
    //                return NotFound("categoria no encontrada");

    //            return Content(xml, "application/xml");


    //        }
    //        catch (SqlException ex)
    //        {

    //            return StatusCode(500, $"error al procesar la solicitud: {ex}");
    //        }
    //        catch (Exception ex)
    //        {

    //            return StatusCode(500, $"error al procesar la solicitud: {ex}");
    //        }

    //    }


    //    [HttpPost("potsFoto")]
    //    [Consumes("multipart/form-data")]
    //    public async Task<IActionResult> PotsFoto([FromForm] string Titulo, [FromForm] string Descripcion, [FromForm] IFormFile Foto)
    //    {
    //        string? urlFoto = null;

    //        if (Foto != null)
    //        {
    //            urlFoto = await _almacenadorDeArchivos.Almacenar("categorias", Foto);

    //        }


    //        var xml = $@"<Categoria>
    //<Titulo>{Titulo}</Titulo>
    //<Descripcion>{Descripcion}</Descripcion>
    //<Foto>{urlFoto}</Foto>
    // </Categoria>";


    //        var connection = _configuration.GetConnectionString("ConnectionString");

    //        var con = new SqlConnection(connection);

    //        try
    //        {
    //            await con.OpenAsync();

    //            var xmlFoto = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_PotsCategoriaFoto]",
    //                new { xmlCategoria = xml }, commandType: CommandType.StoredProcedure);

    //            if (string.IsNullOrEmpty(xmlFoto))
    //            {
    //                return NotFound("No existe categoria ");

    //            }
    //            return Content(xmlFoto, "application/xml");
    //        }
    //        catch (SqlException ex)
    //        {
    //            return BadRequest($"<Error>{ex.Message}</Error>");
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest($"<Error>{ex.Message}</Error>");
    //        }
    //    }

    }
}
