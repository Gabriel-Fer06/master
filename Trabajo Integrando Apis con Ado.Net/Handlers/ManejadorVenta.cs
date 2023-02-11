using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionarProductosWebApi
{
    internal class ManejadorVenta:Manejador
    {
        
        public static List<Venta> ObtenerVentasUsuario(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Venta.* FROM Venta\r\n  INNER JOIN Usuario\r\n  ON Venta.IdUsuario = Usuario.Id\r\n  WHERE Venta.IdUsuario = @idUsuario", conn);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("ID DE LA VENTA:");
                    while (reader.Read())
                    {
                        Venta productoTemporal = new Venta();
                        productoTemporal.Id = reader.GetInt64(0);
                        productoTemporal.Comentarios = reader.GetString(1);
                        productoTemporal.IdUsuario = reader.GetInt64(2);
                        ventas.Add(productoTemporal);
                    }
                    return ventas;
                }
                else Console.WriteLine("Error, no se pudo encontrar la lista de ventas");
            }
            return null;
        }


        public static long InsertarVenta(Venta venta)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario)\r\n  VALUES (@comentarios, @idUsuario); SELECT @@IDENTITY", conn);
                comando.Parameters.AddWithValue("@comentarios", venta.Comentarios);
                comando.Parameters.AddWithValue("@idUsuario", venta.IdUsuario);
                conn.Open();

                return Convert.ToInt64(comando.ExecuteScalar());
            }

        }

        public static void CargarVenta(long idUsuario, List<Producto> productosVendidos)
        {
            Venta venta = new Venta();
            venta.Comentarios = $"Realizada por usuario {idUsuario}";
            venta.IdUsuario = idUsuario;
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                foreach (Producto item in productosVendidos)
                {
                    ProductoVendido productoVendido = new ProductoVendido();
                    productoVendido.Stock = item.Stock;
                    productoVendido.IdProducto = item.Id;
                    productoVendido.IdVenta = InsertarVenta(venta);
                    ManejadorProductosVendidos.InsertarProductoVendido(productoVendido);
                }

            }

        }

    }
}
