using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionarProductosWebApi
{
    internal class ManejadorProducto:Manejador
    {
        
        
        //Traer Productos cargador por usuario
        public static List<Producto> ObtenerProductos(long idUsuario)
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Producto.* FROM Producto\r\n  INNER JOIN Usuario\r\n  ON Producto.IdUsuario = Usuario.Id\r\n  WHERE Usuario.Id = @idUsuario", conn);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto productoTemporal = new Producto();
                        productoTemporal.Descripciones = reader.GetString(0);
                        productoTemporal.Costo = reader.GetDecimal(1);
                        productoTemporal.PrecioVenta = reader.GetDecimal(2);
                        productoTemporal.Stock = reader.GetInt32(3);
                        productoTemporal.IdUsuario = reader.GetInt64(4);

                        productos.Add(productoTemporal);
                    }
                    return productos;
                }
                else Console.WriteLine("Error, no se pudo encontrar la lista de productos");
            }
            return null;
        }

        //Traer producto
        public static Producto ObtenerProducto(long idProducto)
        {
            Producto producto = new Producto();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Producto.* FROM Producto\r\n  WHERE Producto.Id = @idProducto", conn);
                comando.Parameters.AddWithValue("@idProducto", idProducto);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);
                    return producto;
                }
                else Console.WriteLine("Error, no se pudo encontrar el producto buscado");
            }
            return null;
        }

        public static int CrearProducto(Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(" INSERT INTO Producto(Descripciones, Costo, PrecioVenta,Stock,IdUsuario)\r\n  VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario)", conn);
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                conn.Open();

                return comando.ExecuteNonQuery();
            }
        }

        public static int ModificarProducto(Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("  UPDATE Producto\r\n  SET \r\n  Descripciones = @descripciones,\r\n  Costo = @costo,\r\n  PrecioVenta = @precioVenta,\r\n  Stock = @stock,\r\n  IdUsuario = @idUsuario\r\n  WHERE Producto.Id = @id", conn);
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                comando.Parameters.AddWithValue("@id", producto.Id);
                conn.Open();

                return comando.ExecuteNonQuery();

            }
        }

        public static int EliminarProducto(long id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                ManejadorProductosVendidos.EliminarProductoVendido(id);
                SqlCommand comando = new SqlCommand("  DELETE FROM Producto\r\n  WHERE Producto.Id = @id", conn);
                comando.Parameters.AddWithValue("@id", id);
                conn.Open();

                return comando.ExecuteNonQuery();
            }
        }

        public static int ModificarStock(long id, int stockVendido)
        {
            Producto producto = ObtenerProducto(id);
            producto.Stock -= stockVendido;
            return ModificarProducto(producto);
        }
    }
}
