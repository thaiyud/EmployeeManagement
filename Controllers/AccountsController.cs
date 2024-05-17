using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeManagement.DTO;
using EmployeeManagement.Services.Interfaces;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDTO signUpModel)
        {
            var result = await _userService.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInDTO signInModel)
        {
            var result = await _userService.SignInAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(new { Token = result });
        }

    }
}
