using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementApp.Models.Entities;
using EmployeeManagementApp.Models.Interface;

namespace EmployeeManagementApp.Service
{
    //Handles business logic and uses the repository for database interaction.
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
