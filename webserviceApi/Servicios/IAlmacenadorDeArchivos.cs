namespace webserviceApi.Servicios
{
    public interface IAlmacenadorDeArchivos
    {

        //metodo para borrar una imagen. esto es util para cuando borremos una tarea, asi borrar tambien la tarea
        //ruta, es la URL de la imagen, contenedor; es la carpeta ya que podemos tener varias carpetas para multiples archivos.
        Task Borrar(string? ruta, string contenedor);
        //este metodo de almacenar devuelve un string por que devuelve la URL en la que se guarde la imagen.pero esto sirve para guardar PDF, EXEL, lo que sea
        //contenedor que es la carpeta donde se guarda el archivo 
        Task<string> Almacenar(string contenedor, IFormFile archivo);

        //Metodo, de editar, en la cual vamos a borrar la imagen anterior y guardar una nueva.
        //ruta con Null, por que el campo foto no es requerido.
        async Task<string> Editar(string? ruta, string contenedor, IFormFile archivo)
        {
            //borrar la anterior
            await Borrar(ruta, contenedor);
            //almacenar la nueva.
            return await Almacenar(contenedor, archivo);
        }
    }
}
