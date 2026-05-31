using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordleAPI.Application.DTOs;
using WordleAPI.Application.Interfaces;

namespace WordleAPI.API.Controllers;

[ApiController]
[Route("api/games")]
[Authorize]
[Produces("application/json")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly ILogger<GamesController> _logger;

    public GamesController(IGameService gameService, ILogger<GamesController> logger)
    {
        _gameService = gameService;
        _logger = logger;
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("start")]
    [ProducesResponseType(typeof(StartGameResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> StartGame()
    {
        try
        {
            var response = await _gameService.StartGameAsync(GetUserId());
            return CreatedAtAction(nameof(StartGame), response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting game");
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpPost("guess")]
    [ProducesResponseType(typeof(GuessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MakeGuess([FromBody] GuessRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var response = await _gameService.MakeGuessAsync(request, GetUserId());
            return Ok(response);
        }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        catch (UnauthorizedAccessException ex) { return Forbid(); }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
        catch (InvalidOperationException ex) { return UnprocessableEntity(new { error = ex.Message }); }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing guess");
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyGames()
    {
        var games = await _gameService.GetUserGamesAsync(GetUserId());
        return Ok(games);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GameDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGameById(Guid id)
    {
        try
        {
            var game = await _gameService.GetGameByIdAsync(id, GetUserId());
            return Ok(game);
        }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        catch (UnauthorizedAccessException) { return Forbid(); }
    }
}