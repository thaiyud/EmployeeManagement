using EmployeeManagement.DTO;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormsController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpPost("SubmitForm")]
        
        public async Task<IActionResult> SubmitForm([FromForm] FormDTO formDto, [FromForm] IFormFileCollection files)
        {
            var result = await _formService.SubmitFormAsync(formDto, files);
            return Ok(result);
        }
        [HttpGet("GetFormsByUserEmail")]
        public async Task<ActionResult<IEnumerable<FormDTO>>> GetFormsByUserEmail(string email)
        {
            var forms = await _formService.GetFormsByUserEmailAsync(email);
            return Ok(forms);
        }
    }
}
