namespace BlazingTrails.Tests.Example;

using Bunit;

public class GreeterCsTests : TestContext
{
    [Fact]
    public void SetNameParameter_RenderH1ContainingGreetingWithPassedName()
    {
        //Arrange
        //Act
        var cut = RenderComponent<Greeter>(parameters 
            => parameters.Add(p => p.Name, "Robyn"));
        
        // Assert
        cut.MarkupMatches("<h1>Hey! Robyn. Testing with bUnit is awesome.</h1>");
    }
}