using EmployeeManagement.DTO;
using EmployeeManagement.Helpers;
using EmployeeManagement.Models;
using EmployeeManagement.repositoties.interfaces;
using EmployeeManagement.Repositoties.Implementations;
using EmployeeManagement.Repositoties.Interfaces;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISalaryService _salaryService;
    
        private readonly UserManager<ApplicationUser> _userManager;

        public SalariesController(IUnitOfWork unitOfWork, ISalaryService salaryService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _salaryService= salaryService;
            _userManager = userManager;
        }

        [HttpPost("AddBasicSalary")]
        [Authorize(Roles = AppRoles.Accountant)]
        [ClaimRequirement("api", "AddBasicSalary")]
        public async Task<IActionResult> AddBasicSalary(BasicSalaryDTO basicSalaryDTO)
        {
            try
            {
               
                var basicSalary = new BasicSalary
                {
                    UserId = basicSalaryDTO.UserId,
                    BasicAmount = basicSalaryDTO.BasicAmount
                };

                
                await _unitOfWork.GetRepository<BasicSalary>().CreateAsync(basicSalary);
                await _unitOfWork.SaveChangesAsync();

                return Ok("Basic salary added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetSalaryByEmail")]
        [Authorize(Roles = AppRoles.Accountant)]
        [ClaimRequirement("api", "GetSalaryByEmail")]
        public async Task<IActionResult> GetSalaryByEmail(string email, int month, int year)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return NotFound($"No user found with email '{email}'.");
                }

                var salary = await _salaryService.CheckSalary(user.Id, month, year);

                if (salary == null)
                {
                    return NotFound($"No salary information found for user with email '{email}' for month {month} and year {year}.");
                }

                return Ok(salary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
