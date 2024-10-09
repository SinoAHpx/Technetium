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

    public static string? Fetch(this string json)
    {
        var jDocument = JsonDocument.Parse(json);
        return jDocument.RootElement.GetProperty("Name").GetString();
    }
}