using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public interface IUserClaimsService
{
    Task<IList<Claim>> GetUserClaimsAsync(string userId);
    Task AddClaimAsync(string userId, string claimType, string claimValue);
    Task RemoveClaimAsync(string userId, string claimType, string claimValue);
}
