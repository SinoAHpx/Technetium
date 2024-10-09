using System.Text.Json;
using System.Text.Json.Serialization;
using Technetium.Text.String;

namespace Technetium.Runner;

class Program
{
    static async Task Main(string[] args)
    {
        var obj = new MyClass()
        {
            Name = "AHpx",
            Age = "20",
            Description = "Yet another programmer"
        };

        var o =  await "{\"Name\":\"Test\",\"Age\":\"24\",\"Description\": \"Yet another programmer.\"}".DeserializeAsync<MyClass>();
        Console.WriteLine(o.Name);
    }

    class MyClass
    {
        public string Name { get; set; }

        public string Age { get; set; }

        public string Description { get; set; }
    }
}

public static class Utils
{
    public static void Output<T>(this IEnumerable<T> list)
    {
        Console.WriteLine($"[{list
            .Select(x => x.ToString())
            .Aggregate((c, n) => $"{c}, {n}")}]");
    }
}