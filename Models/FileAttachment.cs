using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class FileAttachment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public string FileUrl { get; set; } = null!;

        [Required]
        public DateTime UploadedAt { get; set; }

        public int FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; } = null!;
    }
}
