using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.DTO
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    
    }
}
