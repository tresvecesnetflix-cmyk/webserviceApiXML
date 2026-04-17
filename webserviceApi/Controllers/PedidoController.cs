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

namespace webserviceApi.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController:ControllerBase
    {
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IConfiguration configuration;

        public PedidoController(IServicioUsuarios servicioUsuarios, IConfiguration configuration )
        {
            this.servicioUsuarios = servicioUsuarios;
            this.configuration = configuration;
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult> Post(XmlDocument PedidoDetalle)
        //{

        //    var connection = configuration.GetConnectionString("ConnectionString");

        //     using  var con = new SqlConnection(connection);
 
        //    var xmlString = PedidoDetalle.OuterXml;

        //    var usuario = await servicioUsuarios.ObtenerUsuario();

        //    Console.WriteLine(usuario.Id);

        //    if (usuario is null)
        //    {
        //        return Unauthorized("Usuario no autenticado");

        //    }

        //    try
        //    {
        //          await  con.OpenAsync();

        //        var resultXML = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_PedidoDetallePostByUser]",
        //            new { PedidoDetalle = xmlString,UsuarioId=usuario.Id}, commandType: CommandType.StoredProcedure);

        //        if (string.IsNullOrEmpty(resultXML))
        //            return BadRequest();

        //        return Content(resultXML, "application/xml");

        //    }catch(SqlException ex)
        //    {

        //        return StatusCode(500, $"Error del servidor{ex}");
        //    }

        //}


        //[HttpGet]
        //[Authorize]

        //public async Task<ActionResult> Get(XmlDocument PedidoDetalle)
        //{
        //    var connection = configuration.GetConnectionString("ConnectionString");

        //    using var con = new SqlConnection(connection);

        //    var usuario = await servicioUsuarios.ObtenerUsuario();

        //    var StringXML = PedidoDetalle.OuterXml;

        //    try{

        //        await con.OpenAsync();

        //        var Resultado = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetPedidoDetalleById]",
        //            new { PedidoDetalle=StringXML, UsuarioId=usuario!.Id}, commandType: CommandType.StoredProcedure);

        //        if (string.IsNullOrEmpty(Resultado))




        //            return BadRequest();

        //        //return Content(Resultado,"application/xml");

        //        var doc = XDocument.Parse(Resultado);

        //        var detalles = doc.Descendants("PedidoDetalle");

        //        if (detalles == null)
        //        {

        //            return BadRequest("Peido no encontrado penudo");
        //        }

        //        var Lista = new List<BitcointTDO>();
        //        foreach(var detalle in detalles)
        //        {
        //            Lista.Add(new BitcointTDO
        //            {
        //                id = (int)detalle.Element("Id"),
        //                Monto = (decimal)detalle.Element("Total"),
        //                DireccionCliente = (string)detalle.Element("Direccions"),
        //                EmailCliente = usuario.Email
        //            });
        
        //        };

        //        return Ok(Lista);
        //    }
        //    catch (SqlException ex)
        //    {
        //        return StatusCode(500,$"Error departe del servidor: {ex}");

        //    }

        //}

        //[HttpDelete]
        //[Authorize]
        //public async Task<IActionResult> Delete(XmlDocument PedidoDetalle)
        //{

        //    var connection =  configuration.GetConnectionString("ConnectionString");

        //    using var con =  new SqlConnection(connection);

        //    var xmlString = PedidoDetalle.OuterXml;

        //    var usuario = await servicioUsuarios.ObtenerUsuario();
        //    if (usuario == null)
        //        return Unauthorized();
        //    Console.Write(usuario.Id);
        //    try { 
        //    await con.OpenAsync();
        //        var ResultadoXml = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_deletePedidoDetalleByUser]",
        //                                                                      new { DetallePedido = xmlString, UsuarioId=usuario.Id}, commandType: CommandType.StoredProcedure);
        //        if (string.IsNullOrEmpty(ResultadoXml))
        //            return BadRequest();

        //        return Content(ResultadoXml,"application/xml");
            
        //    }catch (SqlException ex)
        //    {

        //        return StatusCode(500,$"Su saldo es insuficiente para realisar esta llamada {ex}");
        //    }


        //}


    }
}
