using employee.Repository;
using Microsoft.AspNetCore.Mvc;
using ProductData.Models;

namespace employee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _productRepository.GetAll();
            return Ok(employees);
        }

        [HttpPut("{pid}/status")]
        public async Task<IActionResult> UpdateStatus(string pid)
        {
            var result = await _productRepository.GetByIdAsync(pid);

            if (result == null)
            {
                return BadRequest();
            }

            var updateData = await _productRepository.UpdateAsync(pid);
            if (updateData > 0)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("status-all")]
        public async Task<IActionResult> UpdateStatusAll()
        {

            var updateData = await _productRepository.UpdateStatusAllAsync();
            if (updateData > 0)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
