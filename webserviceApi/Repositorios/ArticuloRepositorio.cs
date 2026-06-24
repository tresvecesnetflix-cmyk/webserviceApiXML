using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using webserviceApi.DTOs;
using webserviceApi.Servicios.Externos;

namespace webserviceApi.Repositorios
{
    public class ArticuloRepositorio: IArticuloRespositorio
    {
        private readonly string _connectionString;
        private readonly IAlmacenadorDeArchivos almacenadorDeArchivos;

        public ArticuloRepositorio(IConfiguration configuration, IAlmacenadorDeArchivos almacenadorDeArchivos)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString")!;
            this.almacenadorDeArchivos = almacenadorDeArchivos;
        }

        public async Task<List<ArticuloResponse>> GetAll()
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            var xml = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetAllArticulos]",
                commandType: CommandType.StoredProcedure);

            var DOC = XDocument.Parse(xml);

            var itemxml = DOC.Descendants("Articulo").ToList();

            var lista = new List<ArticuloResponse>();

            foreach(var item in itemxml)
            {
                var art = new ArticuloResponse
                {
                    Id=(int)item.Element("ArticuloId"),
                    Nombre = (string)item.Element("Nombre"),
                    Descripcion = (string)item.Element("Descripcion"),
                    Precio = (decimal)item.Element("Precio"),
                    CategoriaId = (int)item.Element("CategoriaId"),
                    Sotck = (int)item.Element("sotck"),
                    ColoresDisponibles = (string)item.Element("ColoresDisponibles"),
                    TallasDisponbles = (string)item.Element("TallasDisponbles"),

                };

                lista.Add(art);

            }

            return lista.ToList() ?? new List<ArticuloResponse>();
        }

        public async Task<ArticuloResponse> GetById(int Id)
        {
            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            var xmlString = $@"<Articulos>
                <Articulo>
                <Id>{Id}</Id>
                </Articulo>
                </Articulos>";
            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[sp_GetArituloById]",
                new { xmlArticulo = xmlString }, commandType: CommandType.StoredProcedure);


            var doc = XDocument.Parse(xmlResult);

            var item = doc.Descendants("Articulo").FirstOrDefault();

            var art = new ArticuloResponse
            {
                Id = (int)item.Element("Id"),
                Nombre = (string)item.Element("Nombre"),
                Descripcion = (string)item.Element("Descripcion"),
                Precio = (decimal)item.Element("Precio"),
                CategoriaId = (int)item.Element("CategoriaId"),
                Sotck = (int)item.Element("sotck"),
                ColoresDisponibles = (string)item.Element("ColoresDisponibles"),
                TallasDisponbles = (string)item.Element("TallasDisponbles"),
                Foto = (string)item.Element("Foto")

            };


            return art ?? new ArticuloResponse();
        }

        public async Task<int> Post(ArticuloRequest model)
        {
            var xmlString = $@"
    <Articulos>
        <Articulo>
            <Nombre>{model.Nombre}</Nombre>
            <Descripcion>{model.Descripcion}</Descripcion>
            <Precio>{model.Precio}</Precio>
            <CategoriaId>{model.CategoriaId}</CategoriaId>
            <Sotck>{model.Sotck}</Sotck>
            <ColoresDisponibles>{model.ColoresDisponibles}</ColoresDisponibles>
            <TallasDisponbles>{model.TallasDisponbles}</TallasDisponbles>
        </Articulo>
    </Articulos>";



            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].sp_postArticulo",
                   new { xmlArticulo= xmlString }, commandType: CommandType.StoredProcedure);
            return xmlResult;
        }

        public async Task<string> Delete(int Id)
        {
           using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();
            var xmlString = $@"<Articulos>
                <Articulo>
                <Id>{Id}</Id>
                </Articulo>
                </Articulos>";
            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].spu_DeleteArticulo",
                   new { xmlArticulo = xmlString }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;
        }

        public async Task<int> PostFoto(ArticuloRequest model)
        {


            string? urlFoto = null;

            if (model.Foto!= null)
            {
                urlFoto = await almacenadorDeArchivos.Almacenar("articulos",model.Foto);
            }


    
            var xmlArticulo= $@"
    <Articulos>
        <Articulo>
            <Nombre>{model.Nombre}</Nombre>
            <Descripcion>{model.Descripcion}</Descripcion>
            <Precio>{model.Precio}</Precio>
            <CategoriaId>{model.CategoriaId}</CategoriaId>
            <Sotck>{model.Sotck}</Sotck>
            <ColoresDisponibles>{model.ColoresDisponibles}</ColoresDisponibles>
            <TallasDisponbles>{model.TallasDisponbles}</TallasDisponbles>
            <Foto>{urlFoto}</Foto>
        </Articulo>
    </Articulos>";

            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].sp_postFotoArticulo",
                   new { xmlArticulo }, commandType: CommandType.StoredProcedure);


            return xmlResult;
        }
    }
}
