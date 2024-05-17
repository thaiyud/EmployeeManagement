using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using EmployeeManagement.Repositoties.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ClaimsRepository : Repository<IdentityUserClaim<string>>, IClaimsRepository
{
    private readonly EmployeeManagementDBContext _context;

    public ClaimsRepository(EmployeeManagementDBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<IdentityUserClaim<string>>> GetUserClaimsAsync(string userId)
    {
        return await _context.UserClaims
            .Where(uc => uc.UserId == userId)
            .ToListAsync();
    }

}
