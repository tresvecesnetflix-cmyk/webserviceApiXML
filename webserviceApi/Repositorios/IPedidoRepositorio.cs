using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public interface IPedidoRepositorio
    {

        public Task<List<int>> Post(PedidosRequest model, string usuarioId);
        public Task<PedidosResponse> Get(int Id, string usuarioId, string UsuarioEmail);

        public Task<string> Delete(int Id, string usuarioId);


    }
}
