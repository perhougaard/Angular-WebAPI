using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _dbContext;

        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            this._dbContext = fullStackDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _dbContext.Employees.ToListAsync();
            return Ok(employees);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody ]Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}