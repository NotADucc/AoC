using System.Collections.Generic;

namespace AoC._2015;

public class Day02 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc2.txt");

        long res_1 = 0, res_2 = 0;
        var data = File.ReadAllLines(file_name)
            .Select(line =>
            {
                var lst = line.Split('x', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                return lst;
            }).ToArray();

        foreach (var line in data)
        {
            long dim_1 = line[0] * line[1];
            long dim_2 = line[1] * line[2];
            long dim_3 = line[2] * line[0];

            res_1 += (2 * dim_1) + (2 * dim_2) + (2 * dim_3) + Math.Min(dim_1, Math.Min(dim_2, dim_3));

            dim_1 = line[0] + line[1];
            dim_2 = line[1] + line[2];
            dim_3 = line[2] + line[0];
            res_2 += (line[0] * line[1] * line[2]) + (2 * Math.Min(dim_1, Math.Min(dim_2, dim_3)));
        }

        // 3798106 too high
        // 4031628
        return (res_1, res_2);
    }
}