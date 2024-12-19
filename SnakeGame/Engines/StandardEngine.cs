
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
        var attempts = 0;
        
        // checking if the new direction is out of bounds or if it's on the snake's body
        while (attempts < 10 && direction != null && (board.IsOutOfBounds(you.Head + direction) || you.IsOnPosition(you.Head + direction)))
        {
            logger.LogInformation($"position {you.Head + direction} is out of bounds or on the snake's body, testing new direction");
            // direction was not valid, remove from validDirections and try again!
            validDirections = validDirections.Where(d => d != direction).ToList();
            
            logger.LogInformation($"Direction {direction}@{you.Head + direction} was not valid, trying again!");
            direction = GetRandomDirection(validDirections);
            attempts++;
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
        logger.LogInformation($"Directions to choose from: {string.Join(", ", validDirections)}");
        
        if (validDirections.Count() == 0)
        {
            return null;
        }
        
        return validDirections.ElementAt(RandomGenerator.Next(validDirections.Count()));
    }
}