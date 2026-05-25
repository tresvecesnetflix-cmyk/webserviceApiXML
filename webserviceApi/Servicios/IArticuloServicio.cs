using webserviceApi.DTOs;

namespace webserviceApi.Servicios
{
    public interface IArticuloServicio
    {
        public Task<List<ArticuloResponse>> ObtenerTodos();

        public Task<ArticuloResponse> GetById(int Id);

        public Task<int> Post(ArticuloRequest model);

        public Task<string> Delete(int Id);

        public Task<int> PostFoto(ArticuloRequest model);
    }
}
