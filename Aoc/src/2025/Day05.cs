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
            var idx = 0;

            while (idx < ranges.Count && ranges[idx].Start <= ingredient)
            {
                if (ingredient >= ranges[idx].Start && ingredient <= ranges[idx].End)
                {
                    res_1++;
                    break;
                }
                // how did i not inf loop
                // wait bf is just fast???????
                idx++;
            }
        }

        // can you make assumptiuons?
        // wel it's ordered
        // but ye....
        // smallest is idx 0 start and biggest idx -1 end

        var last_range = ranges[0];
        res_2 += last_range.End - last_range.Start + 1;
        long biggest_end = last_range.End;

        for (int i = 0; i < ranges.Count; i++)
        {
            var range = ranges[i];
            var offset = biggest_end < range.Start ? 1 : 0;

            biggest_end = Math.Max(biggest_end, range.Start);
            long add = Math.Max(0, range.End - biggest_end + offset);
            biggest_end = Math.Max(biggest_end, range.End);
            res_2 += add;
        }

        return (res_1, res_2);
    }
    private static (List<Ranges> ranges, List<long> instructions) init(string[] lines) 
    {
        List<Ranges> ranges = [];
        List<long> instructions = [];
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
                instructions.Add(long.Parse(line));
            }
            else
            {
                var range = line.Split("-");
                ranges.Add(new(long.Parse(range[0]), long.Parse(range[1])));
            }
        }

        ranges = ranges
            .OrderBy(x => x.Start)
            .ThenBy(x => x.End)
            .ToList();

        return (ranges, instructions);
    }
    private record Ranges(long Start, long End);
}