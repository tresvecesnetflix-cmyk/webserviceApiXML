using Microsoft.AspNetCore.Identity;

namespace webserviceApi.Servicios
{
    public interface IServicioUsuarios
    {
        Task<IdentityUser?> ObtenerUsuario();
    }
}