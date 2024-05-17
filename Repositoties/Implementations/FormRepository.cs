using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Repositories
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        private readonly EmployeeManagementDBContext _context;

        public FormRepository(EmployeeManagementDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Form>> GetFormsByUserEmailAsync(string email)
        {
            return await _context.Forms
                                 .Include(f => f.User)
                                 .Where(f => f.User.Email == email)
                                 .ToListAsync();
        }
        public async Task<List<Form>> GetFormsByUserIdAndMonthAsync(string userId, int month, int year)
        {
            return await _context.Forms
                .Where(f => f.UserId == userId && f.DateSubmitted.Month == month && f.DateSubmitted.Year == year)
                .ToListAsync();
        }
    }
}
