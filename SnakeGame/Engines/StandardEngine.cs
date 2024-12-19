
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
        
        logger.LogInformation($"Current position: {you.Head}");
        var attempts = 0;
        
        // remove out of bounds directions
        foreach (var d in validDirections)
        {
            if (board.IsOutOfBounds(you.Head + d))
            {
                validDirections = RemoveDirection(d, validDirections);
            }
        }
        
        // remove directions that would hit self
        foreach (var d in validDirections)
        {
            if (you.IsOnPosition(you.Head + d))
            {
                validDirections = RemoveDirection(d, validDirections);
            }
        }
        
        // remove directions that would hit other snake bodies
        foreach (var snake in board.Snakes)
        {
            // skip self
            if (snake.Id == you.Id)
            {
                continue;
            }
            
            foreach (var d in validDirections)
            {
                if (snake.IsOnPosition(you.Head + d))
                {
                    validDirections = RemoveDirection(d, validDirections);
                }
            }
        }
        
        logger.LogInformation($"Directions to choose from after checking self, outofbounds and other snakes have been removed: {string.Join(", ", validDirections)}");
        
        if (!validDirections.Any())
        {
            logger.LogWarning("No valid directions found, returning default direction Down");
            return Direction.Down;
        }
        
        var direction = GetRandomDirection(validDirections);

        if (direction == null)
        {
            logger.LogWarning("Could not find a valid direction");
            // setting a default direction if none was found
            direction = Direction.Down;
        }
        
        logger.LogInformation($"New position: {you.Head + direction}, using direction:{direction}");

        return direction ?? Direction.Down; // this is just to keep the compiler happy
    }

    private static IEnumerable<Direction> RemoveDirection(Direction direction, IEnumerable<Direction> validDirections)
    {
        validDirections = validDirections.Where(d => d != direction).ToList();
        return validDirections;
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