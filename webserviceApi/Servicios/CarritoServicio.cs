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
        public async  Task<string>Post(string xmlString, string Id)
        {
            return await carritoRepositorio.Post( xmlString,  Id);

        }

        public async Task<string> GetById(int Id, string IdUsuario)
        {
            return await carritoRepositorio.GetById(Id, IdUsuario);
        }
        public async Task<string> Delete(int id, string UsuarioId)
        {
            return await carritoRepositorio.Delete(id, UsuarioId);
        }

        public async Task<string> Put(string Carrito, string Id)
        {

            return await carritoRepositorio.Put(Carrito, Id);
        }



    }
}
