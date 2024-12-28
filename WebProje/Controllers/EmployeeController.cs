using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // Tüm çalışanları getir
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

    // Yeni bir çalışan ekle
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

    // Çalışanı sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound($"ID {id} ile çalışan bulunamadı.");
            }

            await _employeeService.DeleteEmployeeAsync(id);
            return Ok(new { message = "Çalışan başarıyla silindi" });
        }
        catch (Exception ex)
        {
            return BadRequest($"Çalışan silinirken hata oluştu: {ex.Message}");
        }
    }
}
