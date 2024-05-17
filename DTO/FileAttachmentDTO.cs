namespace EmployeeManagement.DTO
{
    public class FileAttachmentDTO
    {
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public DateTime UploadedAt { get; set; }
    }
}
