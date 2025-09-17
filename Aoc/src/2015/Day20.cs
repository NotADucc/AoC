using System.Collections.Generic;
using System.Numerics;

namespace AoC._2015;

public class Day20 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc20.txt");
        long res_1 = 0, res_2 = 0;

        var input = int.Parse(File.ReadAllText(file_name));
       
        res_1 = get_house(input, 10, int.MaxValue);
        res_2 = get_house(input, 11, 50);

        return (res_1, res_2);
    }

    private long get_house(int target, int factor, int limit)
    {
        target /= factor;
        Span<int> houses = new int[target + 1];

        for (int elve_nr = 1; elve_nr <= target; elve_nr++)
        {
            for (int hs_nr = elve_nr, count = 0; hs_nr <= target && count <= limit; hs_nr += elve_nr, count++)
            {
                houses[hs_nr] += elve_nr;
            }
        }

        for (int i = 1; i < houses.Length; i++)
        {
            if (houses[i] >= target)
            {
                return i;
            }
        }

        return -1;
    }
}