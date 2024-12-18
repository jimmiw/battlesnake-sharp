
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
        var attempts = 1;
        
        logger.LogInformation($"Current position: {you.Head}");
        
        // checking if the new direction is out of bounds or if it's on the snake's body
        while (attempts < maxAttempts && (board.IsOutOfBounds(you.Head + direction) || you.IsOnPosition(you.Head + direction)))
        {
            direction = GetRandomDirection();
            // decrement the number of attempts, so we don't get stuck in an infinite loop
            attempts++;
        }

        if (attempts == maxAttempts)
        {
            logger.LogWarning("Could not find a valid direction after 10 attempts!");
        }
        else
        {
            logger.LogInformation($"Found direction:{direction} in {attempts} attempts");
        }
        
        logger.LogInformation($"New position: {you.Head + direction}");
        
        return direction;
    }
    
    private Direction GetRandomDirection()
    {
        return Board.ValidDirections.ElementAt(RandomGenerator.Next(Board.ValidDirections.Count()));
    }
}