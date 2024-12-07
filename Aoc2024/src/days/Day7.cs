using AoC;

public class Day7 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc7.txt");
        long res_1 = 0, res_2 = 0;

        bool Calc(long res, long curr, List<long> nums, int idx, bool pipe)
        {
            if (idx >= nums.Count)
            {
                return res == curr;
            }
            else 
            {
                return Calc(res, curr + nums[idx], nums, idx + 1, pipe) 
                    || Calc(res, curr * nums[idx], nums, idx + 1, pipe)
                    || (pipe && Calc(res, Combine(curr, nums[idx]), nums, idx + 1, pipe));
            }
        }

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