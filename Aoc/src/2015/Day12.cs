using System.Dynamic;
using System.Text.Json;

namespace AoC._2015;

public class Day12 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc12.txt");
        string json = File.ReadAllText(file_name);
        long res_1 = 0, res_2 = 0;
        using JsonDocument doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;
        sum_json_vals(root, ref res_1, string.Empty);
        sum_json_vals(root, ref res_2, "red");

        return (res_1, res_2);
    }

    static void sum_json_vals(JsonElement element, ref long res, string filter)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                if (!string.IsNullOrEmpty(filter))
                { 
                    foreach (var property in element.EnumerateObject())
                    {
                        if (property.Value.ValueKind == JsonValueKind.String 
                            && property.Value.GetString() == filter)
                            return;
                    }
                }
                foreach (var property in element.EnumerateObject())
                {
                    sum_json_vals(property.Value, ref res, filter);
                }
                break;

            case JsonValueKind.Array:
                foreach (var item in element.EnumerateArray())
                {
                    sum_json_vals(item, ref res, filter);
                }
                break;

            default:
                if (long.TryParse(element.ToString(), out long num))
                    res += num;
                break;
        }
    }
}