using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class FormType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Form> Forms { get; set; }
    }
}
