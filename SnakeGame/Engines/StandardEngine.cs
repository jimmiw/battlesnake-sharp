
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
        var validDirections = Board.ValidDirections;
        var direction = GetRandomDirection(validDirections);
        
        logger.LogInformation($"Current position: {you.Head}");
        
        // checking if the new direction is out of bounds or if it's on the snake's body
        while (direction != null && (board.IsOutOfBounds(you.Head + direction) || you.IsOnPosition(you.Head + direction)))
        {
            // direction was not valid, remove from validDirections and try again!
            validDirections = validDirections.Where(d => d != direction);
            
            logger.LogInformation($"direction {direction} was not valid!");
            direction = GetRandomDirection(validDirections);
        }

        if (direction == null)
        {
            logger.LogWarning("Could not find a valid direction");
            // setting a default direction if none was found
            direction = Direction.Down;
        }
        
        logger.LogInformation($"New position: {you.Head + direction}, using direction:{direction}");

        return direction ?? Direction.Down; // this is just to keep the compiler happy
    }
    
    private Direction? GetRandomDirection(IEnumerable<Direction> validDirections)
    {
        return validDirections.ElementAt(RandomGenerator.Next(validDirections.Count()));
    }
}