namespace AoC._2025;

public class Day05 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc5.txt");
        long res_1 = 0, res_2 = 0;
        var lines = File.ReadAllLines(file_name);
        (var ranges, var instructions) = init(lines);

        // yea how do u debug an impossible set

        foreach (var ingredient in instructions)
        {
            // wait bf is just fast???????
            for (int idx = 0; idx < ranges.Count && ranges[idx].Start <= ingredient; idx++)
            {
                if (ingredient >= ranges[idx].Start && ingredient <= ranges[idx].End)
                {
                    res_1++;
                    break;
                }
            }
        }

        foreach (var range in ranges)
        {
            res_2 += range.End - range.Start + 1;
        }

        return (res_1, res_2);
    }
    private static (List<Ranges> ranges, List<long> instructions) init(string[] lines) 
    {
        List<Ranges> ans_ranges = [];
        List<long> ans_instructions = [];
        bool has_seen_skip = false;

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                has_seen_skip = true;
                continue;
            }

            if (has_seen_skip)
            {
                ans_instructions.Add(long.Parse(line));
            }
            else
            {
                var range = line.Split("-");
                ans_ranges.Add(new(long.Parse(range[0]), long.Parse(range[1])));
            }
        }

        var sorted_ranges = ans_ranges
            .OrderBy(x => x.Start)
            .ThenBy(x => x.End)
            .ToList();

        ans_ranges.Clear();
        ans_ranges.Add(sorted_ranges[0]);

        for (int i = 1; i < sorted_ranges.Count; i++)
        {
            var prev_range = ans_ranges[^1];
            var current_range = sorted_ranges[i];

            if (current_range.Start <= prev_range.End)
            {
                prev_range.End = Math.Max(prev_range.End, current_range.End);
            }
            else 
            {
                ans_ranges.Add(current_range);
            }
        }

        return (ans_ranges, ans_instructions);
    }
    private class Ranges(long start, long end)
    {
        public long Start { get; set; } = start;
        public long End { get; set; } = end;
    }
}