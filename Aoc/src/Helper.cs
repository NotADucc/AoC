using System.Runtime.CompilerServices;

namespace AoC;

public static class Helper
{
    public static string WhereAmI([CallerFilePath] string callerFilePath = "") => callerFilePath;
    public static string GetBaseDir()
        => Directory.GetParent(WhereAmI()).Parent.Parent.FullName;
    public static string GetInputFilesDir(bool get_relative = true, [CallerFilePath] string callerFilePath = "")
    {
        string absolute_path = GetBaseDir();
        string base_relative_path = Directory.GetParent(callerFilePath).FullName;
        string year = get_relative
            ? base_relative_path[base_relative_path.LastIndexOf(@"\")..][1..]
            : string.Empty;
        return Path.Combine(absolute_path, "AoCInputs", year);
    }
    public static int CharToDigit(this char ch) => ch - '0';
    public static char DigitToChar(this int dig) => (char)(dig % 10 + '0');
}
