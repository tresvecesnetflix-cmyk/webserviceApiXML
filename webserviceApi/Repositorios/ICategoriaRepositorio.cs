using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public interface ICategoriaRepositorio
    {
        public Task<List<CategoriaResponse>> ListaCategoria();

        public Task<CategoriaResponse> GetById(int Id);

        public Task<int> Post(CategoriaRequest model);

        public Task<int> Put(CategoriaRequest model);

        public Task<int> delete(int Id);

        public Task<int> PostFoto(CategoriaRequest model);


    }
}
