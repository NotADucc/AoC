using System.Runtime.CompilerServices;

namespace AoC;

public static class Helper
{
    public static string WhereAmI([CallerFilePath] string callerFilePath = "") => callerFilePath;
    public static string GetBaseDir()
        => Directory.GetParent(WhereAmI()).Parent.Parent.FullName;
    public static string GetInputFilesDir() 
        => Path.Combine(GetBaseDir(), "AoC2024Inputs");

    public static int CharToDigit(this char ch) => ch - '0';
    public static char DigitToChar(this int dig) => (char)(dig % 10 + '0');
}
