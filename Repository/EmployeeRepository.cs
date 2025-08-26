using BCTechExamAdamero.Data;
using BCTechExamAdamero.Interface;
using BCTechExamAdamero.Models;
using Microsoft.EntityFrameworkCore;

namespace BCTechExamAdamero.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> CreateAsync(Employee employeeModel)
        {
            await _context.Employees.AddAsync(employeeModel);
            await _context.SaveChangesAsync();

            return employeeModel;
        }

        public async Task<Employee?> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return null;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> FindByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> UpdateAsync(int id, Employee employeeModel)
        {
            var oldEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (oldEmployee == null) 
            { 
                return null;
            }

            oldEmployee.Name = employeeModel.Name;
            oldEmployee.Email = employeeModel.Email;
            oldEmployee.Address = employeeModel.Address;

            await _context.SaveChangesAsync();

            return (oldEmployee);

            
        }
    }
}
