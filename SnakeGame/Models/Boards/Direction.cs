namespace SnakeGame.Models.Boards;

public class Direction
{
    private readonly string direction;
    
    public static readonly string Up = "up";
    
    public static readonly string Down = "down";
    
    public static readonly string Left = "left";
    
    public static readonly string Right = "right";
    
    public Direction(string direction)
    {
        this.direction = direction;
    }
    
    public override string ToString()
    {
        return direction;
    }
}