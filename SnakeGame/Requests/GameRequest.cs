namespace SnakeGame.Requests;

using Models;
using Models.Boards;
using Models.Snakes;

public class GameRequest
{
    public Game Game { get; }
    
    public int Turn { get; }

    public IBoard Board { get; }

    public ISnake You { get; }
    
    public GameRequest(Game game, int turn, IBoard board, ISnake you)
    {
        Game = game;
        Turn = turn;
        Board = board;
        You = you;
    }
}