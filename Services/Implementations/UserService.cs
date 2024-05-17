using EmployeeManagement.DTO;
using EmployeeManagement.Helpers;
using EmployeeManagement.repositoties.interfaces;

using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }




        public async Task<string> SignInAsync(SignInDTO signInDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(signInDTO.Email, signInDTO.Password, false, false);
            if (!result.Succeeded)
            {
                return string.Empty;
            }

            return await _tokenService.GenerateJwtTokenAsync(signInDTO);
        }



        public async Task<IdentityResult> SignUpAsync(SignUpDTO signUpDTO)
        {
            var user = new ApplicationUser
            {
                FirstName = signUpDTO.FirstName,
                LastName = signUpDTO.LastName,
                Email = signUpDTO.Email,
                UserName = signUpDTO.Email
            };

            var result = await _userManager.CreateAsync(user, signUpDTO.Password);
            if (result.Succeeded)
            {
               
                if (!await _roleManager.RoleExistsAsync(AppRoles.Employee))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRoles.Employee));
                }
                await _userManager.AddToRoleAsync(user, AppRoles.Employee);

            
            }
            return result;
        }
        public async Task<IdentityResult> AddRoleAsync(RoleDTO roleDTO)
        {
            if (!await _roleManager.RoleExistsAsync(roleDTO.RoleName))
            {
                var role = new IdentityRole { Name = roleDTO.RoleName };
                return await _roleManager.CreateAsync(role);
            }
            return IdentityResult.Failed(new IdentityError { Description = "Role already exists" });
        }
        public async Task<bool> ChangeUserRoleAsync(string userId, string newRoleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false; // User not found
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return false; // Failed to remove current roles
            }

            var newRole = await _roleManager.FindByIdAsync(newRoleId);
            if (newRole == null)
            {
                return false; // Role not found
            }

            var addResult = await _userManager.AddToRoleAsync(user, newRole.Name);
            return addResult.Succeeded;
        }


    }
}
