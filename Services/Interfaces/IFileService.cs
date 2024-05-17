using EmployeeManagement.DTO;

namespace EmployeeManagement.Services.Interfaces
{
    public interface IFileService
    {
        Task<FileAttachmentDTO> UploadFileAsync(IFormFile file, int formId);
        Task<FileAttachmentDTO?> GetFileAsync(int fileId);
    }
}
