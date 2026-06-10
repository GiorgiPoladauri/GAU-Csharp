using System.Security.Claims;
using MedicalBilling.Application.Common;
using MedicalBilling.Application.DTOs;
using MedicalBilling.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalBilling.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    private readonly IVisitService _service;
    public VisitsController(IVisitService service) => _service = service;

    [HttpGet]
    [Authorize(Roles = "admin,doctor")]
    public async Task<IActionResult> GetAll([FromQuery] VisitQueryParams query)
    {
        var result = await _service.GetVisitsAsync(query, GetDoctorId());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "admin,doctor")]
    public async Task<IActionResult> GetById(int id)
    {
        var visit = await _service.GetVisitByIdAsync(id, GetDoctorId());
        return visit == null ? NotFound(new { message = $"Visit {id} not found." }) : Ok(visit);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] VisitCreateDto dto)
    {
        var created = await _service.CreateVisitAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(int id, [FromBody] VisitUpdateDto dto)
    {
        var updated = await _service.UpdateVisitAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteVisitAsync(id);
        return NoContent();
    }

    private int? GetDoctorId()
    {
        if (User.FindFirstValue(ClaimTypes.Role) != "doctor") return null;
        var claim = User.FindFirstValue("DoctorId");
        return claim != null ? int.Parse(claim) : null;
    }
}
