namespace AoC;

public static class Helper
{
    public static string GetFilesDir() => Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "files");
}
