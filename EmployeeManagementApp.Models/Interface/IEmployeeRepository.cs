using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementApp.Models.Entities;

namespace EmployeeManagementApp.Models.Interface
{
    public interface IEmployeeRepository
    {
        // Defines the contract for data access operations. Helps in loose coupling and easy testing.
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
