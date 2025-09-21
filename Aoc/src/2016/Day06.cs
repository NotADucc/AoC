using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AoC._2016;

public class Day06 : IRun<string, string>
{
    public (string, string) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc6.txt");

        var corrupt_data = File.ReadAllLines(file_name);
        Dictionary<char, int[]>[] freq = Enumerable.Range(1, corrupt_data[0].Length)
            .Select(i => new Dictionary<char, int[]>())
            .ToArray();

        for (int j = 0; j < corrupt_data.Length; j++)
        {
            string line = corrupt_data[j];
            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (!freq[i].TryAdd(ch, [1, j]))
                    freq[i][ch][0]++;
            }
        }

        var frequent_letters = freq
            .Select(
                x => x.OrderByDescending(kvp => kvp.Value[0])
                .ThenBy(kvp => kvp.Value[1])
                .First()
                .Key
            ).ToArray();

        var infrequent_letters = freq
        .Select(
            x => x.OrderBy(kvp => kvp.Value[0])
            .ThenBy(kvp => kvp.Value[1])
            .First()
            .Key
        ).ToArray();

        return (new string(frequent_letters), new string(infrequent_letters));
    }
}