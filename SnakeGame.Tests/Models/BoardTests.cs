using SnakeGame.Models.Boards;

namespace SnakeGame.Tests.Models;

public class BoardTests
{
    [Fact]
    public void IsOutOfBounds_ShouldReturnTrue_WhenPositionIsOutOfBounds()
    {
        // Arrange
        var board = new Board();
        board.Width = 11;
        board.Height = 11;
        
        // Act
        
        // safe
        var result = board.IsOutOfBounds(new Position(3, 3));
        // out of bounds too high
        var result2 = board.IsOutOfBounds(new Position(11, 11));
        // out of bounds below 0
        var result3 = board.IsOutOfBounds(new Position(-1, 0));
        // safe - largest possible position
        var result4 = board.IsOutOfBounds(new Position(10, 10));
        // safe - smallest possible position
        var result5 = board.IsOutOfBounds(new Position(0, 0));
        
        // Assert
        Assert.False(result);
        Assert.True(result2);
        Assert.True(result3);
        Assert.False(result4);
        Assert.False(result5);
    }
}