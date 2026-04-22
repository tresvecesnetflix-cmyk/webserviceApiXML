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
        public async Task<IActionResult> GetAll()
        {
           
            try
            {
                var XML = await articuloServicio.ObtenerTodos();
           

                if (XML is null)
                    return NotFound("No se encotraron categorias");

                return Content(XML, "application/xml");

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error al obtener respuesta del servidor: {ex}");
            }


        }

        [HttpGet("{Id:int}", Name = "ObtenerArticulo")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var xmlResult = await articuloServicio.ObtenerPorId(Id);

                if (xmlResult is null)
                    return NotFound("no se encotro articulo");

                return Content(xmlResult, "application/xml");


            }
            catch (Exception ex)
            {


                return StatusCode(500, $"Error de parte del servidor {ex}");
            }

        }


        [HttpPost]
        [Consumes("application/xml")]
        public async Task<IActionResult> Post([FromBody] XmlDocument xmlArticulo)
        {
            try
            {
                var xmlString = xmlArticulo.OuterXml;

                var xmlResult = await articuloServicio.Post(xmlString);

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
        public async Task<IActionResult> Delete(int Id)
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
        public async Task<ActionResult> PostFoto([FromForm] ArticuloFotoDTO model)
        {
            string? UrlFoto = null;

            if (model.Foto != null)
            {

                UrlFoto = await almacenadorDeArchivos.Almacenar("articulos", model.Foto);
            }

            var xmlString = $@"
             <Articulos>
             <Articulo>
             <Nombre>{model.Nombre}</Nombre>
             <Descripcion>{model.Descripcion} </Descripcion>
             <Precio>{model.Precio}</Precio>
             <CategoriaId>{model.CategoriaId}</CategoriaId>
             <Sotck>{model.Sotck}</Sotck>
             <ColoresDisponibles>{model.ColoresDisponibles}</ColoresDisponibles>
             <TallasDisponibles>{model.TallasDisponibles}</TallasDisponibles>
             <Foto>{UrlFoto}</Foto>
             </Articulo>
             </Articulos>
              ";

            try
            {
                var xmlResult = await articuloServicio.PostFoto(xmlString);


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
