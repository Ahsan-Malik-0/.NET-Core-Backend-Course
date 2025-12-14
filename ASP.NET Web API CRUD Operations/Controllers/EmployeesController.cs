using ASP.NET_Web_API_CRUD_Operations.Data;
using ASP.NET_Web_API_CRUD_Operations.Models;
using ASP.NET_Web_API_CRUD_Operations.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace ASP.NET_Web_API_CRUD_Operations.Controllers
{
    // localhoat:XXXX/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(DB dbContext) : ControllerBase
    {
        private readonly DB dbContext = dbContext;

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var AllEmployee = dbContext.Employees.ToList();
            return Ok(AllEmployee);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                Salary = employeeDto.Salary
            };
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return Created();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null) {
                return NotFound();
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{name}")]
        public IActionResult DeleteEmployeeByName(string name)
        {
            // Find all employees matching the provided name and delete them.
            var matches = dbContext.Employees
                                   .Where(e => e.Name == name)
                                   .ToList();

            if (!matches.Any())
            {
                return NotFound();
            }

            dbContext.Employees.RemoveRange(matches);
            dbContext.SaveChanges();

            return Ok(new { DeletedCount = matches.Count });
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult EditEmployeeById(EmployeeDto employeeDto, Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.Salary = employeeDto.Salary;
            return Ok(employee);
        }

    }
}
