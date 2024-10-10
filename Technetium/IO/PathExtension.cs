using System.Text;

namespace Technetium.IO;

public static class PathExtension
{
    /// <summary>
    /// An encapsulation of Path.GetFileName
    /// </summary>
    /// <param name="path"></param>
    /// <param name="withExtensionName"></param>
    /// <returns></returns>
    public static string GetFileName(this string path, bool withExtensionName = true)
    {
        return withExtensionName ? Path.GetFileName(path) : Path.GetFileNameWithoutExtension(path);
    }

    /// <summary>
    /// Encapsulation of new FileInfo()
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static FileInfo ToFileInfo(this string fileName)
    {
        return new FileInfo(fileName);
    }

    /// <summary>
    /// Encapsulation of new DirectoryInfo()
    /// </summary>
    /// <param name="directoryName"></param>
    /// <returns></returns>
    public static DirectoryInfo ToDirectoryInfo(this string directoryName)
    {
        return new DirectoryInfo(directoryName);
    }

    #region Write

    public static void WriteAllText(this FileInfo file, string text)
    {
        File.WriteAllText(file.FullName, text, new UTF8Encoding(false));
    }
    
    public static void WriteAllBytes(this FileInfo file, byte[] bytes)
    {
        File.WriteAllBytes(file.FullName, bytes);
    }
    
    public static void WriteAllLine(this FileInfo file, IEnumerable<string> lines)
    {
        File.WriteAllLines(file.FullName, lines, new UTF8Encoding(false));
    }
    
    public static async Task WriteAllTextAsync(this FileInfo file, string text)
    {
        await File.WriteAllTextAsync(file.FullName, text, new UTF8Encoding(false));
    }
    
    public static async Task WriteAllBytesAsync(this FileInfo file, byte[] bytes)
    {
        await File.WriteAllBytesAsync(file.FullName, bytes);
    }
    
    
    public static async Task WriteAllLineAsync(this FileInfo file, IEnumerable<string> lines)
    {
        await File.WriteAllLinesAsync(file.FullName, lines, new UTF8Encoding(false));
    }

    #endregion

    #region Read

    public static string ReadAllText(this FileInfo fileInfo)
    {
        return File.ReadAllText(fileInfo.FullName, new UTF8Encoding(false));
    }
    
    public static string[] ReadAllLines(this FileInfo fileInfo)
    {
        return File.ReadAllLines(fileInfo.FullName, new UTF8Encoding(false));
    }

    public static byte[] ReadAllBytes(this FileInfo fileInfo)
    {
        return File.ReadAllBytes(fileInfo.FullName);
    }
    
    public static async Task<string> ReadAllTextAsync(this  FileInfo fileInfo)
    {
        return await File.ReadAllTextAsync(fileInfo.FullName, new UTF8Encoding(false));
    }
    
    public static async Task<string[]> ReadAllLinesAsync(this FileInfo fileInfo)
    {
        return await File.ReadAllLinesAsync(fileInfo.FullName, new UTF8Encoding(false));
    }
    
    public static async Task<byte[]> ReadAllBytesAsync(this FileInfo fileInfo)
    {
        return await File.ReadAllBytesAsync(fileInfo.FullName);
    } 

    #endregion
}