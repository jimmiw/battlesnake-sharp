
namespace SnakeGame.Engines;

using Models;
using Models.Boards;
using Models.Snakes;

public class ClassicEngine : IEngine
{
    public string FindMove(Game game, int turn, Board board, Snake you)
    {
        return Direction.Down;
    }
}