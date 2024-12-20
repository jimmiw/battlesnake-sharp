using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnakeGame.Controllers;
using SnakeGame.Engines;
using SnakeGame.Models;
using SnakeGame.Models.Boards;
using SnakeGame.Models.Snakes;
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
        var board = A.Fake<Board>();
        var you = A.Fake<Snake>();
        you.Id = "you";
        you.Head = new Position(0, 0);
        you.Body = new List<Position>
        {
            new(1, 0),
            new(2, 0)
        };
        var snakes = new List<Snake>();
        snakes.Add(you);
        var otherSnake = A.Fake<Snake>();
        otherSnake.Id = "otherSnake";
        otherSnake.Head = new Position(1, 1);
        otherSnake.Body = new List<Position>
        {
            new(1, 2),
            new(1, 3)
        };
        snakes.Add(otherSnake);
        board.Snakes = snakes;
        
        var requestBody = A.Fake<GameRequest>();
        requestBody.Game = game;
        requestBody.Board = board;
        requestBody.You = you;

        // Act
        var result = await controller.Move(requestBody, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult);
    }
}