using AoC;
using AoC.benchmark;
using AoC.days;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.days
{
    public class Day6Benchmark : BenchmarkAttributes
    {
        [Benchmark(Baseline = true)]
        public int Refactored()
        {
            new Day06().Run();
            return -1;
        }

        [Benchmark]
        public int Old()
        {
            new Day6Old().Run();
            return -1;
        }
    }
}

public class Day6Old : IRun<long, long>
{
    private readonly int[] dirs = [-1, 0, 1, 0, -1, 0];
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc6.txt");

        int res_1 = 0, res_2 = 0;

        int n = 0, m = 0, current_dir = 0;
        HashSet<(int, int)> visited = new HashSet<(int, int)>(), obs = new HashSet<(int, int)>(), added_obs = new HashSet<(int, int)>();
        int[] guard_pos = [0, 0];

        foreach (string line in File.ReadAllLines(file_name))
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Equals('#'))
                {
                    obs.Add((n, i));
                }
                else if (line[i].Equals('^'))
                {
                    guard_pos[0] = n;
                    guard_pos[1] = i;
                }
            }
            n++;
            m = line.Length;
        }
        added_obs.Add((guard_pos[0], guard_pos[1]));

        while (true)
        {
            (int new_r, int new_c) = (guard_pos[0] + dirs[current_dir], guard_pos[1] + dirs[current_dir + 1]);
            if (new_r < 0 || new_r >= n || new_c < 0 || new_c >= m) break;
            if (obs.Contains((new_r, new_c)))
            {
                current_dir = (current_dir + 1) % 4;
            }
            else
            {
                if (visited.Add((guard_pos[0], guard_pos[1]))) res_1++;
                if (added_obs.Add((new_r, new_c)))
                {
                    obs.Add((new_r, new_c));
                    res_2 += DoesLoop(obs, current_dir, guard_pos[0], guard_pos[1], n, m);
                    obs.Remove((new_r, new_c));
                }

                guard_pos[0] = new_r;
                guard_pos[1] = new_c;
            }
        }
        if (visited.Add((guard_pos[0], guard_pos[1]))) res_1++;

        return (res_1, res_2);
    }

    private int DoesLoop(HashSet<(int, int)> obs, int current_dir, int g_r, int g_c, int n, int m)
    {
        current_dir = (current_dir + 1) % 4;
        var visited = new HashSet<(int, int, int)>();
        while (true)
        {
            (int new_r, int new_c) = (g_r + dirs[current_dir], g_c + dirs[current_dir + 1]);
            if (new_r < 0 || new_r >= n || new_c < 0 || new_c >= m) break;
            if (obs.Contains((new_r, new_c)))
            {
                current_dir = (current_dir + 1) % 4;
            }
            else
            {
                if (!visited.Add((g_r, g_c, current_dir))) return 1;
                g_r = new_r;
                g_c = new_c;
            }
        }

        return 0;
    }
}