using FakeItEasy;
using Microsoft.Extensions.Logging;
using SnakeGame.Engines;
using SnakeGame.Models;
using SnakeGame.Models.Boards;
using SnakeGame.Models.Snakes;

namespace SnakeGame.Tests.Engines;

public class StandardEngineTests
{
    private readonly StandardEngine _engine;

    public StandardEngineTests()
    {
        _engine = new StandardEngine(A.Fake<ILogger<StandardEngine>>());
    }
    
    [Fact]
    public async Task FindMove_ShouldReturnADirection()
    {
        // Arrange
        var turn = 1;
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
        
        // Act
        var result = await _engine.FindMove(game, turn, board, you);
        
        // Assert
        Assert.IsType<Direction>(result);
        Assert.Contains(result, Board.ValidDirections);
    }
    
    [Fact]
    public async Task FindMove_DirectionShouldBeCastableToString()
    {
        // Arrange
        var turn = 1;
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
        
        // Act
        var result = await _engine.FindMove(game, turn, board, you);
        
        // Assert
        Assert.IsType<Direction>(result);
        Assert.IsType<String>(result.ToString());
    }
}