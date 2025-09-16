using System.Reflection;
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
        return Path.Combine(absolute_path, "AoCInput", year);
    }
    public static string GetOutputFilesDir(bool get_relative = true, [CallerFilePath] string callerFilePath = "")
    {
        string absolute_path = GetBaseDir();
        string base_relative_path = Directory.GetParent(callerFilePath).FullName;
        string year = get_relative
            ? base_relative_path[base_relative_path.LastIndexOf(@"\")..][1..]
            : string.Empty;
        return Path.Combine(absolute_path, "AoCOutput", year);
    }
    public static int CharToDigit(this char ch) => ch - '0';
    public static char DigitToChar(this int dig) => (char)(dig % 10 + '0');

    public static (string day, object? result1, object? result2) RunYear(int year, int? day = null)
    {
        string namespaceName = $"AoC._{year}";
        var assembly = Assembly.GetExecutingAssembly();

        var dayTypes = assembly
            .GetTypes()
            .Where(t => t.IsClass && t.Namespace == namespaceName && t.Name.StartsWith("Day"));

        Type dayType = (day is not null
            ? dayTypes.Where(t => t.Name == $"Day{day.Value:D2}").FirstOrDefault()
            : dayTypes.OrderByDescending(t => t.Name).FirstOrDefault()) 
            ?? throw new ArgumentException($"could not find year: {year}");

        var instance = Activator.CreateInstance(dayType);
        var method = dayType.GetMethod("Run") 
            ?? throw new ArgumentException($"could not find year: {year}");
        
        var result = method.Invoke(instance, null);

        object? item1 = null;
        object? item2 = null;

        if (result != null && result.GetType().IsValueType &&
            result.GetType().FullName!.StartsWith("System.ValueTuple"))
        {
            item1 = result.GetType().GetField("Item1")?.GetValue(result);
            item2 = result.GetType().GetField("Item2")?.GetValue(result);
        }

        return (dayType.Name, item1, item2);
    }
}
