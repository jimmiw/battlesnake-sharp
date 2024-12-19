using SnakeGame.Models.Boards;
using SnakeGame.Models.Snakes;

namespace SnakeGame.Tests.Models;

public class SnakeTests
{
    [Fact]
    public void IsOnPosition_ShouldReturnTrue_WhenPositionIsOnHead()
    {
        // Arrange
        var snake = new Snake
        {
            Head = new Position(0, 0),
            Body = new List<Position>
            {
                new(1, 0),
                new(2, 0)
            }
        };
        
        // Act
        var result = snake.IsOnPosition(new Position(0, 0));
        var resultBody = snake.IsOnPosition(new Position(1, 0));
        
        // Assert
        Assert.True(result);
        Assert.True(resultBody);
    }
}