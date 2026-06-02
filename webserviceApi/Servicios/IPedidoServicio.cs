using webserviceApi.DTOs;

namespace webserviceApi.Servicios
{
    public interface IPedidoServicio
    {

        public Task<List<int>> Post(PedidosRequest model, string usuarioId);
        public Task<PedidosResponse> Get(int Id, string usuarioId);

        public Task<string> Delete(int Id, string usuarioId);

    }
}
