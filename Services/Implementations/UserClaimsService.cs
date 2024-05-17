using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeManagement.DTO;
using EmployeeManagement.Repositoties.Interfaces;
using Microsoft.AspNetCore.Identity;

public class UserClaimsService : IUserClaimsService
{
    private readonly IClaimsRepository _claimsRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserClaimsService(IClaimsRepository claimsRepository, UserManager<ApplicationUser> userManager)
    {
        _claimsRepository = claimsRepository;
        _userManager = userManager;
    }

    public async Task<IList<Claim>> GetUserClaimsAsync(string userId)
    {
        var claims = await _claimsRepository.GetUserClaimsAsync(userId);
        return claims.ConvertAll(c => new Claim(c.ClaimType, c.ClaimValue));
    }

    public async Task AddClaimAsync(string userId, string claimType, string claimValue)
    {
        var userClaim = new IdentityUserClaim<string>
        {
            UserId = userId,
            ClaimType = claimType,
            ClaimValue = claimValue
        };
        await _claimsRepository.CreateAsync(userClaim);
    }

    public async Task RemoveClaimAsync(string userId, string claimType, string claimValue)
    {
        var claims = await _claimsRepository.GetUserClaimsAsync(userId);
        var claimToRemove = claims.Find(c => c.ClaimType == claimType && c.ClaimValue == claimValue);
        if (claimToRemove != null)
        {
            await _claimsRepository.DeleteAsync(claimToRemove.Id);
        }
    }
}
