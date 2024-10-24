using SnakeGame.Models.Snakes;

namespace SnakeGame.Models.Boards;

public class Board
{
    public int Height { get; set; }
    
    public int Width { get; set; }

    public List<Position>? Food { get; set; }
    
    public List<Position>? Hazards { get; set; }

    public List<Snake>? Snakes { get; set; }
}