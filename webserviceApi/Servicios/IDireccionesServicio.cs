namespace webserviceApi.Servicios
{
    public interface IDireccionesServicio
    {
        public Task<string> GetAll();
        public Task<string> GetById(int Id,string UsuarioId);

        public Task<string> Delete(int Id, string usuarioId);

        public Task<int> PostById(string Direccion, string usuarioId);

        public Task<int> Post(string Direccion);

        public Task<string> Put(string xmlDocument, string usuarioId);


    }
}
