using AoC;

public class Day11 : IRun
{
    public (long, long) Run()
    {
        void Process(Dictionary<long, long> f, long val, long count, ref long res)
        {
            if (val == 0)
            {
                f.TryAdd(1, 0);
                f[1] += count;
                return;
            }
            var str = val.ToString();
            var len = str.Length >> 1;
            if ((str.Length & 1) == 0)
            {
                long n1 = long.Parse(str.Substring(0, len)), n2 = long.Parse(str.Substring(len));
                f.TryAdd(n1, 0);
                f.TryAdd(n2, 0);
                f[n1] += count;
                f[n2] += count;
                res += count;
            }
            else
            {
                val *= 2024;
                f.TryAdd(val, 0);
                f[val] += count;
            }
        }

        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc11.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadLines(file_name)
            .Select(line => line.Split().Select(long.Parse))
            .SelectMany(x => x)
            .ToArray();

        res_1 = input.Length;

        Dictionary<long, long> freq = input
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => (long)x.Count());
        
        for (int i = 0; i < 25; i++)
        {
            var new_freq = new Dictionary<long, long>();
            foreach (var kvp in freq)
            {
                Process(new_freq, kvp.Key, kvp.Value, ref res_1);
            }
            freq = new_freq;
        }

        res_2 = res_1;

        for (int i = 0; i < 50; i++)
        {
            var new_freq = new Dictionary<long, long>();
            foreach (var kvp in freq)
            {
                Process(new_freq, kvp.Key, kvp.Value, ref res_2);
            }
            freq = new_freq;
        }

        return (res_1, res_2);
    }
}