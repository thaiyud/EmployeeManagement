using EmployeeManagement.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public interface IFormService
    {
        Task<FormDTO> SubmitFormAsync(FormDTO formDto, IFormFileCollection files);
        Task<IEnumerable<FormDTO>> GetFormsByUserEmailAsync(string email);
    }
}
