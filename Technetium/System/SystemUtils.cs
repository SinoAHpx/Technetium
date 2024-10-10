using Technetium.Computation;

namespace Technetium.System;

public class SystemUtils
{
    /// <summary>
    /// Get current computer installed memory size in MB
    /// </summary>
    /// <returns></returns>
    public static int GetComputerMemorySize()
    {
        return (int)Math.Truncate(GC.GetGCMemoryInfo().TotalAvailableMemoryBytes.BytesToMegabytes());
    }
}