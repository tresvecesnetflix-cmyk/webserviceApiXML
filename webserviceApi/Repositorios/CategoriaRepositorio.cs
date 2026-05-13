using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Xml.Schema;
using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public class CategoriaRepositorio:ICategoriaRepositorio
    {
        private readonly string _con;
        public CategoriaRepositorio(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("ConnectionString")!;  
        }

        public async Task<List<CategoriaResponse>> ListaCategoria()
        {
           using var con =  new SqlConnection(_con);

            con.Open();

          
                var xml = await con.QueryFirstOrDefaultAsync<string>(
                    "[dbo].[SPU_GetAllCategoria]", commandType: CommandType.StoredProcedure);
            
            var doc = XDocument.Parse(xml);

            var listaCat = doc.Descendants("Categoria").ToList();

                var cat = new List<CategoriaResponse>();

            foreach (var item in listaCat)
            {
                cat.Add( new CategoriaResponse
                {
                    Id = (int)item.Element("categoriaId"),
                    Titulo = (string)item.Element("Titulo"),
                    Descripcion = (string)item.Element("Descripcion"),
                    Foto = (string)item.Element("Foto")
                });

            }
            return cat ?? new List<CategoriaResponse>();
        }

        public async Task<CategoriaResponse> GetById(int Id)
        {
            using var con = new SqlConnection(_con);

           await con.OpenAsync();

            var xmlString = $@"<Categorias>
                <Categoria>
                <Id>{Id}</Id>
                </Categoria>
                </Categorias>";

    
                var xmlRespuesta = await con.QueryFirstOrDefaultAsync<string>("[dbo].[sp_GetCategoriaByid]", new { xmlCategoria = xmlString }, commandType: CommandType.StoredProcedure);
           
            var doc = XDocument.Parse(xmlRespuesta);

            //validamos si la respuesta viene con error con el nodo 'Resultado' 

            var error= doc.Descendants("Resultado").FirstOrDefault();

            if (error != null)
            {
                throw new Exception(error.Value);
            }


            //Validamos la respuesta exitosa con el Nodo 'Categoria'
            var cat = doc.Descendants("Categoria").FirstOrDefault();

            var response = new CategoriaResponse();

            response.Id = (int)cat.Element("Id");
            response.Titulo = (string)cat.Element("Titulo");
            response.Descripcion = (string)cat.Element("Descripcion");
            response.Foto = (string)cat.Element("Foto");

            return response ?? new CategoriaResponse();
        }

        public async Task<int> Post(CategoriaRequest model)
        {
            var xmlCategoria = $@"
                <Categorias>
                <Categoria>
                <Titulo>{model.Titulo}</Titulo>
                <Descripcion>{model.Descripcion}</Descripcion>
                </Categoria>
                </Categorias>
               ";

            using var con = new SqlConnection(_con);

             await con.OpenAsync();

            var xmlRespuesta = await con.QueryFirstOrDefaultAsync<int>("sp_InsertarCategoria",new {xmlCategoria},commandType:CommandType.StoredProcedure);


            return xmlRespuesta;
        
        }

        public async Task<int> Put(CategoriaRequest model)
        {

            var xmlCategoria = $@"
                <Categorias>
                <Categoria>
                <Id>{model.Id}</Id>
                <Titulo>{model.Titulo}</Titulo>
                <Descripcion>{model.Descripcion}</Descripcion>
                </Categoria>
                </Categorias>
               ";
            using var con = new SqlConnection(_con);

            await con.OpenAsync();

                var xmlRespuesta = await con.QueryFirstOrDefaultAsync<int>("sp_updateCategoria", new { xmlCategoria }, commandType: CommandType.StoredProcedure);
               
            
            return xmlRespuesta;
          
        }

        public async Task<string> delete(int Id)
        {
            using var con=  new SqlConnection(_con);
          var xmlString=$@"
                <Categoria>
                <Id>{Id}</Id>
                </Categoria>
               ";

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_DeleteCategoria]", new { xmlCategoria = xmlString }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }

        public async Task<int> PostFoto(CategoriaFotoDTO model, string UrlFoto)
        {
            var xmlCategoria = $@"
                <Categorias>
                <Categoria>
                <Titulo>{model.Titulo}</Titulo>
                <Descripcion>{model.Descripcion}</Descripcion>
                <Foto>{UrlFoto}</Foto>
                </Categoria>
                </Categorias>
               ";

            using var con = new SqlConnection(_con);

           await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].[spu_PotsCategoriaFoto]", new { xmlCategoria = xmlCategoria }, commandType: CommandType.StoredProcedure);

            return xmlResult;

        }

    }
}
