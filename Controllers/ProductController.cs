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

        #region old
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetEmployee(int id)
        //{
        //    var employee = await _employeeRepository.GetByIdAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(employee);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        //{
        //    var rowsAffected = await _employeeRepository.AddAsync(employee);
        //    if (rowsAffected > 0)
        //    {
        //        return Created("", 201);
        //    }
        //    return BadRequest("Failed to add employee");
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var rowsAffected = await _employeeRepository.UpdateAsync(employee);
        //    if (rowsAffected > 0)
        //    {
        //        return NoContent();
        //    }
        //    return NotFound();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployee(int id)
        //{
        //    var rowsAffected = await _employeeRepository.DeleteAsync(id);
        //    if (rowsAffected > 0)
        //    {
        //        return NoContent();
        //    }
        //    return NotFound();
        //}
        #endregion
    }
}
