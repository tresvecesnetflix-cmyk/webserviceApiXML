using webserviceApi.Repositorios;

namespace webserviceApi.Servicios
{
    public class ArticuloServicios:IArticuloServicio
    {
        private readonly IArticuloRespositorio articuloRespositorio;

        public ArticuloServicios(IArticuloRespositorio articuloRespositorio)
        {
            this.articuloRespositorio = articuloRespositorio;
        }

        public async Task<string> ObtenerTodos()
        {
            return await articuloRespositorio.GetAll();
        }

        public async Task<string> ObtenerPorId(int Id)
        {
            return await articuloRespositorio.GetById(Id);
        }

         public async Task<int> Post(string xmlArticulo)
        {
            return await articuloRespositorio.Post(xmlArticulo);
        }

        public async Task<string> Delete(int Id)
        {
            return await articuloRespositorio.Delete(Id);
        }

        public async Task<int> PostFoto(string xmlArticulo)
        {
            return await articuloRespositorio.PostFoto(xmlArticulo);
        }   
    }
}
