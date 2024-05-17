using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTO

{
    public class SignUpDTO
    {
        [Required]

        public string FirstName { get; set; } = null!;
        [Required]

        public string LastName { get; set; } = null!; 
        
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]

        public string Password { get; set; } = null!;

        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
