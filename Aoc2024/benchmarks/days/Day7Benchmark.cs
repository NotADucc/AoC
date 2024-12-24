using AoC;
using AoC.benchmark;
using AoC.days;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.days
{
    public class Day7Benchmark : BenchmarkAttributes
    {
        [Benchmark(Baseline = true)]
        public int Refactored()
        {
            new Day07().Run();
            return -1;
        }

        [Benchmark]
        public int Old()
        {
            new Day7Old().Run();
            return -1;
        }
    }
}

public class Day7Old : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc7.txt");

        long res_1 = 0, res_2 = 0;

        bool Calc(long res, long curr, List<long> nums, int idx, bool pipe)
            => idx >= nums.Count
                ? res == curr
                : Calc(res, curr + nums[idx], nums, idx + 1, pipe) || Calc(res, curr * nums[idx], nums, idx + 1, pipe)
                || (pipe && Calc(res, Combine(curr, nums[idx]), nums, idx + 1, pipe));

        long Combine(long num1, long num2)
        {
            return long.Parse($"{num1}{num2}");
        }

        foreach (var line in File.ReadAllLines(file_name))
        {
            var split = line.Split([':', ' '], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();
            long res = split[0];
            split.RemoveAt(0);

            long curr = split[0];
            split.RemoveAt(0);

            if (Calc(res, curr, split, 0, false))
            {
                res_1 += res;
                res_2 += res;
            }
            else if (Calc(res, curr, split, 0, true))
            {
                res_2 += res;
            }
        }

        return (res_1, res_2);
    }
}