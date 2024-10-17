using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private static List<int> _wishlist = new List<int>(); 

        [HttpPost]
        public IActionResult PostWishlist(int productId)
        {
            if (!_wishlist.Contains(productId))
            {
                _wishlist.Add(productId);
                return CreatedAtAction(nameof(GetWishlist), new { id = productId }, productId);
            }

            return BadRequest("El producto ya está en la lista de deseos.");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteWishlist(int id)
        {
            
            if (_wishlist.Contains(id))
            {
                _wishlist.Remove(id);
                return NoContent(); 
            }

            
            return NotFound("El producto no se encontró en la lista de deseos.");
        }

        [HttpGet]
        public IActionResult GetWishlist()
        {
            return Ok(_wishlist);
        }
    }
}
