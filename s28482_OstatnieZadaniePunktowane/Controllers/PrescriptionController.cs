using Microsoft.AspNetCore.Mvc;
using s28482_OstatnieZadaniePunktowane.DTOs;
using s28482_OstatnieZadaniePunktowane.Services;

namespace s28482_OstatnieZadaniePunktowane.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateRequestDTO dto)
    {
        try
        {
            await service.AddPrescriptionAsync(dto); // używa domyślnego doctorId = 1
            return Ok(new { message = "Recepta została pomyślnie dodana." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
}