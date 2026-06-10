using MedicalBilling.Application.DTOs;
using MedicalBilling.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalBilling.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _service;
    public PatientsController(IPatientService service) => _service = service;

    [HttpGet]
    [Authorize(Roles = "admin,doctor")]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    [Authorize(Roles = "admin,doctor")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _service.GetByIdAsync(id);
        return p == null ? NotFound(new { message = $"Patient {id} not found." }) : Ok(p);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] PatientCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(int id, [FromBody] PatientUpdateDto dto)
        => Ok(await _service.UpdateAsync(id, dto));

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:int}/billing")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetBilling(int id)
        => Ok(await _service.CalculateTotalBillingForPatientAsync(id));
}
