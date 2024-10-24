namespace SnakeGame.Requests;

using Models;
using Models.Boards;
using Models.Snakes;

public class GameRequest
{
    public Game Game { get; }
    
    public int Turn { get; }

    public Board Board { get; }

    public Snake You { get; }
    
    public GameRequest(Game game, int turn, Board board, Snake you)
    {
        Game = game;
        Turn = turn;
        Board = board;
        You = you;
    }
}