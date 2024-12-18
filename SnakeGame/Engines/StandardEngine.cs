
namespace SnakeGame.Engines;

using Models;
using Models.Boards;
using Models.Snakes;

public class StandardEngine : IEngine
{
    private readonly ILogger<StandardEngine> logger;
    private static readonly Random RandomGenerator = new();
    
    public StandardEngine(ILogger<StandardEngine> logger)
    {
        this.logger = logger;
    }
    
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

        if (maxAttempts == 0)
        {
            logger.LogWarning("Could not find a valid direction after 10 attempts!");
        }
        else
        {
            logger.LogInformation($"Found direction:{direction} in {9 - maxAttempts} attempts");
        }
        
        return direction;
    }
    
    private Direction GetRandomDirection()
    {
        return Board.ValidDirections.ElementAt(RandomGenerator.Next(Board.ValidDirections.Count()));
    }
}