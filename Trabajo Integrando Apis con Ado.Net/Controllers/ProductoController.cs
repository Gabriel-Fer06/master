using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionarProductosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpPost("/api/Producto")]
        public void InsertarProducto(Producto producto)
        {
            ManejadorProducto.CrearProducto(producto);

        }

        [HttpPut("/api/Producto")]
        public void ModificarProducto(Producto producto)
        {
            ManejadorProducto.ModificarProducto(producto);
        }

        [HttpDelete("/api/Producto/{id}")]
        public void EliminarProducto(long id)
        {
            ManejadorProducto.EliminarProducto(id);
        }
    }
}
