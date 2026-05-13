using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public interface ICarritoRepositorio
    {
        public Task<string> Post(CarritoRequest model, string Id);

        public Task<CarritoResponse> GetById( int Id, string IdUsuario);

        public Task<string> Delete(int id, string UsuarioId);

        public Task<string> Put(CarritoRequest Carrito, string Id);


    }
}
