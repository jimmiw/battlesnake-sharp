namespace SnakeGame.Engines;

public class EngineType
{
    public const string Standard = "standard";
    public const string Royale = "royale";
    public const string Constrictor = "constrictor";
    
    public static string parseEngineType(string engineType)
    {
        return engineType switch
        {
            Standard => "standard",
            Royale => "royale",
            Constrictor => "constrictor",
            _ => throw new ArgumentOutOfRangeException(nameof(engineType), engineType, null)
        };
    }
}