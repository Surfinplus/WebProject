using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAppointmentsByEmployee")]
    public IActionResult GetAppointmentsByEmployee(int employeeId)
    {
        var appointments = _context.Appointments
            .Where(a => a.EmployeeId == employeeId)
            .Select(a => new
            {
                a.Id,
                a.AppointmentTime,
                EmployeeName = a.Employee.Name,
                ServiceName = a.Service.Name
            })
            .ToList();

        return Ok(appointments);
    }
}
