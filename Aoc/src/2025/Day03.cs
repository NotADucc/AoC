namespace AoC._2025;

public class Day03 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc3.txt");
        long res_1 = 0, res_2 = 0;

        // read in data
        // go off line and greedily search biggest bank
        // or can bruteforce, dataset seems small enough to bruteforce

        var lines = File.ReadAllLines(file_name);

        foreach (var line in lines)
        {
            res_1 += get_joltage(line, 2);
            res_2 += get_joltage(line, 12);
        }

        return (res_1, res_2);
    }
    private static long get_joltage(string line, int count)
    {
        // make use of max stack/queue?
        // cant prefill with count since u can have gaps for better result
        Stack<int> stack = new Stack<int>(count * 2);
        int n = line.Length;

        for (int i = 0; i < n; i++)
        {
            int current_digit = convert(line[i]);

            if (stack.Count == 0)
            {
                stack.Push(current_digit);
                continue;
            }

            int digits_left = n - i - 1;

            if (current_digit > stack.Peek())
            {
                while (stack.Count > 0 && current_digit > stack.Peek() && digits_left >= count - stack.Count)
                {
                    stack.Pop();
                }
            }

            stack.Push(current_digit);

            if (stack.Count > count)
                stack.Pop();
        }

        int pow = 0;
        long res = 0;

        while (stack.Count > 0)
        {
            long num = stack.Pop() * (long)Math.Pow(10, pow++);
            res += num;
        }

        return res;
    }
    private static int convert(char ch) => ch - '0';
}