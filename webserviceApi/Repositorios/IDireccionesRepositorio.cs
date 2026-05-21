using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public interface IDireccionesRepositorio
    {
        public Task<List<DireccionesResponse>> GetAll();
        public  Task<DireccionesResponse> GetById(int Id,string UsuarioId);

        public  Task<string> Delete(int Id, string usuarioId);

        public Task<int> PostById(DireccionesRequest model, string usuarioId);

        public  Task<int> Post(DireccionesRequest model );

        public Task<int> Put(DireccionesRequest model, string usuarioId);


    }
}
