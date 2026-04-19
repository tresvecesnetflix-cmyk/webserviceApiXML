namespace webserviceApi.Repositorios
{
    public interface IArticuloRespositorio
    {
        public Task<string> GetAll();
        public Task<string> GetById(int Id);

        public Task<int> Post(string xmlArticulo);

        public Task<string> Delete(int Id);

        public Task<int> PostFoto(string xmlArticulo);
    }
}
