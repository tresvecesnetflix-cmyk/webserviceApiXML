using Microsoft.AspNetCore.Identity;

namespace webserviceApi.Servicios.Externos
{
    public interface IServicioUsuarios
    {
        Task<IdentityUser?> ObtenerUsuario();
    }
}