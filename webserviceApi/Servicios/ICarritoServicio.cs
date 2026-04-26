using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Identity.Client;

namespace webserviceApi.Servicios
{
    public interface ICarritoServicio
    {
        public Task<string> Post(string xmlString, string Id);

        public Task<string> GetById(int Id, string IdUsuario);

        public Task<string> Delete(int id, string UsuarioId);

        public Task<string> Put(string Carrito, string Id); 

    }
}
