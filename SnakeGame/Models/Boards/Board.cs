using SnakeGame.Models.Snakes;

namespace SnakeGame.Models.Boards;

public class Board
{
    public int Height { get; set; }
    
    public int Width { get; set; }

    public List<Position>? Food { get; set; }
    
    public List<Position>? Hazards { get; set; }

    public List<Snake>? Snakes { get; set; }
    
    public static readonly IEnumerable<Direction> ValidDirections = new List<Direction>
    {
        Direction.Up,
        Direction.Down,
        Direction.Left,
        Direction.Right
    };
    
    public bool IsOutOfBounds(Position newPosition)
    {
        return IsOutOfBounds(newPosition.X, newPosition.Y, Width, Height);
    }
    
    /// <summary>
    /// Determines if a position is out of bounds, using a minimum of 0 and a maximum of width and height
    /// </summary>
    public bool IsOutOfBounds(int x, int y, int width, int height)
    {
        return x < 0 || y < 0 || x >= width || y >= height;
    }
    
    public static int GetDistance(Position a, Position b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }
}
