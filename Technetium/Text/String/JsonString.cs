using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Technetium.Text.String;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class JsonString
{
    /// <summary>
    /// Serialize an object to a json string, if object is null, null will be returned
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="indented">if true, the json will be indented</param>
    /// <param name="ignoreNullValues">if true, null values will be ignored </param>
    /// <returns></returns>
    public static string? Serialize<T>(this T obj, bool indented = false, bool ignoreNullValues = false)
    {
        try
        {
            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = indented,
                DefaultIgnoreCondition = ignoreNullValues ? JsonIgnoreCondition.WhenWritingNull : JsonIgnoreCondition.Never
            });
            return json;
        }
        catch
        {
            return null;
        }
    }
    
    /// <summary>
    /// Serialize an object to a json string, if object is null, null will be returned
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="indented">if true, the json will be indented</param>
    /// <param name="ignoreNullValues">if true, null values will be ignored </param>
    /// <returns></returns>
    public static async Task<string?> SerializeAsync<T>(this T obj, bool indented = false, bool ignoreNullValues = false)
    {
        try
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, obj, new JsonSerializerOptions
            {
                WriteIndented = indented,
                DefaultIgnoreCondition = ignoreNullValues ? JsonIgnoreCondition.WhenWritingNull : JsonIgnoreCondition.Never
            });
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Deserialize a json string to an object, if input bad json, null will be returned
    /// </summary>
    /// <param name="json"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? Deserialize<T>(this string json)
    {
        try
        {
            //should we suppress the exception or should we throw it?
            var obj = JsonSerializer.Deserialize<T>(json);

            return obj;
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// Deserialize a json string to an object, if input bad json, null will be returned
    /// </summary>
    /// <param name="json"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T?> DeserializeAsync<T>(this string json)
    {
        try
        {
            using var stream = new MemoryStream();
            await using var writer = new StreamWriter(stream);
            await writer.WriteAsync(json);
            await writer.FlushAsync();
            stream.Position = 0;
        
            var obj = await JsonSerializer.DeserializeAsync<T>(stream);

            return obj;
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="jNode"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static JsonNode? FetchJsonNode(this JsonNode jNode, string path)
    {
        if (!path.Contains('.'))
        {
            return jNode[path];
        }
        var segments = path.Split('.').ToList();
        var stage = FetchJsonNode(FetchJsonNode(jNode, segments[0])!, path.Empty($"{segments[0]}."));
        segments.RemoveAt(0);
        
        return stage;
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    private static string? Fetch(this string json, string path)
    {
        var jsonNode = JsonNode.Parse(json)!;
        return FetchJsonNode(jsonNode, path)?.ToString();
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static string? FetchString(this string json, string path)
    {
        return Fetch(json, path);
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static bool? FetchBoolean(this string json, string path)
    {
        var result = Fetch(json, path);
        return result != null ? bool.Parse(result) : null;
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static decimal? FetchDecimal(this string json, string path)
    {
        var result = Fetch(json, path);
        return result != null ? decimal.Parse(result) : null;
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static double? FetchDouble(this string json, string path)
    {
        var result = Fetch(json, path);
        return result != null ? double.Parse(result) : null;
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static Guid? FetchGuid(this string json, string path)
    {
        var result = Fetch(json, path);
        return result != null ? Guid.Parse(result) : null;
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static int? FetchInt32(this string json, string path)
    {
        var result = Fetch(json, path);
        return result != null ? int.Parse(result) : null;
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static long? FetchInt64(this string json, string path)
    {
        var result = Fetch(json, path);
        return result != null ? long.Parse(result) : null;
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns>
    public static float? FetchSingle(this string json, string path)
    {
        var result = Fetch(json, path);
        return result != null ? float.Parse(result) : null;
    }

    /// <summary>
    /// Example json: {"name": "x", "nest": {"name": "y"}}, so to get "y", path would be "nest.name"
    /// </summary>
    /// <param name="json"></param>
    /// <param name="path">A combination of property name and dots</param>
    /// <returns></returns> 
    public static DateTime? FetchDateTime(this string json, string path)
    {
        var result = Fetch(json, path);
        return result != null ? DateTime.Parse(result) : null;
    }

}