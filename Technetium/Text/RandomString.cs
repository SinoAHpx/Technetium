using Technetium.Collection;

namespace Technetium.Text;

public static class RandomString
{
    /// <summary>
    /// Generate a random sequence of string from alphabets and numbers.
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Random(int length)
    {
        var table = new[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };
        var result = "";
        while (length > 0)
        {
            result += table.Random();
            length--;
        }

        return result;
    }
}