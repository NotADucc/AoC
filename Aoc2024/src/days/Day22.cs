using System.Net.Mime;
using System.Runtime.InteropServices.Marshalling;

namespace AoC.days;

public class Day22 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc22.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadAllLines(file_name)
            .Select(long.Parse)
            .ToArray();

        long MIX(long nm1, long nm2) => nm1 ^ nm2;
        long PRUNE(long num) => num % 16777216;

        for (int i = 0; i < 2000; i++)
        {
            for (int j = 0; j < input.Length; j++)
            {
                var secret = input[j] * 64;
                input[j] = MIX(secret, input[j]);
                input[j] = PRUNE(input[j]);

                secret = input[j] / 32;
                input[j] = MIX(secret, input[j]);
                input[j] = PRUNE(input[j]);

                secret = input[j] * 2048;
                input[j] = MIX(secret, input[j]);
                input[j] = PRUNE(input[j]);
            }
        }

        for (int i = 0; i < input.Length; i++)
        {
            res_1 += input[i];
        }

        return (res_1, res_2);
    }
}