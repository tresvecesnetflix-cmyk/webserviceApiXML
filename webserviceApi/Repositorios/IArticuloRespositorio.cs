using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public interface IArticuloRespositorio
    {
        public Task<List<ArticuloResponse>> GetAll();
        public Task<ArticuloResponse> GetById(int Id);

        public Task<int> Post(ArticuloRequest model);

        public Task<string> Delete(int Id);

        public Task<int> PostFoto(ArticuloFotoDTO model);
    }
}
