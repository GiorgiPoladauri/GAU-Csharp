using MedicalBilling.Application.DTOs;
using MedicalBilling.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalBilling.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _service;
    public DoctorsController(IDoctorService service) => _service = service;

    [HttpGet]
    [Authorize(Roles = "admin,doctor")]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    [Authorize(Roles = "admin,doctor")]
    public async Task<IActionResult> GetById(int id)
    {
        var d = await _service.GetByIdAsync(id);
        return d == null ? NotFound(new { message = $"Doctor {id} not found." }) : Ok(d);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] DoctorCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}/stats")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetStats(int id)
        => Ok(await _service.CountDoctorVisitsAsync(id));
}
