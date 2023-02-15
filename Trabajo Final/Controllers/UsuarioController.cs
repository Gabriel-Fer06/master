using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionarProductosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("/api/Usuario/{id}")]
        public Usuario TraerUsuario(long id)
        {
            return ManejadorUsuario.ObtenerUsuario(id);
        }

        [HttpGet("/api/Usuario/{usuario}/{contraseña}")]
        public Usuario IniciarSesion(string usuario, string contraseña)
        {
            return ManejadorUsuario.IniciarSesion(usuario, contraseña);
        }

        [HttpPut("/api/Usuario")]
        public void ModificarUsuarioApi(Usuario usuario)
        {
            ManejadorUsuario.ModificarUsuario(usuario);
        }

        [HttpDelete("/api/Usuario/Eliminar/{idUsuario}")]
        public void EliminarUsuario(long idUsuario)
        {
            ManejadorUsuario.EliminarUsuario(idUsuario);
        }

        [HttpPost("/api/Usuario")]
        public void CrearUsuario(Usuario usuario)
        {
            ManejadorUsuario.CrearUsuario(usuario);
        }
    }
}
