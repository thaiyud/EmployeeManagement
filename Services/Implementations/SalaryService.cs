using EmployeeManagement.Models;
using EmployeeManagement.repositoties.interfaces;
using EmployeeManagement.Repositoties.Interfaces;
using EmployeeManagement.Services.Interfaces;

namespace EmployeeManagement.Services.Implementations
{
    public class SalaryService : ISalaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISalaryRepository _salaryRepository;

        public SalaryService(IUnitOfWork unitOfWork, ISalaryRepository salaryRepository)
        {
            _unitOfWork = unitOfWork;
            _salaryRepository = salaryRepository;
        }

        public async Task<decimal> CheckSalary(string userId, int month, int year)
        {
            // Retrieve basic salary
            var basicSalary = await _salaryRepository.GetBasicSalaryByUserIdAsync(userId);
            if (basicSalary == null)
            {
                throw new InvalidOperationException("Basic salary not found for the user.");
            }

   
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int numberOfDaysOff = await GetNumberOfDaysOff(userId, month, year);
            decimal bonuses = 0; 
            decimal deductions = numberOfDaysOff * (basicSalary.BasicAmount / daysInMonth); 
            decimal allowances = 0; 

           
            decimal salary = basicSalary.BasicAmount + bonuses - deductions + allowances;
            // Check if a record already exists for the specified BasicId, month, and year
            var existingMonthlySalary = await _salaryRepository.GetMonthlySalaryByUserIdAsync(userId, month, year);

            if (existingMonthlySalary != null)
            {
                // Update the existing record
                existingMonthlySalary.Bonuses = bonuses;
                existingMonthlySalary.Deductions = deductions;
                existingMonthlySalary.Allowances = allowances;
                existingMonthlySalary.Salary = salary;

                _salaryRepository.UpdateAsync(existingMonthlySalary);
            }
            else
            {
                // Save monthly salary
                var newMonthlySalary = new MonthlySalary
                {
                    BasicId = basicSalary.Id,
                    Month = month,
                    Year = year,
                    Bonuses = bonuses,
                    Deductions = deductions,
                    Allowances = allowances,
                    Salary = salary
                };
            await _salaryRepository.CreateAsync(newMonthlySalary);
            await _unitOfWork.SaveChangesAsync();
            }

            return salary;
        }
        public async Task<int> GetNumberOfDaysOff(string userId, int month, int year)
        {
           
            var forms = await _unitOfWork.Forms.GetFormsByUserIdAndMonthAsync(userId, month, year);

           
            int totalDaysOff = 0;
            foreach (var form in forms)
            {
              
                DateTime formStartDate = form.DayStart ?? DateTime.MinValue;
                DateTime formEndDate = form.DayEnd ?? DateTime.MinValue;


                if (formStartDate < new DateTime(year, month, 1))
                {
                    formStartDate = new DateTime(year, month, 1);
                }
                if (formEndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))
                {
                    formEndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                }

                int daysOffForForm = (int)(formEndDate - formStartDate).TotalDays + 1;

               
                totalDaysOff += daysOffForForm;
            }

            return totalDaysOff;
        }
      

    }


}
