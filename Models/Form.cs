using EmployeeManagement.DTO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Form
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("FormType")]
        public int FormTypeId { get; set; }

        public DateTime DateSubmitted { get; set; }
        public DateTime? DayStart { get; set; } 
        public DateTime? DayEnd { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual FormType FormType { get; set; }
        public virtual FileAttachment FileAttachment { get; set; }
    }
}
