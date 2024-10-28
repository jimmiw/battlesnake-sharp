namespace SnakeGame.Engines;

public class EngineFactory
{
    public static IEngine GetEngine(string engineType)
    {
        return engineType switch
        {
            EngineType.Standard => new StandardEngine(),
            EngineType.Royale => new RoyaleEngine(),
            EngineType.Constrictor => new ConstrictorEngine(),
            _ => throw new ArgumentOutOfRangeException(nameof(engineType), engineType, null)
        };
    }
}