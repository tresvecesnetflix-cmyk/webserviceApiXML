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

        public Task<string> GetById(int Id)
        {
            return categoriaRepositorio.GetById(Id);

        }

        public Task<int>Post(string xmlCategoria)
        {

            return categoriaRepositorio.Post(xmlCategoria);
        }

        public Task<int> Put(string xmlCategoria)
        {

            return categoriaRepositorio.Put(xmlCategoria);
        }

        public  Task<string> delete(int Id)
        {

            return categoriaRepositorio.delete(Id); 
        }

        public Task<string> PostFoto(string xmlCategoria)
        {

            return categoriaRepositorio.PostFoto(xmlCategoria);
        }

    }
}
