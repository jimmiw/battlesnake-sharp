namespace SnakeGame.Engines;

using Models;
using Models.Boards;
using Models.Snakes;

public interface IEngine
{
    /// <summary>
    /// Finds a move for your snake, using the current game state
    /// </summary>
    public string FindMove(Game game, int turn, IBoard board, ISnake you);
}