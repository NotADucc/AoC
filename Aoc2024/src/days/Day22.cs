using System.Net.Mime;
using System.Runtime.InteropServices.Marshalling;

namespace AoC.days;

public class Day22 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc22.txt");
        long res_1 = 0, res_2 = 0;

        long MIX(long nm1, long nm2) => nm1 ^ nm2;
        long PRUNE(long num) => num % 16777216;
        
        var input = File.ReadAllLines(file_name)
            .Select(long.Parse)
            .ToArray();

        var dct = input.ToDictionary(x => x, x => new List<long>());

        foreach (var kvp in dct)
        {
            var prev = kvp.Key;
            for (int i = 0; i < 2000; i++)
            {
                prev = PRUNE(MIX(prev * 64, prev));
                prev = PRUNE(MIX(prev / 32, prev));
                prev = PRUNE(MIX(prev * 2048, prev));
                dct[kvp.Key].Add(prev);
            }
            res_1 += prev;
        }

        return (res_1, res_2);
    }
}