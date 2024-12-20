
namespace SnakeGame.Engines;

using Models;
using Models.Boards;
using Models.Snakes;

public class StandardEngine : IEngine
{
    private ILogger logger;
    private readonly Random RandomGenerator = new();
    private readonly PositionComparer PositionComparer = new();
    
    public StandardEngine(ILogger<StandardEngine> logger)
    {
        this.logger = logger;
    }
    
    public async Task<Direction> FindMove(Game game, int turn, Board board, Snake you)
    {
        logger.LogInformation($"Current position: {you.Head}");

        var safePostions = GetSafePostions(board, you.Head);
        var position = FindBestMove(safePostions, board, you);

        // this block is just for debugging purposes :)
        if (position is null)
        {
            logger.LogWarning("Could not find a safe position to move to, using random direction");
        }

        // if no position was found, we will return a random position
        position ??= GetRandomPosition(you.Head, safePostions);

        logger.LogInformation($"New position: {position}, using direction:{you.Head.GetDirectionTo(position)}");
        
        // returning the direction to move to
        return you.Head.GetDirectionTo(position);
    }

    /// <summary>
    /// Finds a list of valid positions to move to, using the current board and the given snake
    /// </summary>
    private IEnumerable<Position> GetSafePostions(Board board, Position position)
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

        // convert the directions to positions
        return validDirections.Select(d => (position + d)).ToList();
    }

    public Position? FindBestMove(IEnumerable<Position> safePositions, Board board, Snake you)
    {
        Position? bestMove = null;
        
        // if the snake is in need of food, find food!
        // also, if the snake is too small, find food!
        if (you.Health <= 30 || you.Length < 10)
        {
            logger.LogInformation($"Finding food! Health:{you.Health}, Length:{you.Length}");
            
            bestMove = GetClosestFood(board.Food, you.Head);
            
            // since we need food now, we are returning fast!
            if (bestMove is not null)
            {
                var distanceToFood = Board.GetDistance(you.Head, bestMove);
                // the max distance we want to move, when finding food is 20, so if the distance is greater, we will limit it
                if (distanceToFood > 20)
                {
                    distanceToFood = 20;
                }
                
                bestMove = GetMoveToDestination(you.Head, bestMove, [], board, distanceToFood);
                logger.LogInformation($"Going towards food, next move is from {you.Head} to {bestMove}");
            }
        }
        
        // TODO: dominate middle?
        
        // TODO: attack other snakes?
        
        // TODO: Use floodfill to check for space, if we take the currently found move
        // bestMove = GetSafeMove(bestMove, you.Head, board);

        // nothing to do, just return a random direction
        return bestMove ?? GetRandomPosition(you.Head, safePositions);
    }

    private Position? GetSafeMove(Position? bestMove, Position youHead, Board board)
    {
        // if (bestMove is null)
        // {
        //     return null;
        // }
        //
        // var safePositions = GetSafePostions(board, youHead);
        // if (safePositions.Contains(bestMove.Value, PositionComparer))
        // {
        //     return bestMove;
        // }
        
        return null;
    }

    /// <summary>
    /// Finds the food closest to the given position
    /// </summary>
    private Position? GetClosestFood(IEnumerable<Position> foodPositions, Position currentPosition)
    {
        Position? closestFood = null;
        int distance = int.MaxValue;

        if (foodPositions == null || !foodPositions.Any())
        {
            logger.LogInformation("No food found");
            return null;
        }
        
        foreach (var foodPosition in foodPositions)
        {
            var distanceToFood = Board.GetDistance(currentPosition, foodPosition);
            if (distanceToFood < distance)
            {
                distance = distanceToFood;
                closestFood = foodPosition;
            }
        }
        
        logger.LogInformation($"Closest food found at {closestFood}, distance:{distance}");
        
        return closestFood;
    }

    /// <summary>
    /// Finds the direction towards the given position, using the list of valid directions only.
    /// We can use the given maxSteps to determine if we have "enough" moves left to reach the position.
    /// </summary>
    private Position? GetMoveToDestination(Position currentPosition, Position endPosition, IEnumerable<Position> route, Board board, int maxDepth)
    {
        logger.LogInformation($"GetMoveToDestination: {currentPosition} -> {endPosition}, maxDepth:{maxDepth}");
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
        
        var adjacentPositions = currentPosition.GetAdjacentPositions().ToList();
        adjacentPositions = adjacentPositions.OrderBy(p => p.GetDistance(endPosition)).ToList();
        
        // if (adjacentPositions.Contains(endPosition, PositionComparer))
        // {
        //     return endPosition;
        // }

        foreach (var possibleMove in adjacentPositions)
        {
            var safePositions = GetSafePostions(board, currentPosition);
            // if the position is safe and not already in the route, we will add it to the route
            if (safePositions.Any() && ! route.Contains(possibleMove, PositionComparer))
            {
                // adding the possbie move to the route (because we need this, to check if we are going back to the same position later on)
                route = route.Append(possibleMove);
                
                // finding the next move, using the possible move as the new current position
                var nextMove = GetMoveToDestination(possibleMove, endPosition, route, board, maxDepth-1);
                
                // if we can't move further, we will remove the last position from the route
                if (nextMove is null || ! safePositions.Contains(nextMove, PositionComparer))
                {
                    route = route.SkipLast(1);
                }
                else
                {
                    logger.LogInformation($"Next move found at {nextMove}");
                    return nextMove;
                }
            }   
        }

        return null;
    }

    /// <summary>
    /// Removes the given direction from the list of valid directions
    /// </summary>
    private IEnumerable<Direction> RemoveDirection(Direction direction, IEnumerable<Direction> validDirections)
    {
        return validDirections.Where(d => d != direction).ToList();
    }

    /// <summary>
    /// Finds a random direction from the list of valid directions
    /// </summary>
    private Position GetRandomPosition(Position currentPosition, IEnumerable<Position> safePositions)
    {   
        if (! safePositions.Any())
        {
            return currentPosition + Direction.Down;
        }
        // split the list of safe positions, so we can log the positions
        
        logger.LogInformation($"GetRandomPosition: {string.Join(", ", safePositions)} safe positions found");
        
        return safePositions.ElementAt(RandomGenerator.Next(safePositions.Count()));
    }
}