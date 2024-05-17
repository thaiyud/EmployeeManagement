using EmployeeManagement.Data;
using EmployeeManagement.DTO;

namespace EmployeeManagement.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(SignInDTO model);
        //string GenerateJwtToken(User user, IList<string> roles);
    }
}

