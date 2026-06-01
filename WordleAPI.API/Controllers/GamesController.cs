using Microsoft.AspNetCore.Mvc;
using WordleAPI.Application.DTOs;
using WordleAPI.Application.Interfaces;

namespace WordleAPI.API.Controllers;

[ApiController]
[Route("api/games")]
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

    /// <summary>
    /// Start a new Wordle game.
    /// </summary>
    /// <returns>New game details including gameId to use for guesses.</returns>
    [HttpPost("start")]
    [ProducesResponseType(typeof(StartGameResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> StartGame()
    {
        try
        {
            _logger.LogInformation("Starting a new game");
            var response = await _gameService.StartGameAsync();
            return CreatedAtAction(nameof(StartGame), response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting game");
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    /// <summary>
    /// Make a guess in an existing Wordle game.
    /// </summary>
    /// <param name="request">GameId and 5-letter word to guess.</param>
    /// <returns>Letter-by-letter result of the guess.</returns>
    [HttpPost("guess")]
    [ProducesResponseType(typeof(GuessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> MakeGuess([FromBody] GuessRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            _logger.LogInformation("Processing guess '{Word}' for game {GameId}", request.Word, request.GameId);
            var response = await _gameService.MakeGuessAsync(request);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Game not found: {Message}", ex.Message);
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Invalid guess input: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Invalid game state: {Message}", ex.Message);
            return UnprocessableEntity(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing guess");
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }
}
