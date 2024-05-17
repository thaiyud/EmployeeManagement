using EmployeeManagement.DTO;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> SignInAsync(SignInDTO signInDTO);
        Task<IdentityResult> SignUpAsync(SignUpDTO signUpDTO);
        Task<IdentityResult> AddRoleAsync(RoleDTO roleDTO);
        Task<bool> ChangeUserRoleAsync(string userId, string newRoleId);
    }
}
    