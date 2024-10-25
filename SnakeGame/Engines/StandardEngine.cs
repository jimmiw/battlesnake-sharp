
namespace SnakeGame.Engines;

using Models;
using Models.Boards;
using Models.Snakes;

public class StandardEngine : IEngine
{
    public async Task<Direction> FindMove(Game game, int turn, Board board, Snake you)
    {
        return new Direction(Direction.Down);
    }
}