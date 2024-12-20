using System.Text.Json;

namespace SnakeGame.Controllers;

using Requests;
using Engines;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
[Produces("application/json")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> logger;
    private readonly EngineFactory engineFactory;

    public GameController(EngineFactory engineFactory, ILogger<GameController> logger)
    {
        this.logger = logger;
        this.engineFactory = engineFactory;
    }
    
    /// <summary>
    /// Handles the initial WhoAmI request.
    /// </summary>
    [HttpGet]
    public IActionResult WhoAmI()
    {
        return Ok(new {
            apiversion = "1",
            author = "jimmiw",
            // NOTE: use Customization?
            color = "#660000",
            head = "bee",
            tail = "bee",
            // NOTE-end
            version = "0.0.2"
        });
    }
    
    [HttpPost("/start")]
    public async Task<IActionResult> Start()
    {
        logger.LogInformation("/start hit");
        
        return Ok();
    }

    [HttpPost("/move")]
    public async Task<IActionResult> Move(GameRequest requestBody, CancellationToken cancellationToken)
    {
        logger.LogInformation("/move hit");
        
        logger.LogInformation($"Request: {JsonSerializer.Serialize(requestBody)}");
        
        // constructing the engine to use, using the map settings
        var engine = engineFactory.GetEngine(EngineType.parseEngineType(requestBody.Game.Map));

        var move = await engine.FindMove(requestBody.Game, requestBody.Turn, requestBody.Board, requestBody.You);
        
        return Ok(new {
            move = move.ToString().ToLower(),
            shout = $"We'd be moving {move}"
        });
    }

    [HttpPost("/end")]
    public IActionResult End()
    {
        logger.LogInformation("/end hit");
        
        return Ok();
    }
}