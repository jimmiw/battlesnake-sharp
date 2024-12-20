
namespace SnakeGame.Engines;

using Models;
using Models.Boards;
using Models.Snakes;

public class StandardEngine : IEngine
{
    private static ILogger logger;
    private static readonly Random RandomGenerator = new();

    private static ILogger GetLogger()
    {
        if (logger != null)
        {
            return logger;
        }
        
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        logger = loggerFactory.CreateLogger(string.Empty);

        return logger;
    }
    
    public async Task<Direction> FindMove(Game game, int turn, Board board, Snake you)
    {
        GetLogger().LogInformation($"Current position: {you.Head}");

        var safePostions = GetSafePostions(board, you.Head);
        var position = FindBestMove(safePostions, board, you);

        // this block is just for debugging purposes :)
        if (position is null)
        {
            GetLogger().LogWarning("Could not find a safe position to move to, using random direction");
        }

        // if no position was found, we will return a random position
        position ??= GetRandomPosition(you.Head, safePostions);

        GetLogger().LogInformation($"New position: {position}, using direction:{you.Head.GetDirectionTo(position)}");
        
        // returning the direction to move to
        return you.Head.GetDirectionTo(position);
    }

    /// <summary>
    /// Finds a list of valid positions to move to, using the current board and the given snake
    /// </summary>
    private static IEnumerable<Position> GetSafePostions(Board board, Position position)
    {
        var validDirections = Board.ValidDirections;
        
        // remove out of bounds directions
        foreach (var d in validDirections)
        {
            if (board.IsOutOfBounds(position + d))
            {
                validDirections = RemoveDirection(d, validDirections);
            }
        }

        // remove directions that would hit other snake bodies
        foreach (var s in board.Snakes)
        {
            foreach (var d in validDirections)
            {
                if (s.IsOnPosition(position + d))
                {
                    validDirections = RemoveDirection(d, validDirections);
                }
            }
        }
        
        // TODO: check for hazards, e.g. walls
        
        GetLogger().LogInformation($"Directions to choose from after checking self, outofbounds and other snakes have been removed: {string.Join(", ", validDirections)}");

        // convert the directions to positions
        return validDirections.Select(d => (position + d)).ToList();
    }

    private Position? FindBestMove(IEnumerable<Position> safePositions, Board board, Snake you)
    {
        Position? destination = null;
        
        // if the snake is in need of food, find food!
        // also, if the snake is too small, find food!
        if (you.Health <= 30 || you.Length < 10)
        {
            GetLogger().LogInformation("Finding food! Health:{you.Health}, Length:{you.Length}");
            
            destination = GetClosestFood(board.Food, you.Head);
            
            // since we need food now, we are returning fast!
            if (destination is not null)
            {
                GetLogger().LogWarning("Food found at {destination} heading there now!");
                var maxFoodDistance = Board.GetDistance(you.Head, destination);
                // the max distance we want to move, when finding food is 20, so if the distance is greater, we will limit it
                if (maxFoodDistance > 20)
                {
                    maxFoodDistance = 20;
                }
                
                destination = GetDirectionTowardsPosition(you.Head, destination, [], board, maxFoodDistance);
            }
        }
        
        // TODO: dominate middle?
        
        // TODO: attack other snakes?

        // nothing to do, just return a random direction
        return destination ?? GetRandomPosition(you.Head, safePositions);
    }
    
    /// <summary>
    /// Finds the food closest to the given position
    /// </summary>
    private static Position? GetClosestFood(IEnumerable<Position> food, Position currentPosition)
    {
        Position? closestFood = null;
        int distance = int.MaxValue;

        if (food == null || !food.Any())
        {
            return null;
        }
        
        foreach (var f in food)
        {
            var d = Board.GetDistance(currentPosition, f);
            if (d < distance)
            {
                distance = d;
                closestFood = f;
            }
        }
        
        return closestFood;
    }

    /// <summary>
    /// Finds the direction towards the given position, using the list of valid directions only.
    /// We can use the given maxSteps to determine if we have "enough" moves left to reach the position.
    /// </summary>
    private static Position? GetDirectionTowardsPosition(Position currentPosition, Position endPosition, IEnumerable<Position> route, Board board, int maxDepth)
    {
        // exit early, if max depth is reached
        if (maxDepth < 0)
        {
            return null;
        }
        
        // if we are already at the position
        if (currentPosition == endPosition)
        {
            if (!route.Any()) // we are already at the position
            {
                return null;
            }
            
            return route.First();
        }
        
        var adjacentPositions = currentPosition.GetAdjacentPositions();

        foreach (var possibleMove in adjacentPositions)
        {
            // if the position is safe and not already in the route, we will add it to the route
            if (GetSafePostions(board, possibleMove).Any() && ! route.Contains(possibleMove))
            {
                // adding the possbie move to the route (because we need this, to check if we are going back to the same position later on)
                route.Append(possibleMove);
                
                // finding the next move, using the possible move as the new current position
                var nextMove = GetDirectionTowardsPosition(possibleMove, endPosition, route, board, --maxDepth);
                
                // if we can't move further, we will remove the last position from the route
                if (nextMove is null)
                {
                    route.SkipLast(1);
                }
                else
                {
                    return nextMove;
                }
            }   
        }

        return null;
    }

    /// <summary>
    /// Removes the given direction from the list of valid directions
    /// </summary>
    private static IEnumerable<Direction> RemoveDirection(Direction direction, IEnumerable<Direction> validDirections)
    {
        return validDirections.Where(d => d != direction).ToList();
    }

    /// <summary>
    /// Finds a random direction from the list of valid directions
    /// </summary>
    private static Position GetRandomPosition(Position currentPosition, IEnumerable<Position> safePositions)
    {   
        if (! safePositions.Any())
        {
            return currentPosition + Direction.Down;
        }
        
        return safePositions.ElementAt(RandomGenerator.Next(safePositions.Count()));
    }
}