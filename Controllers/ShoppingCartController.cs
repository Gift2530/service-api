using employee.Repository;
using Microsoft.AspNetCore.Mvc;
using ProductData.Models;
using ProductData.Repository;

namespace ProductData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartController(ShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpPut("{pid}")]
        public async Task<IActionResult> UpdateQuantityAsync(string pid, [FromBody] ShoppingCarts shopping)
        {
            if ((shopping.reserve.Equals(0)) || (pid != shopping.pid))
            {
                return BadRequest();
            }

            var result = await _shoppingCartRepository.GetByIdAsync(pid);
            if (result == null)
            {
                return NotFound();
            }

            if (shopping.qty < shopping.reserve)
            {
                return NotFound();
            }

            var stock = (shopping.qty - shopping.reserve);
            shopping.qty = stock;

            var rows = await _shoppingCartRepository.UpdateAsync(shopping);
            if (rows > 0)
            {
                return NoContent();
            }
            return NotFound();
        }

    }
}
