using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webserviceApi.DTOs;

namespace webserviceApi.Controllers
{

    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController: ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("registro")]

        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registro(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            //instasnciamos el usuario 
            var usuario = new IdentityUser
            {
                UserName = credencialesUsuarioDTO.email,
                Email = credencialesUsuarioDTO.email

            };

            //creamos el usuario y pasamos el usuario y el password

            var resultado = await userManager.CreateAsync(usuario, credencialesUsuarioDTO.password);
            if (resultado.Succeeded)
            {
                return await ConstruiToken(credencialesUsuarioDTO);

            }
            else
            {
                return RetorLoginIncorrecto();

            }

        }
        [HttpPost("Login")]

        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {

            //buscamos el usuario

            var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDTO.email);
            if (usuario is null)
            {
                return RetorLoginIncorrecto();
                
            }

            //verificamos si la contraseña creada es la correcta 


            var resultado = await signInManager.CheckPasswordSignInAsync(usuario, credencialesUsuarioDTO.password, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return await ConstruiToken(credencialesUsuarioDTO);

            }
            else
            {

                return RetorLoginIncorrecto();

            }
        }

        private ActionResult RetorLoginIncorrecto()
        {
            ModelState.AddModelError(string.Empty,"Login Incorrecto");
            return ValidationProblem();


        }

        private async Task<ActionResult<RespuestaAutenticacionDTO>> ConstruiToken(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var claims = new List<Claim>
            {
                new Claim("email", credencialesUsuarioDTO.email),
                new Claim("Lo que yo quiera","cual quier valor")

            };


            //usamos userManager para busac el suario por el Email

            var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDTO.email);

            //optenemos los claims asociados al usuario de la base de datos

            var claimsDB = await userManager.GetClaimsAsync(usuario!);

            //agreamos los claims de la base de atos al listado de claims 

            claims.AddRange(claimsDB);

            //Traemos la llave secreta que se encuentra en una proveedor de configuraciones

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!));



            //credenciales firmadas.
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddYears(1); //<--para que el token tenga un ao de vigencia

            //emisor null, auddiencia null
            var tokenDeSeguridad = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: credenciales);
            //generamos el token

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);
            return new RespuestaAutenticacionDTO()
            {
                Token = token,
                Expiracion = expiracion,
                UsuarioId = usuario!.Id
            };
        }

    }
    }

