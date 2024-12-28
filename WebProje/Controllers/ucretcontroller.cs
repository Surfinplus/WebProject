using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class UcretKontrolController : ControllerBase
{
    // Ücret kontrol için minimum ve maksimum değerler
    private readonly decimal _minimumUcret = 50m;
    private readonly decimal _maksimumUcret = 500m;

    // Ücretin geçerli olup olmadığını kontrol eden endpoint
    [HttpGet("UcretGecerliMi")]
    public IActionResult UcretGecerliMi(decimal ucret)
    {
        if (ucret >= _minimumUcret && ucret <= _maksimumUcret)
        {
            return Ok(new { IsValid = true, Message = "Ücret geçerli." });
        }
        else
        {
            return BadRequest(new { IsValid = false, Message = $"Ücret {_minimumUcret} ile {_maksimumUcret} arasında olmalıdır." });
        }
    }

    // Ücret üzerinden bir yüzde hesaplayan endpoint
    [HttpGet("UcretUzerindenYuzde")]
    public IActionResult UcretUzerindenYuzde(decimal ucret, decimal yuzde)
    {
        if (ucret < _minimumUcret || ucret > _maksimumUcret)
        {
            return BadRequest(new { Message = $"Ücret {_minimumUcret} ile {_maksimumUcret} arasında olmalıdır." });
        }

        decimal hesaplananTutar = ucret * (yuzde / 100);
        return Ok(new { HesaplananTutar = hesaplananTutar, Message = $"Ücretin %{yuzde} kadarı: {hesaplananTutar:C2}" });
    }

    // Özet ücret bilgisi endpointi
    [HttpGet("UcretBilgisi")]
    public IActionResult UcretBilgisi(decimal ucret)
    {
        bool isValid = ucret >= _minimumUcret && ucret <= _maksimumUcret;
        string message = isValid ? "Ücret geçerlidir." : $"Ücret {_minimumUcret} ile {_maksimumUcret} arasında olmalıdır.";

        return Ok(new { Ucret = ucret, IsValid = isValid, Message = message });
    }
}
