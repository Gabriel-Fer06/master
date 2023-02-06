using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase_13
{
    internal class ManejadorProducto:Manejador
    {
        
        
        //Traer Productos cargador por usuario
        public static List<Producto> ObtenerProductos(long idUsuario)
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Producto.Descripciones FROM Producto\r\n  INNER JOIN Usuario\r\n  ON Producto.IdUsuario = Usuario.Id\r\n  WHERE Usuario.Id = @idUsuario", conn);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto productoTemporal = new Producto();
                        productoTemporal.Descripciones = reader.GetString(0);

                        productos.Add(productoTemporal);
                    }
                }
                else Console.WriteLine("Error, no se pudo encontrar la lista de productos");
                return productos;
            }
        }

        //Traer producto
        public static Producto ObtenerProducto(long idProducto)
        {
            Producto producto = new Producto();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Producto.Descripciones FROM Producto\r\n  WHERE Producto.Id = @idProducto", conn);
                comando.Parameters.AddWithValue("@idProducto", idProducto);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    producto.Descripciones = reader.GetString(0);
                }
                else Console.WriteLine("Error, no se pudo encontrar el producto buscado");
                return producto;
            }
        }
    }
}
