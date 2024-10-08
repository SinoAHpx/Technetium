using Technetium.Text.String;

namespace Technetium.Runner;

class Program
{
    static void Main(string[] args)
    {
        List<int> i = [1, 2, 3, 4];
        i.Output();
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