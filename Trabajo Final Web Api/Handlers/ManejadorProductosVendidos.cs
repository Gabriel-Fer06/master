using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionarProductosWebApi
{
    internal class ManejadorProductosVendidos:Manejador
    {

        //Traer productos vendidos
        public static List<Producto> TraerProductosVendidos(long idUsuario)
        {
            List<Producto> productoVendido = new List<Producto>();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT ProductoVendido.IdProducto FROM ProductoVendido\r\n  INNER JOIN Venta\r\n  ON Venta.Id = ProductoVendido.IdVenta\r\n  WHERE Venta.IdUsuario = @id", conn);
                comando.Parameters.AddWithValue("@id", idUsuario);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto productoTemporal = ManejadorProducto.ObtenerProducto(reader.GetInt64(0));

                        productoVendido.Add(productoTemporal);
                    }
                    return productoVendido;
                }
                else Console.WriteLine("Error, no se pudo encontrar la lista de productos");
            }
            return null;
        } 

        public static int EliminarProductoVendido(long id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("  DELETE FROM ProductoVendido\r\n  WHERE ProductoVendido.IdProducto = @id", conn);
                comando.Parameters.AddWithValue("@id", id);
                conn.Open();

                return comando.ExecuteNonQuery(); 
            }
        }

        public static int InsertarProductoVendido(ProductoVendido productoVendido)
        {
            ManejadorProducto.ModificarStock(productoVendido.IdProducto, productoVendido.Stock);

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta)\r\n  VALUES (@stockVendido, @idProducto, @idVenta)", conn);
                comando.Parameters.AddWithValue("@stockVendido", productoVendido.Stock);
                comando.Parameters.AddWithValue("@idProducto", productoVendido.IdProducto);
                comando.Parameters.AddWithValue("@idVenta", productoVendido.IdVenta);
                conn.Open();

                return comando.ExecuteNonQuery();
            }
        }

        public static int EliminarProductoVendidoUsuario(long idUsuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("DELETE e FROM ProductoVendido e\r\n  INNER JOIN Producto a\r\n  on a.Id = e.IdProducto\r\n  WHERE a.IdUsuario = @idUsuario", conn);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                conn.Open();

                return comando.ExecuteNonQuery();
            }
        }

        public static int EliminarProductoVendidoVenta(long idUsuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(" DELETE a FROM ProductoVendido a\r\n  INNER JOIN Venta b\r\n  on a.IdVenta = b.Id\r\n  WHERE b.IdUsuario = @idUsuario", conn);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                conn.Open();

                return comando.ExecuteNonQuery();
            }
        }
    }
}
