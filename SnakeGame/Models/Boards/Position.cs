using static SnakeGame.Models.Boards.Direction;

namespace SnakeGame.Models.Boards;

public class Position
{
    public int X { get;}
    
    public int Y { get; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    /// <summary>
    /// Create a new position by adding two positions together
    /// </summary>
    public static Position operator +(Position a, Position b)
    {
        return new Position(a.X + b.X, a.Y + b.Y);
    }
    
    /// <summary>
    /// Use a direction to change a given position
    /// </summary>
    public static Position operator +(Position a, Direction direction)
    {
        return direction switch
        {
            Up => new Position(a.X, a.Y - 1),
            Down => new Position(a.X, a.Y + 1),
            Left => new Position(a.X - 1, a.Y),
            Right => new Position(a.X + 1, a.Y),
            _ => throw new ArgumentOutOfRangeException($"Given direction:{direction} is not valid")
        };
    }

    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}