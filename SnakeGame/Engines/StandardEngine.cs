
namespace SnakeGame.Engines;

using Models;
using Models.Boards;
using Models.Snakes;

public class StandardEngine : IEngine
{
    private static readonly Random RandomGenerator = new();
    
    public async Task<Direction> FindMove(Game game, int turn, Board board, Snake you)
    {
        var direction = GetRandomDirection();
        var maxAttempts = 10;
        
        // checking if the new direction is out of bounds or if it's on the snake's body
        while (maxAttempts > 0 && (board.IsOutOfBounds(you.Head + direction) || you.IsOnPosition(you.Head + direction)))
        {
            direction = GetRandomDirection();
            // decrement the number of attempts, so we don't get stuck in an infinite loop
            maxAttempts--;
        }

        return direction;
    }
    
    private Direction GetRandomDirection()
    {
        return Board.ValidDirections.ElementAt(RandomGenerator.Next(Board.ValidDirections.Count()));
    }
}