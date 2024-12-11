using AoC;

public class Day11 : IRun
{
    public (long, long) Run()
    {
        void Process(Dictionary<long, int> freq, long val, ref long score)
        {
            if (val == 0)
            {
                freq.TryAdd(1, 0);
                freq[1]++;
                return;
            }
            var str = val.ToString();
            var len = str.Length >> 1;
            if ((str.Length & 1) == 0)
            {
                long n1 = long.Parse(str.Substring(0, len)), n2 = long.Parse(str.Substring(len));
                freq.TryAdd(n1, 0);
                freq.TryAdd(n2, 0);
                freq[n1]++;
                freq[n2]++;
                score++;
            }
            else
            {
                val *= 2024;
                freq.TryAdd(val, 0);
                freq[val]++;
            }
        }

        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc11.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadLines(file_name)
            .Select(line => line.Split().Select(long.Parse))
            .SelectMany(x => x)
            .ToArray();

        res_1 = input.Length;

        Dictionary<long, int> freq = input
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
        
        for (int i = 0; i < 25; i++)
        {
            Dictionary<long, int> new_freq = new();
            foreach (var kvp in freq)
            {
                for (int k = 0; k < kvp.Value; k++)
                {
                    Process(new_freq, kvp.Key, ref res_1);
                }
            }
            freq = new_freq;
        }

        res_2 = res_1;

        for (int i = 0; i < 50; i++)
        {
            Dictionary<long, int> new_freq = new();
            foreach (var kvp in freq)
            {
                for (int k = 0; k < kvp.Value; k++)
                {
                    Process(new_freq, kvp.Key, ref res_2);
                }
            }
            freq = new_freq;
        }

        return (res_1, res_2);
    }
}