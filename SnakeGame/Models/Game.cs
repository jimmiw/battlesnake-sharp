namespace SnakeGame.Models;

public class Game
{
    public string Id { get; set; } = "";

    public Ruleset? Ruleset { get; set; }

    public string Map { get; set; } = "";
    
    public int Timeout { get; set; }

    public string Source { get; set; } = "";
}