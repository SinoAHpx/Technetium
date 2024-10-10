namespace Technetium.Collection;

public static class RandomElement
{
    /// <summary>
    /// Return a random element from specific collection.
    /// </summary>
    /// <param name="enumerable"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Random<T>(this IEnumerable<T> enumerable)
    {
        var random = new Random();
        var array = enumerable.ToArray();

        return array.ToArray()[random.Next(array.Length)];
    }
}