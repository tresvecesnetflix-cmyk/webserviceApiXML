namespace webserviceApi.Servicios
{
    public interface ICategoriaServicio
    {
        public Task<string> ListaCategoria();

        public Task<string> GetById(int Id);

        public Task<int> Post(string xmlCategoria);
        public Task<int> Put(string xmlCategoria);

        public Task<string> delete(int Id);

        public Task<string> PostFoto(string xmlCategoria);


    }
}
