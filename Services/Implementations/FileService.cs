using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;
using EmployeeManagement.repositoties.interfaces;
using EmployeeManagement.Services.Interfaces;
using Microsoft.Extensions.Hosting.Internal;

namespace EmployeeManagement.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FileAttachmentDTO> UploadFileAsync(IFormFile file, int formId)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (string.IsNullOrEmpty(_hostingEnvironment.WebRootPath)) throw new InvalidOperationException("Web root path is not configured.");

            var extension = Path.GetExtension(file.FileName);
            var fileName = DateTime.Now.Ticks.ToString() + extension;
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Files", fileName);

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileAttachment = new FileAttachment
            {
                FileName = fileName,
                FileUrl = filePath,
                UploadedAt = DateTime.Now,
                FormId = formId
            };

            var attachmentRepository = _unitOfWork.GetRepository<FileAttachment>();
            await attachmentRepository.CreateAsync(fileAttachment);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<FileAttachmentDTO>(fileAttachment);
        }

        public async Task<FileAttachmentDTO?> GetFileAsync(int fileId)
        {
            var attachmentRepository = _unitOfWork.GetRepository<FileAttachment>();
            var fileAttachment = await attachmentRepository.GetByIdAsync(fileId);

            return fileAttachment != null ? _mapper.Map<FileAttachmentDTO>(fileAttachment) : null;
        }
    }
}
