using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public interface IPedidoRepositorio
    {

        public Task<string> Post(string xmlDocument, string usuarioId);
        public Task<string> Get(int Id, string usuarioId, string UsuarioEmail);

        public Task<string> Delete(int Id, string usuarioId);


    }
}
