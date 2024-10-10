using NUnit.Framework;
using System.Threading.Tasks;
using Technetium.Text;

namespace Technetium.Test.Text.String;


public class JsonStringTests
{
    [Test]
    public void Serialize_ValidObject_ReturnsJsonString()
    {
        var testObject = new { Name = "Test", Value = 42 };
        var result = testObject.Serialize();
        Assert.That(result, Is.EqualTo("{\"Name\":\"Test\",\"Value\":42}"));
        
        Assert.That(new FileInfo("aa").Serialize(), Is.Null);
    }
    

    [Test]
    public void Deserialize_ValidJsonString_ReturnsObject()
    {
        var json = "{\"Name\":\"Test\",\"Value\":42}";
        var result = json.Deserialize<TestObject>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("Test"));
        Assert.That(result.Value, Is.EqualTo(42));
    }

    [Test]
    public void Deserialize_InvalidJsonString_ReturnsDefault()
    {
        var json = "Invalid JSON";
        var result = json.Deserialize<TestObject>();
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task DeserializeAsync_ValidJsonString_ReturnsObject()
    {
        var json = "{\"Name\":\"Test\",\"Value\":42}";
        var result = await json.DeserializeAsync<TestObject>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("Test"));
        Assert.That(result.Value, Is.EqualTo(42));
    }

    private class TestObject
    {
        public string? Name { get; set; }
        public int Value { get; set; }
    }
}