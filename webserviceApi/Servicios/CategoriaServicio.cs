using webserviceApi.Repositorios;

namespace webserviceApi.Servicios
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly ICategoriaRepositorio categoriaRepositorio;

        public CategoriaServicio(ICategoriaRepositorio categoriaRepositorio)
        {
            this.categoriaRepositorio = categoriaRepositorio;
        }

        public Task<string> ListaCategoria()
        {
            return categoriaRepositorio.ListaCategoria();
        }
    }
}
