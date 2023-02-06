namespace Clase_13
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //TraerUsuario
            Console.WriteLine("\nTraerUsuario");
            Usuario usuario = ManejadorUsuario.ObtenerUsuario(1);
            Console.WriteLine(usuario.NombreUsuario);

            //Traer Productos cargado por usuario
            Console.WriteLine("\nTraer Productos cargado por usuario");
            List<Producto> productos = ManejadorProducto.ObtenerProductos(1);
            foreach (var item in productos)
            {
                Console.WriteLine(item.Descripciones);
            }

            //Traer producto vendido segun el idUsuario
            Console.WriteLine("\nTraer producto vendido segun el idUsuario");
            List<Producto> productoVendido = ManejadorProductosVendidos.TraerProductosVendidos(3);
            foreach (Producto item in productoVendido)
            {
                Console.WriteLine(item.Descripciones);
            }

            //Traer producto 
            Console.WriteLine("\nTraer producto");
            Producto producto = ManejadorProducto.ObtenerProducto(7);
            Console.WriteLine(producto.Descripciones);

            //Traer ventas segun el idUsuario
            Console.WriteLine("\nTraer ventas segun idUsuario");
            List<Venta> ventas = ManejadorVenta.ObtenerVentasUsuario(1);
            foreach (Venta item in ventas)
            {
                Console.WriteLine("{0}",item.Id);
            }

            //Iniciar Sesión
            Console.WriteLine("\nINICIAR SESION");
            Usuario usuario1 = ManejadorUsuario.IniciarSesion("eperez", "Ernesto123");
        }
    }
}