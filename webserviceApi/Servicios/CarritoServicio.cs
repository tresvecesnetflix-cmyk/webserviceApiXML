using webserviceApi.DTOs;
using webserviceApi.Repositorios;

namespace webserviceApi.Servicios
{
    public class CarritoServicio: ICarritoServicio
    {
        private readonly ICarritoRepositorio carritoRepositorio;

        public CarritoServicio(ICarritoRepositorio carritoRepositorio)
        {
            this.carritoRepositorio = carritoRepositorio;
        }
        public async Task<string> Post(CarritoRequest model, string Id)
        {
            return await carritoRepositorio.Post( model,  Id);

        }

        public async Task<CarritoResponse> GetById(int Id, string IdUsuario)
        {
            return await carritoRepositorio.GetById(Id, IdUsuario);
        }
        public async Task<string> Delete(int id, string UsuarioId)
        {
            return await carritoRepositorio.Delete(id, UsuarioId);
        }

        public async Task<string> Put(CarritoRequest Carrito, string Id)
        {

            return await carritoRepositorio.Put(Carrito, Id);
        }



    }
}
