using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SalonController : ControllerBase
{
    private readonly ISalonService _salonService;

    public SalonController(ISalonService salonService)
    {
        _salonService = salonService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSalons()
    {
        var salons = await _salonService.GetAllSalonsAsync();
        return Ok(salons);
    }

    [HttpPost]
    public async Task<IActionResult> AddSalon([FromBody] Salon salon)
    {
        await _salonService.AddSalonAsync(salon);
        return Ok();
    }
}
