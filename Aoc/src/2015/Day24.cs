using System.Collections.Generic;
using System.Xml.Linq;

namespace AoC._2015;

public class Day24 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc24.txt");
        long res_1 = long.MaxValue, res_2 = long.MaxValue;

        var packages = File.ReadAllLines(file_name)
            .Select(int.Parse)
            .ToList();

        res_1 = find_ideal_qe(packages, 3);
        res_2 = find_ideal_qe(packages, 4);

        return (res_1, res_2);
    }

    private static long find_ideal_qe(List<int> packages, int divisor)
    {
        int target = packages.Sum() / divisor;

        int smallest_leg_room = int.MaxValue;
        long best_qe = long.MaxValue;

        void search(int package_idx, int current_sum, int count, long product)
        {
            if (current_sum > target || count > smallest_leg_room) 
                return;

            if (current_sum == target)
            {
                if (count < smallest_leg_room)
                {
                    smallest_leg_room = count;
                    best_qe = product;
                }
                else if (count == smallest_leg_room)
                {
                    best_qe = Math.Min(best_qe, product);
                }
                return;
            }

            if (package_idx >= packages.Count) 
                return;

            int package = packages[package_idx];

            search(package_idx + 1, current_sum + package, count + 1, product * package);
            search(package_idx + 1, current_sum, count, product);
        }

        search(0, 0, 0, 1);
        return best_qe;
    }
}