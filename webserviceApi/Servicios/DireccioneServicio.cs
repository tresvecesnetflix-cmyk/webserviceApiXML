using webserviceApi.DTOs;
using webserviceApi.Repositorios;

namespace webserviceApi.Servicios
{
    public class DireccioneServicio: IDireccionesServicio
    {
        private readonly IDireccionesRepositorio direccionesRepositorio;

        public DireccioneServicio(IDireccionesRepositorio direccionesRepositorio)
        {
            this.direccionesRepositorio = direccionesRepositorio;
        }
        public async Task<List<DireccionesResponse>> GetAll()
        {

            return await direccionesRepositorio.GetAll();
        }

        public async Task<DireccionesResponse> GetById(int Id,string UsuarioId)
        {

            return await direccionesRepositorio.GetById(Id, UsuarioId);
        }

        public async Task<string> Delete(int Id, string usuarioId)
        {
            return await direccionesRepositorio.Delete(Id, usuarioId);

        }
        public async Task<int> PostById(DireccionesRequest model, string usuarioId)
        {

            return await direccionesRepositorio.PostById(model, usuarioId);
        }

        public async Task<int> Post(DireccionesRequest model)
        {
            return await direccionesRepositorio.Post( model);
        }


        public async Task<int> Put(DireccionesRequest model, string usuarioId)
        {

            return await direccionesRepositorio.Put(model, usuarioId);    
        }

    }
}
