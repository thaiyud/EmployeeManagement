using EmployeeManagement.DTO;
using EmployeeManagement.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Authorize(Roles = AppRoles.Admin)]
[Route("api/[controller]")]
[ApiController]
public class UserClaimsController : ControllerBase
{
    private readonly IUserClaimsService _userClaimsService;

    public UserClaimsController(IUserClaimsService userClaimsService)
    {
        _userClaimsService = userClaimsService;
    }

    [HttpPost("AddUserClaim")]
    [Authorize(Roles = AppRoles.Admin)]
    [ClaimRequirement("api", "AddUserClaim")]
    public async Task<IActionResult> AddClaim([FromBody] UserClaimDTO model)
    {
        await _userClaimsService.AddClaimAsync(model.UserId, model.ClaimType, model.ClaimValue);
        return Ok("Claim added");
    }

    [HttpPost("RemoveUserClaim")]
    [Authorize(Roles = AppRoles.Admin)]
    [ClaimRequirement("api", "RemoveUserClaim")]
    public async Task<IActionResult> RemoveClaim([FromBody] UserClaimDTO model)
    {
        await _userClaimsService.RemoveClaimAsync(model.UserId, model.ClaimType, model.ClaimValue);
        return Ok("Claim removed");
    }

    [HttpGet("GetUserClaims/{userId}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<IActionResult> GetClaims(string userId)
    {
        var claims = await _userClaimsService.GetUserClaimsAsync(userId);
        return Ok(claims);
    }
}

