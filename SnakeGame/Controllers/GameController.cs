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
    private readonly IEngine engine;

    /// <summary>
    /// Initializes the controller, using a specified engine.
    /// </summary>
    public GameController(IEngine engine, ILogger<GameController> logger)
    {
        this.logger = logger;
        this.engine = engine;
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
            color = "#FF7518",
            head = "bee",
            tail = "pumpkin",
            // NOTE-end
            version = "0.0.2"
        });
    }
    
    [HttpPost("/start")]
    public IActionResult Start()
    {
        logger.LogInformation("/start hit");
        
        return Ok();
    }

    [HttpPost("/move")]
    public IActionResult Move(GameRequest requestBody)
    {
        logger.LogInformation("/move hit");
        
        return Ok(new {
            move = this.engine.FindMove(requestBody.Game, requestBody.Turn, requestBody.Board, requestBody.You),
            shout = "Hello" // optional
        });
    }

    [HttpPost("/end")]
    public IActionResult End()
    {
        logger.LogInformation("/end hit");
        
        return Ok();
    }
}