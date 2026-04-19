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
    }
}
