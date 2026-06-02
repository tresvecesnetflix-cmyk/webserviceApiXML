using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using webserviceApi.DTOs;
using webserviceApi.Repositorios;

namespace webserviceApi.Servicios
{
    public class PedidoServicio: IPedidoServicio
    {
        private readonly IPedidoRepositorio pedidoRepositorio;

        public PedidoServicio(IPedidoRepositorio pedidoRepositorio)
        {
            this.pedidoRepositorio = pedidoRepositorio;
        }


        public async Task<List<int>> Post(PedidosRequest model, string usuarioId)
        {

            var respuesta = pedidoRepositorio.Post(model, usuarioId);

            return await respuesta;
        }

        public async Task<PedidosResponse> Get(int Id, string usuarioId)
        {
            var respuesta = pedidoRepositorio.Get(Id, usuarioId);

            return await respuesta;

        }

        public async Task<string> Delete(int Id, string usuarioId)
        {
            var respuesta = pedidoRepositorio.Delete(Id, usuarioId);

            return await respuesta;

        }

    }
}
