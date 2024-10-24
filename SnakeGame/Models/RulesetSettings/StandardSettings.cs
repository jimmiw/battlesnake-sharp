namespace SnakeGame.Models.RulesetSettings;

public class StandardSettings : ISettings
{
    public int FoodSpawnChange { get; set; }
    
    public int MinimumFood { get; set; }
    
    public int HazardDamagePerTurn { get; set; }
}
