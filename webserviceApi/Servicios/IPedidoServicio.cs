using webserviceApi.DTOs;

namespace webserviceApi.Servicios
{
    public interface IPedidoServicio
    {

        public Task<string> Post(string xmlDocument, string usuarioId);
        public Task<string> Get(int Id, string usuarioId, string UsuarioEmail);

        public Task<string> Delete(int Id, string usuarioId);

    }
}
