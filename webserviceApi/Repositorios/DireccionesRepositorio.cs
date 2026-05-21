using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System.Data;
using System.Xml.Linq;
using webserviceApi.DTOs;

namespace webserviceApi.Repositorios
{
    public class DireccionesRepositorio:IDireccionesRepositorio
    {
        private readonly string _configuration;
        public DireccionesRepositorio(IConfiguration configuration)          
        {
            _configuration = configuration.GetConnectionString("ConnectionString")!;
        }

        public async Task<List<DireccionesResponse>> GetAll()
        {

            var con = new SqlConnection(_configuration);

            await con.OpenAsync();


                var xmlResult = await con.ExecuteScalarAsync<string>("[dbo].[spu_GetAllDirecciones]",
                commandType: CommandType.StoredProcedure);


            var doc = XDocument.Parse(xmlResult);

            var listaDirecciones = doc.Descendants("Direccion").ToList();

            var lista = new List<DireccionesResponse>();

            foreach(var dir in listaDirecciones)
            {
               var direcciones = new DireccionesResponse
                {
                   Nombres = (string)dir.Element("Nombres"),
                   Apellidos = (string)dir.Element("Apellidos"),
                   Correo = (string)dir.Element("Correo"),
                   Telefono = (string)dir.Element("Telefono"),
                   TipoDocumento = (string)dir.Element("TipoDocumento"),
                   NumeroDocumneto = (string)dir.Element("NumeroDocumneto"),
                   Pais = (string)dir.Element("Pais"),
                   Departamento = (string)dir.Element("Departamento"),
                   CiudadMunicipio = (string)dir.Element("CiudadMunicipio"),
                   Colonia = (string)dir.Element("Colonia"),
                   Direccions = (string)dir.Element("Direccions"),

               };

                lista.Add(direcciones);
            }

            return lista ?? new List<DireccionesResponse>();
        }

        public async Task<DireccionesResponse> GetById(int Id, string UsuarioId)
        {

            var con = new SqlConnection(_configuration);

            var xmlString = $@"<Direcciones>
                              <Direccion>
                              <Id>{Id}</Id>
                              </Direccion>
                              </Direcciones>";

            await con.OpenAsync();


            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_GetDireccionById]",
                                                    new { xmlDirecciones = xmlString, usuarioId=UsuarioId }, commandType: CommandType.StoredProcedure);

            var doc = XDocument.Parse(xmlResult);

            var dir = doc.Descendants("Direccion").FirstOrDefault();

            var direccion = new DireccionesResponse();

            direccion.Nombres = (string)dir.Element("Nombres");
            direccion.Apellidos = (string)dir.Element("Apellidos");
            direccion.Correo = (string)dir.Element("Correo");
            direccion.Telefono = (string)dir.Element("Telefono");
            direccion.TipoDocumento = (string)dir.Element("TipoDocumento");
            direccion.NumeroDocumneto = (string)dir.Element("NumeroDocumneto");
            direccion.Pais = (string)dir.Element("Pais");
            direccion.Departamento = (string)dir.Element("Departamento");
            direccion.CiudadMunicipio = (string)dir.Element("CiudadMunicipio");
            direccion.Colonia = (string)dir.Element("Colonia");
            direccion.Direccions = (string)dir.Element("Direccions");


            return direccion ?? new DireccionesResponse();
        }

        public async Task<string> Delete(int Id, string usuarioId)
        {
            var con = new SqlConnection(_configuration);

            var xmlString = $@"<Direcciones>
                              <Direccion>
                              <Id>{Id}</Id>
                              </Direccion>
                              </Direcciones>";

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_DeleteDirecciones]",
                              new { xmlDirecciones = xmlString, UsuarioId= usuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult ?? string.Empty;

        }


        public async Task<int> PostById(DireccionesRequest model, string usuarioId)
        {

            var Direccion  = $@"<Direcciones>
                              <Direccion>
                              <Correo>{model.Correo}</Correo>
                              <Nombres>{model.Nombres}</Nombres>
                              <Apellidos>{model.Apellidos}</Apellidos>
                              <Telefono>{model.Telefono}</Telefono>
                              <TipoDocumento>{model.TipoDocumento}</TipoDocumento>
                              <NumeroDocumneto>{model.NumeroDocumneto}</NumeroDocumneto>
                              <Pais>{model.Pais}</Pais>
                              <Departamento>{model.Departamento}</Departamento>
                              <CiudadMunicipio>{model.CiudadMunicipio}</CiudadMunicipio>
                              <Colonia>{model.Colonia}</Colonia>
                              <Direccions>{model.Direccions}</Direccions>
                              </Direccion>
                              </Direcciones>";

            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].[spu_postDireccionesUser]",
                       new { xmlDirecciones = Direccion,UsuarioId= usuarioId }, commandType: CommandType.StoredProcedure);

            return xmlResult;

        }

        public async Task<int> Post(DireccionesRequest model)
        {
            var Direccion = $@"<Direcciones>
                              <Direccion>
                              <Correo>{model.Correo}</Correo>
                              <Nombres>{model.Nombres}</Nombres>
                              <Apellidos>{model.Apellidos}</Apellidos>
                              <Telefono>{model.Telefono}</Telefono>
                              <TipoDocumento>{model.TipoDocumento}</TipoDocumento>
                              <NumeroDocumneto>{model.NumeroDocumneto}</NumeroDocumneto>
                              <Pais>{model.Pais}</Pais>
                              <Departamento>{model.Departamento}</Departamento>
                              <CiudadMunicipio>{model.CiudadMunicipio}</CiudadMunicipio>
                              <Colonia>{model.Colonia}</Colonia>
                              <Direccions>{model.Direccions}</Direccions>
                              </Direccion>
                              </Direcciones>";

            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<int>("[dbo].[spu_postDirecciones]",
                       new { xmlDirecciones = Direccion }, commandType: CommandType.StoredProcedure);

            return xmlResult;

        }

        public async Task<int>Put(DireccionesRequest model, string usuarioId)
        {
            var xmlDocument = $@"<Direcciones>
                              <Direccion>
                              <Id>{model.Id}</Id>
                              <Correo>{model.Correo}</Correo>
                              <Nombres>{model.Nombres}</Nombres>
                              <Apellidos>{model.Apellidos}</Apellidos>
                              <Telefono>{model.Telefono}</Telefono>
                              <TipoDocumento>{model.TipoDocumento}</TipoDocumento>
                              <NumeroDocumneto>{model.NumeroDocumneto}</NumeroDocumneto>
                              <Pais>{model.Pais}</Pais>
                              <Departamento>{model.Departamento}</Departamento>
                              <CiudadMunicipio>{model.CiudadMunicipio}</CiudadMunicipio>
                              <Colonia>{model.Colonia}</Colonia>
                              <Direccions>{model.Direccions}</Direccions>
                              </Direccion>
                              </Direcciones>";

            var con = new SqlConnection(_configuration);

            await con.OpenAsync();

            var xmlResult = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spu_PutDireccionesByUser]",
                new { xmlDirecciones = xmlDocument, UsuarioId = usuarioId }, commandType: CommandType.StoredProcedure);

            var doc = XDocument.Parse(xmlResult);

            var resultado = doc.Descendants("Resultado").FirstOrDefault();

            int Id;

            Id = (int)resultado.Element("Id");


            return Id;

        }
    }
}
