using SnakeGame.Models.RulesetSettings;

namespace SnakeGame.Models;

public class Ruleset
{
    public string Name { get; }
    
    public string Version { get; }
    
    public StandardSettings Settings { get; }
    
    public Ruleset(string name, string version, StandardSettings settings)
    {
        Name = name;
        Version = version;
        Settings = settings;
    }
}