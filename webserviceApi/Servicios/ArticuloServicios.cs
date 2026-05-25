using webserviceApi.DTOs;
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

        public async Task<List<ArticuloResponse>> ObtenerTodos()
        {
            return await articuloRespositorio.GetAll();
        }

        public async Task<ArticuloResponse> GetById(int Id)
        {
            return await articuloRespositorio.GetById(Id);
        }

         public async Task<int> Post(ArticuloRequest model)
        {
            return await articuloRespositorio.Post(model);
        }

        public async Task<string> Delete(int Id)
        {
            return await articuloRespositorio.Delete( Id);
        }

        public async Task<int> PostFoto(ArticuloRequest model)
        {
            return await articuloRespositorio.PostFoto( model);
        }   
    }
}
