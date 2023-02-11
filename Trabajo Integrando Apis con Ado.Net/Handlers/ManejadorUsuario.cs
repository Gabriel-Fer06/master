using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionarProductosWebApi
{
    internal class ManejadorUsuario:Manejador
    {
        //Traer Usuario
        public static Usuario ObtenerUsuario(long id)
        {
            Usuario usuario = new Usuario();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            { 

                SqlCommand comando = new SqlCommand("  SELECT * FROM Usuario\r\n  WHERE Id = @id", conn);
                comando.Parameters.AddWithValue("@id", id);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    usuario.Id = reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);
                    return usuario;
                }
                else Console.WriteLine("Usuario no encontrado");

            }
            return null;
        }

        //Inciar Sesión
        public static Usuario IniciarSesion(string nombreUsuario, string contraseña)
        {
            Usuario usuario = new Usuario();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Usuario.Id, Usuario.Nombre, Usuario.NombreUsuario From Usuario\r\nWHERE nombreUsuario = @nombreUsuario AND Contraseña = @contraseña", conn);
                comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", contraseña);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    usuario = ManejadorUsuario.ObtenerUsuario(reader.GetInt64(0));
                    Console.WriteLine("Usuario encontrado" + $"\nNombre:{usuario.Nombre}" + $"\nApellido:{usuario.Apellido}"
                        + $"\nNombreUsuario:{usuario.NombreUsuario}");
                    return usuario;
                }
                else Console.WriteLine("\nERROR, no se pudo encontrar el usuario ingresado");
            }
            return null;
        }

        public static int ModificarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("  UPDATE Usuario\r\n  SET \r\n  Nombre = @nombre,\r\n  Apellido = @apellido,\r\n  NombreUsuario = @usuario,\r\n  Contraseña = @contraseña,\r\n  Mail = @mail\r\n  WHERE Id = @id", conn);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);
                comando.Parameters.AddWithValue("@id", usuario.Id);
                conn.Open();

                return comando.ExecuteNonQuery();
                
            }
        }
    }
}
