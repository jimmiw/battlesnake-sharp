namespace SnakeGame.Models.Snakes;

using Boards;

public class Snake : ISnake
{
    public string? Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Health { get; set; }
    
    public List<Position>? Body { get; set; }
    
    public string? Latency { get; set; }
    
    public Position? Head { get; set; }
    
    public int Length { get; set; }
    
    public string? Shout { get; set; }
    
    public Customization? Customizations { get; set; }
    
    public Snake()
    {
    }
    
    public bool IsOnPosition(Position position)
    {
        throw new NotImplementedException();
    }
}