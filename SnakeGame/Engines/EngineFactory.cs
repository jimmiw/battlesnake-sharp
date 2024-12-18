namespace SnakeGame.Engines;

public class EngineFactory
{
    private readonly StandardEngine standardEngine;
    private readonly RoyaleEngine royaleEngine;
    private readonly ConstrictorEngine constrictorEngine;

    public EngineFactory(StandardEngine standardEngine, RoyaleEngine royaleEngine, ConstrictorEngine constrictorEngine)
    {
        this.standardEngine = standardEngine;
        this.royaleEngine = royaleEngine;
        this.constrictorEngine = constrictorEngine;
    }
    
    public IEngine GetEngine(string engineType)
    {
        return engineType switch
        {
            EngineType.Standard => standardEngine,
            EngineType.Royale => royaleEngine,
            EngineType.Constrictor => constrictorEngine,
            _ => throw new ArgumentOutOfRangeException(nameof(engineType), engineType, null)
        };
    }
}