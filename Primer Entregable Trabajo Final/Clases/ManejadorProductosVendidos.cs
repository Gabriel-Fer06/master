using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase_13
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
                }
                else Console.WriteLine("Error, no se pudo encontrar la lista de productos");
            }
            return productoVendido;
        }
    }
}
