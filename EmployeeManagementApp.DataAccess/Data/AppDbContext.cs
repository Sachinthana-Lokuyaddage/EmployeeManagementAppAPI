using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Represents the Employees table
        public DbSet<Employee> Employees => Set<Employee>();

        
    }
}
