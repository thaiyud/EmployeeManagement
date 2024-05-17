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

            //// Retrieve monthly salary
            //var monthlySalary = await _salaryRepository.GetMonthlySalaryByUserIdAsync(userId, month, year);
            //if (monthlySalary != null)
            //{
            //    // Monthly salary already calculated, return it
            //    return monthlySalary.Salary;
            //}
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int numberOfDaysOff = await GetNumberOfDaysOff(userId, month, year);
            decimal bonuses = 0; 
            decimal deductions = numberOfDaysOff * (basicSalary.BasicAmount / daysInMonth); 
            decimal allowances = 0; 

           
            decimal salary = basicSalary.BasicAmount + bonuses - deductions + allowances;

            // Save monthly salary
            var newMonthlySalary = new MonthlySalary
            {
                Id = basicSalary.Id,
                Month = month,
                Year = year,
                Bonuses = bonuses,
                Deductions = deductions,
                Allowances = allowances,
                Salary = salary
            };
            await _salaryRepository.CreateAsync(newMonthlySalary);
            await _unitOfWork.SaveChangesAsync();

            return salary;
        }
        public async Task<int> GetNumberOfDaysOff(string userId, int month, int year)
        {
           
            var forms = await _unitOfWork.Forms.GetFormsByUserIdAndMonthAsync(userId, month, year);

           
            int totalDaysOff = 0;
            foreach (var form in forms)
            {
                // Check if the form spans multiple months
                DateTime formStartDate = form.DayStart ?? DateTime.MinValue;
                DateTime formEndDate = form.DayEnd ?? DateTime.MinValue;

                // Adjust the start and end dates if they fall outside the given month
                if (formStartDate.Month != month)
                {
                    formStartDate = new DateTime(year, month, 1);
                }
                if (formEndDate.Month != month)
                {
                    formEndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                }

                // Calculate the number of days off for this form
                int daysOffForForm = (int)(formEndDate - formStartDate).TotalDays + 1;

                // Add the days off for this form to the total
                totalDaysOff += daysOffForForm;
            }

            return totalDaysOff;
        }
      

    }


}
