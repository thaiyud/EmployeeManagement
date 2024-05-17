using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.DTO;

namespace EmployeeManagement.Models
{
    public class BasicSalary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicAmount { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<MonthlySalary> MonthlySalaries { get; set; }
    }
}
