using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC._2016;

public class Day03 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc3.txt");
        long res_1 = 0, res_2 = 0;

        var dimensions = File
            .ReadAllLines(file_name)
            .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray())
            .ToArray();


        List<int[]> new_dimensions = new();
        for (int i = 0; i < dimensions.Length; i += 3)
        {
            for (int j = 0; j < 3; j++)
            {
                new_dimensions.Add(
                    [dimensions[i][j], dimensions[i + 1][j], dimensions[i + 2][j]]
                );
            }
        }

        res_1 = dimensions
            .Where(is_possible).Count();
        res_2 = new_dimensions
            .Where(is_possible).Count();

        return (res_1, res_2);
    }

    private static bool is_possible(int[] triangle)
    {
        return (
             (triangle[0] + triangle[1] > triangle[2])
          && (triangle[1] + triangle[2] > triangle[0])
          && (triangle[2] + triangle[0] > triangle[1])
        );
    }
}