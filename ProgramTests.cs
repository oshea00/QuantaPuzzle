using NUnit.Framework;
namespace QuantaPuzzle.Tests;

[TestFixture]
public class ProgramTests
{
    [Test]
    public void TestGetTripsHome()
    {
        // Arrange
        var program = new Program();
        string planets = "7,8,6,1,1,6,7,7,4"; 
    
        // Act
        List<string> result = program.GetTripsHome(planets);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Contains("61771786"), Is.True);
    }
}

