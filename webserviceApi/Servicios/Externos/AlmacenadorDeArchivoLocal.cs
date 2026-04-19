namespace webserviceApi.Servicios.Externos
{
    public class AlmacenadorDeArchivoLocal : IAlmacenadorDeArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        //vamos inyectar dos servivicios ,IWebHostEnvironment; que me va permitir acceder a una carpeta que tendremos en el servidor para guardar las imagenes 
        //IHttpContextAccessor para acceder al contexto http
        public AlmacenadorDeArchivoLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Almacenar(string contenedor, IFormFile archivo)
        {
            //obtenemos la extension del archivo .PGN .JPG Etc
            var extension = Path.GetExtension(archivo.FileName);
            //inventamos un nuevo nombre con la extension
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            //necesitamos el folder donde vamos a guardar el archivo
            string folder = Path.Combine(env.WebRootPath, contenedor);

            //si no exite el directorio nombrado en forlderlo creamos
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            //creamos la ruta que va ser la combinacion del folder y el nombre del archiv
            string ruta = Path.Combine(folder, nombreArchivo);

            //guardamos
            using (var ms = new MemoryStream())
            {
                //estamos pasando el archivo asi memoria string
                await archivo.CopyToAsync(ms);
                //convertimos el memory string en una representacion en arreglo de Bytes
                var contenido = ms.ToArray();
                //escribir todos los bytes de manera asincrona
                await File.WriteAllBytesAsync(ruta, contenido);
            }

            //necesito retornar de este metodo la URL que representa de archivo
            var request = httpContextAccessor.HttpContext!.Request;
            var Url = $"{request.Scheme}://{request.Host}";
            //necesitamos convertir los Slash a los que se usan a URL que son /
            var urlArchivo = Path.Combine(Url, contenedor, nombreArchivo).Replace("\\", "/");

            return urlArchivo;
        }

        public Task Borrar(string? ruta, string contenedor)
        {
            //si es null
            if (string.IsNullOrEmpty(ruta))
            { 
                return Task.CompletedTask;
            }

            //obtenemos el nombre del archivo apartir de la ruta
            var nombrearchivo = Path.GetFileName(ruta);
            //WebRootPath; donde se guarda el archivo
            var directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombrearchivo);
            //si existe el archivo lo borramos
            if (File.Exists(directorioArchivo))
            {
                File.Delete(directorioArchivo);
            }

            return Task.CompletedTask;

        }
    }
}
