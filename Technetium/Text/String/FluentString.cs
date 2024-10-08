using System;
using System.Diagnostics.CodeAnalysis;

namespace Technetium.Text.String;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class FluentString
{
    /// <summary>
    /// Equivalent to a combination of string.IsNullOrEmpty and string.IsNullOrWhiteSpace
    /// </summary>
    /// <param name="origin">The string to check</param>
    /// <returns>True if the string is null or empty or whitespace, otherwise false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(returnValue: false)] this string? origin)
    {
        return string.IsNullOrWhiteSpace(origin);
    }

    /// <summary>
    /// Equivalent to int.TryParse
    /// </summary>
    /// <param name="origin">The string to check</param>
    /// <returns>True if the string is a valid integer, otherwise false</returns>
    public static bool IsInt32(this string? origin)
    {
        return int.TryParse(origin, out _);
    }

    /// <summary>
    /// Equivalent to long.TryParse
    /// </summary>
    /// <param name="origin">The string to check</param>
    /// <returns>True if the string is a valid long, otherwise false</returns>
    public static bool IsInt64(this string? origin)
    {
        return long.TryParse(origin, out _);
    }

    /// <summary>
    /// Equivalent to double.TryParse
    /// </summary>
    /// <param name="origin">The string to check</param>
    /// <returns>True if the string is a valid double, otherwise false</returns>
    public static bool IsDouble(this string? origin)
    {
        return double.TryParse(origin, out _);
    }

    /// <summary>
    /// Equivalent to float.TryParse
    /// </summary>
    /// <param name="origin">The string to check</param>
    /// <returns>True if the string is a valid float, otherwise false</returns>
    public static bool IsFloat(this string? origin)
    {
        return float.TryParse(origin, out _);
    }

    /// <summary>
    /// Equivalent to decimal.TryParse
    /// </summary>
    /// <param name="origin">The string to check</param>
    /// <returns>True if the string is a valid decimal, otherwise false</returns>
    public static bool IsDecimal(this string? origin)
    {
        return decimal.TryParse(origin, out _);
    }

    /// <summary>
    /// Equivalent to int.Parse
    /// </summary>
    /// <param name="origin">The string to parse</param>
    /// <returns>The parsed integer</returns>
    public static int ToInt32(this string origin)
    {
        return int.Parse(origin);
    }

    /// <summary>
    /// Equivalent to long.Parse
    /// </summary>
    /// <param name="origin">The string to parse</param>
    /// <returns>The parsed long</returns>
    public static long ToInt64(this string origin)
    {
        return long.Parse(origin);
    }

    /// <summary>
    /// Convert string to bool
    /// </summary>
    /// <param name="origin">The string to parse</param>
    /// <returns>The parsed bool</returns>
    public static bool ToBool(this string origin)
    {
        return bool.Parse(origin);
    }

    /// <summary>
    /// Remove all the invalid characters in a particular string
    /// </summary>
    /// <param name="origin">The string to remove invalid path characters from</param>
    /// <returns>The string with invalid path characters removed</returns>
    public static string RemoveInvalidPathChars(this string origin)
    {
        return string.Concat(origin.Where(c => !Path.GetInvalidPathChars().Contains(c)));
    }

    /// <summary>
    /// Remove all the invalid characters in a particular string
    /// </summary>
    /// <param name="origin">The string to remove invalid file name characters from</param>
    /// <returns>The string with invalid file name characters removed</returns>
    public static string RemoveInvalidFileNameChars(this string origin)
    {
        return Path.GetInvalidFileNameChars()
            .Aggregate(origin, (current, c) => current.Replace(c.ToString(), string.Empty));
    }

    /// <summary>
    /// Equivalent to Path.Combine
    /// </summary>
    /// <param name="origin">The string to combine</param>
    /// <param name="paths">The strings to combine</param>
    /// <returns>The combined string</returns>
    public static string CombinePath(this string origin, params string[] paths)
    {
        var list = new List<string> { origin };

        return list.Concat(paths).Aggregate(Path.Combine);
    }

    /// <summary>
    /// string.Join but is an extension method
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="separator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The joined string</returns>
    public static string JoinToString<T>(this IEnumerable<T> origin, string separator)
    {
        return string.Join(separator, origin.Select(x => x!.ToString()));
    }

    /// <summary>
    /// string.Join but is an extension method
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="separator"></param>
    /// <param name="selector">specified IEnumerable.Select() selector</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The joined string</returns>
    public static string JoinToString<T>(this IEnumerable<T> origin, string separator, Func<T, string> selector)
    {
        return string.Join(separator, origin.Select(selector));
    }

    /// <summary>
    /// Empty specified string in a string
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="toEmpty">The string to empty</param>
        /// <returns>The string with the specified string empty</returns>
    public static string Empty(this string origin, string toEmpty)
    {
        return origin.Replace(toEmpty, string.Empty);
    }

    /// <summary>
    /// Substring between two string in this specified string
    /// </summary>
    /// <param name="origin">The string to get the substring between</param>
    /// <param name="left">The left string of origin</param>
    /// <param name="right">The right string of origin</param>
    /// <param name="fromLastIndex">Start right string from fromLastIndex index of it</param>
    /// <returns>The substring between the two strings, if any of left or right is not contained in original string, null will be returned.</returns>
    public static string? SubstringBetween(this string origin, string left, string right, bool fromLastIndex = false)
    {
        if (!origin.Contains(left) || !origin.Contains(right))
            return null;

        var iLeft = origin.IndexOf(left, StringComparison.Ordinal) + left.Length;
        var iRight = fromLastIndex
            ? origin.LastIndexOf(right, StringComparison.Ordinal)
            : origin.IndexOf(right, StringComparison.Ordinal);

        return origin.Substring(iLeft, iRight - iLeft);
    }

    /// <summary>
    /// Substring after a string in specified string
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="left"></param>
    /// <param name="fromLastIndex">Start from last eligible string</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string? SubstringAfter(this string origin, string left, bool fromLastIndex = false)
    {
        if (!origin.Contains(left))
            return null;

        var iLeft = fromLastIndex
            ? origin.LastIndexOf(left, StringComparison.Ordinal) + left.Length
            : origin.IndexOf(left, StringComparison.Ordinal) + left.Length;

        return origin[iLeft..];
    }

    /// <summary>
    /// Insert a string after specified string
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="left"></param>
    /// <param name="toInsert"></param>
    /// <param name="each">Insert after each matched string</param>
    /// <param name="last">Start from last matched string</param>
    /// <returns></returns>
    public static string InsertAfter(this string origin, string left, string toInsert, bool each = true, bool last = false)
    {
        if (each)
            return origin.Split([left], StringSplitOptions.None).JoinToString($"{left}{toInsert}");

        var iLeft = last
            ? origin.LastIndexOf(left, StringComparison.Ordinal)
            : origin.IndexOf(left, StringComparison.Ordinal);

        return origin.Insert(iLeft + 1, toInsert);
    }

    /// <summary>
    /// Insert a string before specified string
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="right"></param>
    /// <param name="toInsert"></param>
    /// <param name="each">Insert before each matched string</param>
    /// <param name="last">Start from last matched string</param>
    /// <returns></returns>
    public static string InsertBefore(this string origin, string right, string toInsert, bool each = true, bool last = false)
    {
        if (each)
            return origin.Split([right], StringSplitOptions.None).JoinToString($"{toInsert}{right}");

        var iRight = last
            ? origin.LastIndexOf(right, StringComparison.Ordinal)
            : origin.IndexOf(right, StringComparison.Ordinal);

        return origin.Insert(iRight, toInsert);
    }
}
