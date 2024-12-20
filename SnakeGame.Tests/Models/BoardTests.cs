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
    
    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 1, 2)]
    [InlineData(0, 0, 2, 2, 4)]
    [InlineData(1, 0, 5, 5, 9)]
    public void GetDistance_ShouldReturnCorrectDistance_WhenPositionsAreDifferent(int x1, int y1, int x2, int y2, int expected)
    {
        // Arrange
        var position1 = new Position(x1, y1);
        var position2 = new Position(x2, y2);
        
        // Act
        var result = Board.GetDistance(position1, position2);
        
        // Assert
        Assert.Equal(expected, result);
    }
}