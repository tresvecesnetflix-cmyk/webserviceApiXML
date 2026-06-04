using webserviceApi.DTOs;

namespace webserviceApi.Servicios
{
    public interface IDireccionesServicio
    {
        public Task<List<DireccionesResponse>> GetAll();
        public Task<DireccionesResponse> GetById(int Id,string UsuarioId);

        public Task<int> Delete(int Id, string usuarioId);

        public Task<int> PostById(DireccionesRequest model, string usuarioId);

        public Task<int> Post(DireccionesRequest model );

        public Task<int> Put(DireccionesRequest model, string usuarioId);


    }
}
