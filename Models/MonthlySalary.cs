using EmployeeManagement.DTO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{

    public class MonthlySalary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BasicId { get; set; }
        public BasicSalary BasicSalary { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Bonuses { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductions { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Allowances { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }


 

    }
}