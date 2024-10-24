namespace SnakeGame.Models;

using Maps;

public class Game
{
    public string Id { get; set; }
    
    public Ruleset Ruleset { get; set; }
    
    public IMap Map { get; set; }
    
    public int Timeout { get; set; }
    
    public string Source { get; set; }
}