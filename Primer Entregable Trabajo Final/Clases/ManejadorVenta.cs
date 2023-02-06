using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase_13
{
    internal class ManejadorVenta:Manejador
    {
        
        public static List<Venta> ObtenerVentasUsuario(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Venta.Id FROM Venta\r\n  INNER JOIN Usuario\r\n  ON Venta.IdUsuario = Usuario.Id\r\n  WHERE Venta.IdUsuario = @idUsuario", conn);
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
                        ventas.Add(productoTemporal);
                    }
                }
                else Console.WriteLine("Error, no se pudo encontrar la lista de ventas");
            }
            return ventas;
        }

    }
}
