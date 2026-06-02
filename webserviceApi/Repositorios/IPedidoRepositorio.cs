using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public interface IPedidoRepositorio
    {

        public Task<List<int>> Post(PedidosRequest model, string usuarioId);
        public Task<PedidosResponse> Get(int Id, string usuarioId );

        public Task<int> Delete(int Id, string usuarioId);


    }
}
