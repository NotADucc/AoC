using AoC;
using System.Threading.Tasks.Sources;

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
            var s = display[i];
            int n = display[i].Length;
            int[] dp = new int[n + 1];

            for (int start = n - 1; start >= 0; start--)
            {
                dp[start] = dp[start + 1] + 1;
                for (int end = start; end < n; end++)
                {
                    var sub = s.Substring(start, end + 1 - start);
                    if (patterns.Contains(sub))
                    {
                        dp[start] = Math.Min(dp[start], dp[end + 1]);
                    }
                }
            }

            res_1 += dp[0] == 0 ? 1 : 0;
            res_2 += Rec(patterns, display[i]);
        }

        return (res_1, res_2);
    }
}