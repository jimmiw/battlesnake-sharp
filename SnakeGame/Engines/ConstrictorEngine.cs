using SnakeGame.Models;
using SnakeGame.Models.Boards;
using SnakeGame.Models.Snakes;

namespace SnakeGame.Engines;

public class ConstrictorEngine : IEngine
{
    public async Task<Direction> FindMove(Game game, int turn, Board board, Snake you)
    {
        throw new NotImplementedException();
    }
}