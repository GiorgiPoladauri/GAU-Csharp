using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordleAPI.Application.DTOs;
using WordleAPI.Application.Interfaces;

namespace WordleAPI.API.Controllers;

[ApiController]
[Route("api/statistics")]
[Authorize]
[Produces("application/json")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticService _statisticService;

    public StatisticsController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(StatisticResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatistics()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        try
        {
            var stats = await _statisticService.GetStatisticsAsync(userId);
            return Ok(stats);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}