
namespace SnakeGame.Engines;

using Models;
using Models.Boards;
using Models.Snakes;

public class StandardEngine : IEngine
{
    public string FindMove(Game game, int turn, Board board, Snake you)
    {
        return Direction.Down;
    }
}