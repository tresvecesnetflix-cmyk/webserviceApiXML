namespace webserviceApi.Servicios
{
    public interface IArticuloServicio
    {
        public Task<string> ObtenerTodos(); 

        public Task<string> ObtenerPorId(int Id);

        public Task<int> Post(string xmlArticulo);

        public Task<string> Delete(int Id);

        public Task<int> PostFoto(string xmlArticulo);
    }
}
