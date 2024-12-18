namespace SnakeGame.Requests;

using Models;
using Models.Boards;
using Models.Snakes;

public class GameRequest
{
    public Game Game { get; set; } = new();

    public int Turn { get; set; } = 1;

    public Board Board { get; set; } = new();

    public Snake You { get; set; } = new();
}