using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnakeGame.Controllers;
using SnakeGame.Engines;
using SnakeGame.Models;
using SnakeGame.Requests;

namespace SnakeGame.Tests.Controllers;

using FakeItEasy;

public class GameControllerTests
{
    [Fact]
    public async Task Start_ReturnsOkResult()
    {
        // Arrange
        var logger = A.Fake<ILogger<GameController>>();
        var engineFactory = A.Fake<EngineFactory>();
        var controller = new GameController(engineFactory, logger);

        // Act
        var result = await controller.Start();

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.NotNull(okResult);
    }
    
    [Fact]
    public async Task Move_ReturnsGameMove()
    {
        // Arrange
        var logger = A.Fake<ILogger<GameController>>();
        var engineFactory = new EngineFactory(
            new StandardEngine(A.Fake<ILogger<StandardEngine>>()),
            new RoyaleEngine(),
            new ConstrictorEngine()
            );
        
        var controller = new GameController(engineFactory, logger);
        
        var game = A.Fake<Game>();
        game.Map = "standard";
        
        var requestBody = A.Fake<GameRequest>();
        requestBody.Game = game;

        // Act
        var result = await controller.Move(requestBody, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult);
    }
}