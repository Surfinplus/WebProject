using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly ILogger<AppointmentController> _logger;
    private readonly ApplicationDbContext _context;

    public AppointmentController(IAppointmentService appointmentService, 
                               ILogger<AppointmentController> logger,
                               ApplicationDbContext context)
    {
        _appointmentService = appointmentService;
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
    {
        try
        {
            if (appointment == null)
                return BadRequest("Randevu bilgileri boş olamaz");

            // Gelen veriyi logla
            _logger.LogInformation($"Gelen randevu verisi: {JsonSerializer.Serialize(appointment)}");

            // Temel validasyonlar
            if (appointment.EmployeeId <= 0)
                return BadRequest("Geçerli bir çalışan seçilmedi");

            if (appointment.AppointmentTime < DateTime.Now)
                return BadRequest("Geçmiş bir tarihe randevu oluşturulamaz");

            // Randevu çakışması kontrolü
            var appointmentDate = appointment.AppointmentTime.Date;
            var appointmentTime = appointment.AppointmentTime.TimeOfDay;

            // Seçilen çalışanın o saatteki randevularını kontrol et
            var existingAppointment = await _context.Appointments
                .FirstOrDefaultAsync(a => 
                    a.EmployeeId == appointment.EmployeeId && 
                    a.AppointmentTime.Date == appointmentDate &&
                    a.AppointmentTime.TimeOfDay == appointmentTime);

            if (existingAppointment != null)
            {
                return BadRequest($"Seçilen çalışanın {appointment.AppointmentTime:HH:mm} saatinde başka bir randevusu bulunmaktadır. Lütfen başka bir saat seçin.");
            }

            // CustomerName boş ise UserId'yi kullan
            if (string.IsNullOrEmpty(appointment.CustomerName))
            {
                appointment.CustomerName = User.Identity?.Name ?? "Anonim";
            }

            await _appointmentService.AddAppointmentAsync(appointment);
            return Ok(new { message = "Randevu başarıyla oluşturuldu" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Randevu oluşturma hatası: {ex.Message}");
            if (ex.InnerException != null)
            {
                _logger.LogError($"İç hata: {ex.InnerException.Message}");
            }
            return BadRequest(new { error = ex.Message, innerError = ex.InnerException?.Message });
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserAppointments(string userId)
    {
        try
        {
            var appointments = await _appointmentService.GetAppointmentsByUserIdAsync(userId);
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Randevuları getirme hatası: {ex.Message}");
            return BadRequest($"Randevular getirilirken hata oluştu: {ex.Message}");
        }
    }

    // Tüm randevuları getir (Admin için)
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAppointments()
    {
        try
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Randevular getirilirken hata oluştu: {ex.Message}");
            return BadRequest("Randevular getirilemedi");
        }
    }

    // Randevu silme (Admin için)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        try
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return Ok(new { message = "Randevu başarıyla silindi" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Randevu silinirken hata oluştu: {ex.Message}");
            return BadRequest("Randevu silinemedi");
        }
    }
}
