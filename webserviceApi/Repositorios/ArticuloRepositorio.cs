using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
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

        public async Task<string> GetAll()
        {
            using var con = new SqlConnection(_connectionString);

            await con.OpenAsync();

            var xml = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetAllArticulos]",
                commandType: CommandType.StoredProcedure);

            return xml ?? string.Empty;
        }

        public async Task<string> GetById(int Id)
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
            return xmlResult ?? string.Empty;
        }

        public async Task<int> Post(string xmlArticulo)
        {
            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].sp_postArticulo",
                   new { xmlArticulo }, commandType: CommandType.StoredProcedure);
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

        public async Task<int> PostFoto(string xmlArticulo)
        {
            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].sp_postFotoArticulo",
                   new { xmlArticulo }, commandType: CommandType.StoredProcedure);
            return xmlResult;
        }
    }
}
