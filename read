## How to Build an Employee Management System Backend in ASP.NET Core

This article guides you through building a clean, maintainable Employee Management API backend with .NET. You’ll learn how to organize code into layers, implement CRUD operations, and configure dependency injection and CORS.

![Screenshot 2025-05-21 203650](https://github.com/user-attachments/assets/7da9f047-fec2-4d56-b9e4-ba1253845508)

1. Employee Entity with Validation


using System;
using System.ComponentModel.DataAnnotations;
namespace EmployeeManagementApp.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }  // Primary key

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name can't be longer than 100 characters.")]
        public string FullName { get; set; } = string.Empty;  // Employee's full name

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty;  // Employee's email address

        [Required(ErrorMessage = "Department is required.")]
        [StringLength(50, ErrorMessage = "Department can't be longer than 50 characters.")]
        public string Department { get; set; } = string.Empty;  // Department name employee belongs to

        public DateTime HireDate { get; set; } = DateTime.UtcNow;  // Date employee was hired, defaults to now
    }
}


2. EF Core Database Context

using EmployeeManagementApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagementApp.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        // Inject DbContextOptions through constructor for configuration
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet represents Employees table in the database
        public DbSet<Employee> Employees => Set<Employee>();
    }
}


3. Repository Interface (Data Access Contract)


using EmployeeManagementApp.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EmployeeManagementApp.Models.Interface
{
    // Defines data access methods for Employee entity
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();     // Get all employees
        Task<Employee?> GetByIdAsync(int id);          // Get employee by ID (nullable if not found)
        Task AddAsync(Employee employee);               // Add new employee
        Task UpdateAsync(Employee employee);            // Update existing employee
        Task DeleteAsync(int id);                        // Delete employee by ID
    }
}


4. Repository Implementation (EF Core logic)


using EmployeeManagementApp.DataAccess.Data;
using EmployeeManagementApp.Models.Entities;
using EmployeeManagementApp.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EmployeeManagementApp.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        // Inject AppDbContext to access database
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all employees asynchronously from database
        public async Task<IEnumerable<Employee>> GetAllAsync() =>
            await _context.Employees.ToListAsync();

        // Find employee by primary key ID asynchronously
        public async Task<Employee?> GetByIdAsync(int id) =>
            await _context.Employees.FindAsync(id);

        // Add new employee entity and save changes asynchronously
        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        // Update existing employee entity and save changes asynchronously
        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        // Delete employee entity by ID if found, then save changes asynchronously
        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}



5. Service Interface (Business Logic Contract)


using EmployeeManagementApp.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EmployeeManagementApp.Models.Interface
{
    // Defines business operations on employees, can add validations or rules here
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}



6. Service Implementation (Business Logic)


using EmployeeManagementApp.Models.Entities;
using EmployeeManagementApp.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EmployeeManagementApp.Service
{
    // Implements business logic, currently just calls repository methods directly
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Employee>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Employee?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task AddAsync(Employee employee) => _repository.AddAsync(employee);

        public Task UpdateAsync(Employee employee) => _repository.UpdateAsync(employee);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}



7. Employees API Controller


using EmployeeManagementApp.Models.Entities;
using EmployeeManagementApp.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace EmployeeManagementApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  // Route: api/employees
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        // Inject the service layer for business operations
        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // GET api/employees
        // Returns all employees
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        // GET api/employees/{id}
        // Returns employee by ID or 404 if not found
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null) return NotFound();  // 404 if no employee
            return Ok(employee);                      // 200 OK with employee data
        }

        // POST api/employees
        // Creates new employee and returns 201 Created with location header
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _service.AddAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // PUT api/employees/{id}
        // Updates an existing employee, returns 400 if id mismatch, else 204 No Content
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();  // Id in route must match body

            await _service.UpdateAsync(employee);
            return NoContent();  // 204 No Content on success
        }

        // DELETE api/employees/{id}
        // Deletes employee by ID and returns 204 No Content
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}


8. Program.cs / Startup.cs Configuration


using EmployeeManagementApp.DataAccess.Data;
using EmployeeManagementApp.DataAccess.Repositories;
using EmployeeManagementApp.Models.Interface;
using EmployeeManagementApp.Service;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add controllers for API
builder.Services.AddControllers();

// Add Swagger for API documentation/testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core to use SQL Server with connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repository and service with Dependency Injection container
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Setup CORS policy to allow frontend React app requests from localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Enable Swagger UI in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS middleware with the named policy
app.UseCors("AllowReactApp");

app.MapControllers();

app.Run();


Summary
Entities define your data shape with validation.

DbContext connects to the database.

Repositories handle database CRUD operations.

Services encapsulate business logic (currently pass-through).

Controllers expose REST API endpoints.

Program.cs wires everything up with dependency injection, CORS, and Swagger.



