using System.Runtime.CompilerServices;

namespace AoC;

public static class Helper
{
    public static string WhereAmI([CallerFilePath] string callerFilePath = "") => callerFilePath;
    public static string GetFilesDir() 
        => Path.Combine(Directory.GetParent(WhereAmI()).Parent.Parent.FullName, "files");
}
