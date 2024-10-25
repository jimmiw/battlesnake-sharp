namespace SnakeGame.Requests;

using Models;
using Models.Boards;
using Models.Snakes;

public class GameRequest
{
    public Game Game { get; set; }
    
    public int Turn { get; set; }

    public Board Board { get; set; }

    public Snake You { get; set; }
}