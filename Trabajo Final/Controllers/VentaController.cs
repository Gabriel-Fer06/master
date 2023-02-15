using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionarProductosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost("/api/Venta/{idUsuario}")]
        public void CargarVenta(long idUsuario, List<Producto> productos)
        {
            ManejadorVenta.CargarVenta(idUsuario, productos);
        }

        [HttpGet("/api/Venta/{idUsuario}")]
        public List<Venta> TraerVenta(long idUsuario)
        {
            List<Venta> ventas = ManejadorVenta.ObtenerVentasUsuario(idUsuario);
            return ventas;
        }
    }
}
