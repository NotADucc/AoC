using AoC;

public class Day07 : IRun<long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc7.txt");
        long res_1 = 0, res_2 = 0;

        bool Calc(long res, long curr, List<long> nums, int idx, bool pipe)
            => idx >= nums.Count || curr > res
                ? res == curr 
                : Calc(res, curr + nums[idx], nums, idx + 1, pipe) || Calc(res, curr * nums[idx], nums, idx + 1, pipe)
                || (pipe && Calc(res, Combine(curr, nums[idx]), nums, idx + 1, pipe));

        long Combine(long num1, long num2)
        {
            long i = 1;
            while (i * 10 <= num2) i *= 10;

            for (; i > 0; i /= 10)
            {
                num1 *= 10;
                num1 += num2 / i;
                num2 %= i;
            }
            return num1;
        }

        foreach (var line in File.ReadAllLines(file_name))
        {
            var split = line.Split([':', ' '], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();
            long res = split[0];
            long curr = split[1];

            if (Calc(res, curr, split, 2, false))
            {
                res_1 += res;
            }
            else if (Calc(res, curr, split, 2, true))
            {
                res_2 += res;
            }
        }

        return (res_1, res_1 + res_2);
    }
}