using AutoMapper;
using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using Microsoft.SqlServer.Server;

namespace EmployeeManagement.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Form, FormDTO>().ReverseMap();
            CreateMap<FileAttachment, FileAttachmentDTO>().ReverseMap();
        }
    }
}
