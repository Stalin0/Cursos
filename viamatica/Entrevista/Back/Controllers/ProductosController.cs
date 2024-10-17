using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back.Data;
using Back.Models;
using System.Threading.Tasks;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductosController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            var categoria = await _context.Categorias.FindAsync(producto.CategoriaId);
            if (categoria == null)
            {
                return NotFound("La categoría especificada no existe.");
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            var resultado = new
            {
                producto.ProductoId,
                producto.Nombre,
                producto.Precio,
                producto.Stock,
                producto.CategoriaId,
                producto.CategoriaNombre,

            };

            return CreatedAtAction(nameof(GetProducto), new { id = producto.ProductoId }, resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        [HttpGet("byPrecio")]
        public async Task<ActionResult<Producto>> GetProductoByPrecio(int precio)
        {
            var promedioPrecio = await _context.Productos.AverageAsync(p => p.Precio);

            return Ok(promedioPrecio);
        }
    }
}
