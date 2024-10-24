using SnakeGame.Models.RulesetSettings;

namespace SnakeGame.Models;

public class Ruleset
{
    public string Name { get; }
    
    public string Version { get; }
    
    public ISettings Settings { get; }
    
    public Ruleset(string name, string version, ISettings settings)
    {
        Name = name;
        Version = version;
        Settings = settings;
    }
}