using SnakeGame.Engines;

namespace SnakeGame.Tests.Engines;

public class EngineTypeTests
{
    [Fact]
    public void parseEngineType_Standard_ReturnsStandard()
    {
        // Arrange
        var engineType = EngineType.Standard;
        
        // Act
        var result = EngineType.parseEngineType(engineType);
        
        // Assert
        Assert.Equal("standard", result);
    }
    
    [Fact]
    public void parseEngineType_Royale_ReturnsRoyale()
    {
        // Arrange
        var engineType = EngineType.Royale;
        
        // Act
        var result = EngineType.parseEngineType(engineType);
        
        // Assert
        Assert.Equal("royale", result);
    }
    
    [Fact]
    public void parseEngineType_Constrictor_ReturnsConstrictor()
    {
        // Arrange
        var engineType = EngineType.Constrictor;
        
        // Act
        var result = EngineType.parseEngineType(engineType);
        
        // Assert
        Assert.Equal("constrictor", result);
    }
    
    [Fact]
    public void parseEngineType_InvalidEngineType_ThrowsException()
    {
        // Arrange
        var engineType = "invalid";
        
        // Act and Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => EngineType.parseEngineType(engineType));
    }
}