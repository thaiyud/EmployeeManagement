using EmployeeManagement.DTO;
using EmployeeManagement.Helpers;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public RolesController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpPost("AddRole")]
        [Authorize(Roles = AppRoles.Admin)]
        [ClaimRequirement("api", "AddRole")]
        public async Task<IActionResult> AddRole(RoleDTO roleDTO)
        {
            var result = await _userService.AddRoleAsync(roleDTO);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("ChangeUserRole")]
        [Authorize(Roles = AppRoles.Admin)]
        [ClaimRequirement("api", "ChangeRole")]
        public async Task<IActionResult> ChangeUserRole(string userId, string newRoleId)
        {
            var result = await _userService.ChangeUserRoleAsync(userId, newRoleId);
            if (result)
            {
                return Ok(new { message = "User role changed successfully." });
            }
            return BadRequest(new { message = "Failed to change user role." });
        }
    }
}
