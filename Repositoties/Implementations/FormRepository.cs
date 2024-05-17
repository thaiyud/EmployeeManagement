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
    }
}
