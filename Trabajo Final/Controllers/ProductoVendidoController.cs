using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionarProductosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("/api/ProductoVendido/{idUsuario}")]
        public List<Producto> TraerProductosVendidos(long idUsuario)
        {
            List<Producto> productos = ManejadorProductosVendidos.TraerProductosVendidos(idUsuario);
            return productos;
        }
    }
}
