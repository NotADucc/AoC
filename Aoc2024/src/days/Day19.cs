namespace AoC.days;

public class Day19 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc19.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadAllLines(file_name);

        var patterns = input[0]
            .Split(',', StringSplitOptions.TrimEntries)
            .ToHashSet();

        var display = input
            .Skip(2)
            .ToArray();

        var mem = new Dictionary<string, long>();

        long Rec(HashSet<string> patterns, string curr)
        {
            if (curr.Length == 0)
            {
                return 1;
            }
            else
            {
                if (mem.TryGetValue(curr, out long value))
                    return value;

                long score = 0;
                foreach (var pattern in patterns)
                {
                    if (curr.StartsWith(pattern))
                    {
                        score += Rec(patterns, curr.Substring(pattern.Length));
                    }
                }

                mem[curr] = score;

                return mem[curr];
            }
        }

        for (int i = 0; i < display.Length; i++)
        {
            var res = Rec(patterns, display[i]);
            if (res > 0)
            {
                res_1++;
                res_2 += res;
            }
        }

        return (res_1, res_2);
    }
}