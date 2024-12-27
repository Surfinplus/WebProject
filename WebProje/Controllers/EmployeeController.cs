using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        try
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            return BadRequest($"Hata: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
    {
        try
        {
            if (employee == null)
                return BadRequest("Çalışan bilgileri boş olamaz");

            await _employeeService.AddEmployeeAsync(employee);
            return Ok(new { message = "Çalışan başarıyla eklendi" });
        }
        catch (Exception ex)
        {
            return BadRequest($"Çalışan eklenirken hata oluştu: {ex.Message}");
        }
    }
}
