using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTO

{
    public class SignInDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
