using Microsoft.AspNetCore.Mvc;
using webserviceApi.DTOs;
using webserviceApi.Servicios;

namespace webserviceApi.Controllers
{

    [ApiController]
    [Route("api/pagos")]
    public class PagosController:ControllerBase
    {
        //private readonly IConfiguration configuration;
        //private readonly WompiService wompiService;

        //public PagosController(IConfiguration configuration, WompiService wompiService)
        //{
        //    this.configuration = configuration;
        //    this.wompiService = wompiService;
        //}

        //[HttpPost("crear-transaccion-bitcoin")]
        //public async Task<ActionResult<string>> CrearTransaccionBTC([FromBody] BitcointTDO bitcointTDO)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return ValidationProblem();
        //    }


        //    try
        //    {

        //        var resultado = await wompiService.TransacctionBTC(bitcointTDO);
        //        return Ok(resultado);
        //    }catch (Exception ex)
        //    {

        //        return BadRequest(new {mensaje= ex.Message });
        //    }

        //}
    }
}
