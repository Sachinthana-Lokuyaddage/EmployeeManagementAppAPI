using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models.Entities
{
  public class Employee
{
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name can't be longer than 100 characters.")]
        public string FullName { get; set; } = string.Empty; // Employee's full name

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty; // Employee's email

        [Required(ErrorMessage = "Department is required.")]
        [StringLength(50, ErrorMessage = "Department can't be longer than 50 characters.")]
        public string Department { get; set; } = string.Empty; // Department name

        public DateTime HireDate { get; set; } = DateTime.UtcNow; // Default value
    }
}
