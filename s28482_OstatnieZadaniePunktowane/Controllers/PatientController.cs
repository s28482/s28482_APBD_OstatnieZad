using Microsoft.AspNetCore.Mvc;
using s28482_OstatnieZadaniePunktowane.Services;

namespace s28482_OstatnieZadaniePunktowane.Controllers;


[ApiController]
[Route("patients")]
public class PatientController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        try
        {
            var result = await service.GetPatientDetailsAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(new { error = e.Message });
        }
    }
}