using webserviceApi.DTOs;
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

        public Task<List<CategoriaResponse>> ListaCategoria()
        {
            return categoriaRepositorio.ListaCategoria();
        }

        public Task<CategoriaResponse> GetById(int Id)
        {
            return categoriaRepositorio.GetById(Id);

        }

        public async Task<int> Post(CategoriaRequest model)
        {

            return await categoriaRepositorio.Post(model);
        }

        public Task<int> Put(CategoriaRequest model)
        {

            return categoriaRepositorio.Put(model);
        }

        public  Task<string> delete(int Id)
        {

            return categoriaRepositorio.delete(Id); 
        }

        public Task<int> PostFoto(CategoriaFotoDTO model,string urlFoto)
        {

            return categoriaRepositorio.PostFoto(model,urlFoto);
        }

    }
}
