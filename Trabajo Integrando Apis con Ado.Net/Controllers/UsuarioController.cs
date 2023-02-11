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

        [HttpPut("/api/Usuario")]
        public void ModificarUsuarioApi(Usuario usuario)
        {
            ManejadorUsuario.ModificarUsuario(usuario);
        }
    }
}
