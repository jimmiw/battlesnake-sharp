namespace SnakeGame.Models.Boards;

public class PositionComparer : IEqualityComparer<Position>
{
    public bool Equals(Position? x, Position? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        return x == y;
    }

    public int GetHashCode(Position obj)
    {
        return HashCode.Combine(obj.X, obj.Y);
    }
}