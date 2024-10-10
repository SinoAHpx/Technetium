namespace Technetium.Debug;

public static class DebugExtension
{
    /// <summary>
    /// Encapsulation of Console.WriteLine, also return the result.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="prefix">The text right before the output content.</param>
    /// <param name="suffix">The text right behind the output content.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T OutputConsole<T>(this T obj, string? prefix = null, string? suffix = null)
    {
        Console.WriteLine($"{prefix}{obj}{suffix}");
        
        return obj;
    }
}