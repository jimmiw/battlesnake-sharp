namespace SnakeGame.Models.Snakes;

using Boards;

public class Snake
{
    public string? Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Health { get; set; }
    
    public List<Position>? Body { get; set; } = [];
    
    public string? Latency { get; set; }

    public Position Head { get; set; } = new Position(0, 0);

    public int Length { get; set; } = 1;
    
    public string? Shout { get; set; }
    
    public Customization? Customizations { get; set; }
    
    /// <summary>
    /// Checks if the snake's body is on the given position
    /// </summary>
    public bool IsOnPosition(Position position)
    {
        // checking the head first
        if (position == Head)
        {
            return true;
        }

        // checking the body last, as it's most intensive
        return Body.Contains(position);
    }
}