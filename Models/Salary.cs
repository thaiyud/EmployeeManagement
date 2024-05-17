using EmployeeManagement.DTO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{

    public class Salary
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BasicPay { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Allowances { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Bonuses { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Deductions { get; set; }

       


        public virtual ApplicationUser User { get; set; }

    }
}