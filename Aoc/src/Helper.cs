using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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
        string name_space = $"AoC._{year}";
        var assembly = Assembly.GetExecutingAssembly();

        var day_types = assembly
            .GetTypes()
            .Where(t => t.IsClass && t.Namespace == name_space && t.Name.StartsWith("Day"));

        Type day_type = (day is not null
            ? day_types.FirstOrDefault(t => t.Name == $"Day{day.Value:D2}")
            : day_types.OrderByDescending(t => t.Name).FirstOrDefault())
            ?? throw new ArgumentException($"could not find year: {year}");

        var instance = Activator.CreateInstance(day_type) as IRun
            ?? throw new ArgumentException($"type {day_type.Name} does not implement IRun");

        var (result1, result2) = instance.RunUntyped();
        return (day_type.Name, result1, result2);
    }

    public static (object? result1, object? result2) RunAocDayBasedOnCallerPath([CallerFilePath] string callerFilePath = "")
    {
        string file_name = Path.GetFileName(callerFilePath);
        string base_relative_path = Directory.GetParent(callerFilePath).FullName;
        if (!int.TryParse(base_relative_path[base_relative_path.LastIndexOf(@"\")..][1..], out int year))
            throw new ArgumentException("cant detect year from caller class execution path");


        if (!int.TryParse(Regex.Match(file_name, @"\d+").Value, out int day))
            throw new ArgumentException("cant detect day from caller class execution path");

        (string _, object? resul_t1, object? result_2) = RunYear(year, day);

        return (resul_t1, result_2);
    }
}
