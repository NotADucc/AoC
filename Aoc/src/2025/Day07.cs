using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AoC._2025;

public class Day07 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc7.txt");
        long res_1 = 0, res_2 = 0;
        
        var lines = File.ReadAllLines(file_name)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        int start_width = lines[0].IndexOf('S');
        
        res_1 = rec1(lines, 0, start_width);
        res_2 = rec2(lines, 0, start_width);

        return (res_1, res_2);
    }
    private long rec1(string[] lines, int depth, int width)
    {
        int max_depth = lines.Length;
        int max_width = lines[depth].Length;
        HashSet<string> memo = [];

        void inner(int inner_depth, int inner_width)
        {
            int next_depth = inner_depth + 1;
            string key = $"{inner_depth}|{inner_width}";

            if (next_depth >= max_depth)
            {
                return;
            }

            if (memo.Contains(key))
            {
                return;
            }

            if (lines[next_depth][inner_width] == '.')
            {
                inner(next_depth, inner_width);
            }
            else
            {
                if (inner_width - 1 >= 0)
                {
                    inner(inner_depth, inner_width - 1);
                }

                if (inner_width + 1 < max_width)
                {
                    inner(inner_depth, inner_width + 1);
                }

                memo.Add(key);
            }
        }

        inner(depth, width);
        return memo.Count;
    }
    private long rec2(string[] lines, int depth, int width)
    {
        int max_depth = lines.Length;
        int max_width = lines[depth].Length;
        Dictionary<string, long> memo = [];

        long inner(int inner_depth, int inner_width)
        {
            int next_depth = inner_depth + 1;
            string key = $"{inner_depth}|{inner_width}";

            if (next_depth >= max_depth)
            {
                memo[key] = 1;
                return 1;
            }

            if (memo.TryGetValue(key, out long value))
            {
                return value;
            }

            long split = 0;
            if (lines[next_depth][inner_width] == '.')
            {
                split += inner(next_depth, inner_width);
            }
            else 
            {
                if (inner_width - 1 >= 0)
                {
                    split += inner(inner_depth, inner_width - 1);
                }

                if (inner_width + 1 < max_width)
                {
                    split += inner(inner_depth, inner_width + 1);
                }

                memo[key] = split;
            }

            return split;
        }

        return inner(depth, width);
    }
}