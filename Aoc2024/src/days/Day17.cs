using AoC;
using System;

public class Day17 : IRun<string, long>
{
    public (string, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc17.txt");
        List<long> res_1 = [], initial_registars = [], instructions = [];
        long res_2 = 0;
        bool first_half = true;

        List<long> Solve(long num, bool flag)
        {
            List<long> registars = [num, 0, 0], res = [];
            long Combo(long num)
            {
                return num switch
                {
                    0 or 1 or 2 or 3 => num,
                    4 => registars[0],
                    5 => registars[1],
                    6 => registars[2],
                    _ => -1
                };
            }
            for (int i = 0; i < instructions.Count; i += 2)
            {
                long combo = Combo(instructions[i + 1]);
                if (instructions[i] == 0)
                {
                    registars[0] /= (long)Math.Pow(2, combo);
                }
                else if (instructions[i] == 1)
                {
                    registars[1] ^= instructions[i + 1];
                }
                else if (instructions[i] == 2)
                {
                    registars[1] = combo % 8;
                }
                else if (instructions[i] == 3)
                {
                    if (registars[0] == 0)
                        continue;

                    i = (int) instructions[i + 1] - 2;
                }
                else if (instructions[i] == 4)
                {
                    registars[1] ^= registars[2];
                }
                else if (instructions[i] == 5)
                {
                    res.Add(combo % 8);
                    if (flag) 
                        return res;
                }
                else if (instructions[i] == 6)
                {
                    registars[1] = registars[0] / (long)Math.Pow(2, combo);
                }
                else if (instructions[i] == 7)
                {
                    registars[2] = registars[0] / (long)Math.Pow(2, combo);
                }
            }
            return res;
        }

        long Solve2(int idx, long num)
        {
            if (idx < 0)
            {
                return num;
            }
            for (int d = 0; d < 8; d++)
            {
                var res = Solve(num << 3 | d, true);
                if (res[0] == instructions[idx])
                {
                    var r = Solve2(idx - 1, num << 3 | d);
                    if (r != -1)
                        return r;
                }
            }

            return -1;
        }

        foreach (var line in File.ReadAllLines(file_name))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                first_half = false;
                continue;
            }

            if (first_half)
            {
                initial_registars.Add(int.Parse(line.Split(':')[1]));
            }
            else 
            {
                instructions.AddRange(line.Split(':')[1].Split(',').Select(long.Parse));
            }
        }

        res_1 = Solve(initial_registars[0], false);
        res_2 = Solve2(instructions.Count - 1, 0);

        return (string.Join(',', res_1), res_2);
    }
}