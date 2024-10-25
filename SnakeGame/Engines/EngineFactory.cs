namespace SnakeGame.Engines;

public class EngineFactory
{
    public static IEngine CreateEngine(string engineType)
    {
        return engineType switch
        {
            EngineType.Classic => new ClassicEngine(),
            EngineType.Royale => new RoyaleEngine(),
            EngineType.Constrictor => new ConstrictorEngine(),
            _ => throw new ArgumentOutOfRangeException(nameof(engineType), engineType, null)
        };
    }
}