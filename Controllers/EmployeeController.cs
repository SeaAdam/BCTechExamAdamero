using BCTechExamAdamero.Data;
using BCTechExamAdamero.Dto;
using BCTechExamAdamero.Interface;
using BCTechExamAdamero.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BCTechExamAdamero.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeController(ApplicationDbContext context, IEmployeeRepository employeeRepo)
        {
            _context = context;
            _employeeRepo = employeeRepo;
        }

        //create employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeesDto createEmployeesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeModel = createEmployeesDto.ToCreateEmployeesDto();
            await _employeeRepo.CreateAsync(employeeModel);

            return StatusCode(201, employeeModel.ToEmployeesDto());
        }

        //get employee to list
        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeModel = await _employeeRepo.GetAllAsync();
            var employees = employeeModel.Select(e => e.ToEmployeesDto());

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        //get employee by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = await _employeeRepo.FindByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return StatusCode(201, employee.ToEmployeesDto());
        }

        //update employee
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] UpdateEmployeesDto updateEmployeesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeModel = await _employeeRepo.UpdateAsync(id , updateEmployeesDto.ToUpdateEmployeesDto());

            if (employeeModel == null)
            {
                return NotFound();
            }

            return Ok(employeeModel.ToEmployeesDto());
        }

        //delete employee 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee (int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = await _employeeRepo.DeleteAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return NoContent();

        }

    }
}
