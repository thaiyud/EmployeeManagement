using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EmployeeManagement.repositoties.interfaces;
using Microsoft.SqlServer.Server;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using EmployeeManagement.Services.Interfaces;

namespace EmployeeManagement.Services
{
    public class FormService : IFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFileService _fileService;

        public FormService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _fileService = fileService;
        }

        public async Task<FormDTO> SubmitFormAsync(FormDTO formDto, IFormFileCollection files)
        {
            var form = _mapper.Map<Form>(formDto);
            form.DateSubmitted = DateTime.UtcNow;
            // Add form to the database
            var formRepository = _unitOfWork.GetRepository<Form>();
            await formRepository.CreateAsync(form);
            await _unitOfWork.SaveChangesAsync();

            // Handle file uploads
            var attachmentDtos = new List<FileAttachmentDTO>();

            foreach (var file in files)
            {
                var fileDto = await _fileService.UploadFileAsync(file, form.Id);
                attachmentDtos.Add(fileDto);
            }

            var resultDto = _mapper.Map<FormDTO>(form);
            resultDto.Attachments = attachmentDtos;

            return resultDto;
        }
        public async Task<IEnumerable<FormDTO>> GetFormsByUserEmailAsync(string email)
        {
            var forms = await _unitOfWork.Forms.GetFormsByUserEmailAsync(email);
            return _mapper.Map<IEnumerable<FormDTO>>(forms);
        }
    }
}
