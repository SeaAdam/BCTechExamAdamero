using BCTechExamAdamero.Dto;
using BCTechExamAdamero.Models;

namespace BCTechExamAdamero.Mappers
{
    public static class EmployeeMappers
    {
        public static EmployeesDto ToEmployeesDto (this Employee employeeModel)
        {
            return new EmployeesDto
            {
                Id = employeeModel.Id,
                Name = employeeModel.Name,
                Email = employeeModel.Email,
                Address = employeeModel.Address,
            };
        }

        public static Employee ToCreateEmployeesDto (this CreateEmployeesDto createEmployeesDto)
        {
            return new Employee
            {
                Name = createEmployeesDto.Name,
                Email = createEmployeesDto.Email,
                Address = createEmployeesDto.Address,
            };
        }

        public static Employee ToUpdateEmployeesDto (this UpdateEmployeesDto updateEmployeesDto)
        {
            return new Employee
            {
                Name = updateEmployeesDto.Name,
                Email = updateEmployeesDto.Email,
                Address = updateEmployeesDto.Address
            };
        }
    }
}
