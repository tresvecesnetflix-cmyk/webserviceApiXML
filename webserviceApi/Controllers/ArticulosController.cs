using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Xml;
using webserviceApi.Servicios;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/articulo")]
    public class ArticulosController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        //private readonly IAlmacenadorDeArchivos almacenadorDeArchivos;

        //public ArticulosController(IConfiguration configuration, IAlmacenadorDeArchivos almacenadorDeArchivos)
        //{
        //    _configuration = configuration;
        //    this.almacenadorDeArchivos = almacenadorDeArchivos;
        //}


        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var connection = _configuration.GetConnectionString("ConnectionString");

        //    var con = new SqlConnection(connection);

        //    try
        //    {

        //        await con.OpenAsync();

        //        var xml = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetAllArticulos]",
        //                        commandType: CommandType.StoredProcedure);

        //        if (xml is null)
        //            return NotFound("No se encotraron categorias");

        //        return Content(xml, "application/xml");

        //    } catch (Exception ex)
        //    {

        //        return StatusCode(500, $"Error al obtener respuesta del servidor: {ex}");
        //    }


        //}

        //[HttpGet("{Id:int}", Name = "ObtenerArticulo")]
        //public async Task<IActionResult> GetById(int Id)
        //{
        //    var connection = _configuration.GetConnectionString("ConnectionString");

        //    var con = new SqlConnection(connection);


        //    //convertimos a XML el objeto recibido para poder pasarlo al procedimiento almacenado

        //    var xmlString = $@"<Articulos>
        //        <Articulo>
        //        <Id>{Id}</Id>
        //        </Articulo>
        //        </Articulos>";

        //    try
        //    {
        //        await con.OpenAsync();

        //        var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[sp_GetArituloById]",
        //            new { xmlArticulo = xmlString }, commandType: CommandType.StoredProcedure);

        //        if (xmlResult is null)
        //            return NotFound("no se encotro articulo");

        //        return Content(xmlResult, "application/xml");


        //    } catch (Exception ex)
        //    {


        //        return StatusCode(500, $"Error de parte del servidor {ex}");
        //    }

        //}


        //[HttpPost]
        //[Consumes("application/xml")]
        //public async Task<IActionResult> Post([FromBody] string xmlArticulo)
        //{

        //    var connection = _configuration.GetConnectionString("ConnectionString");

        //    var con = new SqlConnection(connection);

        //    try
        //    {
        //        await con.OpenAsync();

        //        var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].sp_postArticulo",
        //               new { xmlArticulo = xmlArticulo }, commandType: CommandType.StoredProcedure);

        //        if (xmlResult == 0)
        //        {
        //            return NotFound("NO SE INGRESO ARTICULO");
        //        }

        //        return CreatedAtRoute("ObtenerArticulo", new { Id = xmlResult }, xmlResult);

        //    } catch (SqlException ex)
        //    {

        //        return StatusCode(500, $"Error de parte del server: {ex}");
        //    }


        //}

        //[HttpDelete("{Id:int}")]   
        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var connection = _configuration.GetConnectionString("ConnectionString");

        //    var con = new SqlConnection(connection);

        //    var xmlString = $@"<Articulos>
        //        <Articulo>
        //        <Id>{Id}</Id>
        //        </Articulo>
        //        </Articulos>";

        //    try
        //    {
        //       await con.OpenAsync();

        //        var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].spu_DeleteArticulo",
        //            new {xmlArticulo=xmlString}, commandType:CommandType.StoredProcedure);

        //        if (string.IsNullOrEmpty(xmlResult))
        //            return NotFound("No se pudo encotrar la categoria");

        //        return Content(xmlResult,"application/xml");

        //    }catch (SqlException ex)
        //    {

        //        return StatusCode(500, $"Error departe del server: {ex}");
        //    }

        //}

        //[HttpPost("foto")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> PostFoto([FromForm] string Nombre, [FromForm] string Descripcion, [FromForm] decimal Precio,
        //                                          [FromForm] int CategoriaId, [FromForm] int Sotck, [FromForm] string ColoresDisponibles, 
        //                                          [FromForm] string TallasDisponibles, [FromForm]IFormFile Foto)
        //{


        //    var connection = _configuration.GetConnectionString("ConnectionString");

        //   using var con = new SqlConnection(connection);

        //    string? UrlFoto = null;

        //    if (Foto != null)
        //    {

        //        UrlFoto = await almacenadorDeArchivos.Almacenar("articulos",Foto);
        //    }

        //    var xmlString = $@"
        //     <Articulos>
        //     <Articulo>
        //     <Nombre>{Nombre}</Nombre>
        //     <Descripcion>{Descripcion} </Descripcion>
        //     <Precio>{Precio}</Precio>
        //     <CategoriaId>{CategoriaId}</CategoriaId>
        //     <Sotck>{Sotck}</Sotck>
        //     <ColoresDisponibles>{ColoresDisponibles}</ColoresDisponibles>
        //     <TallasDisponibles>{TallasDisponibles}</TallasDisponibles>
        //     <Foto>{Foto}</Foto>
        //     </Articulo>
        //     </Articulos>
        //      ";

        //    try
        //    {

        //       await con.OpenAsync();

        //        var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].sp_postFotoArticulo",
        //                                 new { xmlArticulo = xmlString }, commandType: CommandType.StoredProcedure);


        //        if (xmlResult == 0)
        //            return NotFound("No se ingreso el articulo");

        //        return CreatedAtRoute("ObtenerArticulo", new {id=xmlResult}, xmlResult);

        //    }catch(SqlException ex)
        //    {

        //        return StatusCode(500,$"Error Conectar con el servidor:{ex}");
        //    }

        //}
    }
}
