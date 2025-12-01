namespace AoC._2025;

public class Day01 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc1.txt");
        int res_1 = 0, res_2 = 0, start = 50;
        const int DIAL_COUNT = 100;

        var instructions = get_instructions(File.ReadAllLines(file_name));

        foreach (var instruction in instructions)
        {
            var temp = start + instruction.Value;
            var ans = modulo(temp, DIAL_COUNT);
            if (ans == 0)
            {
                res_1++;
                res_2++;
            }


            int potential_clicks = instruction.AbsValue / DIAL_COUNT;
            res_2 += potential_clicks;
            int temp_2 = start + (instruction.Value % DIAL_COUNT);
            if (temp_2 < 0 || temp_2 > DIAL_COUNT)
            {
                if (start != 0)
                {
                    res_2++;
                }
            }

            start = ans;
        }

        return (res_1, res_2);
    }

    private List<Instruction> get_instructions(IEnumerable<string> lines)
    {
        List<Instruction> res = [];

        foreach (string line in lines)
        {
            bool is_right = line[0].Equals('R');
            int mul = is_right ? 1 : -1;
            int num = int.Parse(line.Substring(1));
            res.Add(new Instruction(num * mul, is_right, num));
        }

        return res;
    }

    private int modulo(int a, int b)
        => ((a % b) + b) % b;

    private record Instruction(int Value, bool IsPositive, int AbsValue);
}