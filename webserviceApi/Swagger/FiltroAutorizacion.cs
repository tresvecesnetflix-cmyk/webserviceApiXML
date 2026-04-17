using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace webserviceApi.Swagger 
{
    public class FiltroAutorizacion: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //ESTAMOS verificando la descripcion de los endpoint que lo que tenga AuthorizeAttribute, esta validacion verifica si nos los tiene !
            if (!context.ApiDescription.ActionDescriptor
                .EndpointMetadata.OfType<AuthorizeAttribute>().Any())
            {
                return;
            }//Verificamos si existe un AlloAnonymousAttribute, entonces obviamos lo que vamos hacer de siguente.
            if (context.ApiDescription.ActionDescriptor
               .EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            //Segundo, indicamos la configuracion de JWT que hicimos.
            operation.Security = new List<OpenApiSecurityRequirement>{
                new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference
                            {
                                Type= ReferenceType.SecurityScheme,
                                Id="Bearer"

                            }
                        },
                        new string[] {}
                    }

                }
            };
        }
    }
}

