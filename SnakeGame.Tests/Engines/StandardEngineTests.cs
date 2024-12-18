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
        _engine = new StandardEngine();
    }
    
    [Fact]
    public async Task FindMove_ShouldReturnADirection()
    {
        // Arrange
        var game = new Game();
        var turn = 1;
        var board = new Board();
        var you = new Snake();
        
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
        var game = new Game();
        var turn = 1;
        var board = new Board();
        var you = new Snake();
        
        // Act
        var result = await _engine.FindMove(game, turn, board, you);
        
        // Assert
        Assert.IsType<Direction>(result);
        Assert.IsType<String>(result.ToString());
    }
}