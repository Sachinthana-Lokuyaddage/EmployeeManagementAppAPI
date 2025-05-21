using EmployeeManagementApp.Models.Entities;
using EmployeeManagementApp.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Presentation.Controllers
{
   
    [ApiController]

    // Route for this controller: api/employees
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        // Service layer dependency (business logic)
        private readonly IEmployeeService _service;

        // Constructor with Dependency Injection of IEmployeeService
        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employees
        // Retrieves a list of all employees
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        // GET: api/employees/{id}
        // Retrieves a single employee by their ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null) return NotFound(); // Return 404 if not found
            return Ok(employee); // Return 200 with the employee data
        }

        // POST: api/employees
        // Creates a new employee
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _service.AddAsync(employee); // Add the employee via service
            // Return 201 Created with the location of the new resource
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // PUT: api/employees/{id}
        // Updates an existing employee
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            // Ensure route ID matches employee ID in body
            if (id != employee.Id) return BadRequest(); // Return 400 if mismatch
            await _service.UpdateAsync(employee); // Update employee via service
            return NoContent(); // Return 204 No Content if successful
        }

        // DELETE: api/employees/{id}
        // Deletes an employee by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id); // Delete via service
            return NoContent(); // Return 204 No Content
        }
    }
}
