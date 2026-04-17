using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class XmlController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public XmlController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("{id}")]
  
    public async Task<IActionResult> GetXml(int id)
    {
    
        string connString = _configuration.GetConnectionString("AdventureWorks");

        if (string.IsNullOrEmpty(connString))
            return BadRequest("No se encontró la cadena de conexión 'AdventureWorks' en appsettings.json");

        using var conn = new SqlConnection(connString);

        try
        {
            await conn.OpenAsync();

            var xml = await conn.QueryFirstOrDefaultAsync<string>(
                "dbo.sp_humanresource",
                new { id },
                commandType: CommandType.StoredProcedure);

            if (string.IsNullOrWhiteSpace(xml))
                return NotFound($"No se generó XML para el ID {id}");

            return Content(xml, "application/xml");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al ejecutar el SP: {ex.Message}");
        }
    }
}