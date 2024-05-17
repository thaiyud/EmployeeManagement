using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Repositoties.Interfaces
{
    public interface IClaimsRepository : IRepository<IdentityUserClaim<string>>
    {
        Task<List<IdentityUserClaim<string>>> GetUserClaimsAsync(string userId);
    }
}
