using System.Collections.Generic;
using System.Numerics;

namespace AoC._2015;

public class Day21 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc21.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadAllText(file_name);

        return (res_1, res_2);
    }
}