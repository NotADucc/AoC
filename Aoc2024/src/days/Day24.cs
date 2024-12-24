namespace AoC.days;

public class Day24 : IRun<long, string>
{
    public (long, string) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc24.txt");
        long res_1 = 0; string res_2 = string.Empty;

        var input = File.ReadAllLines(file_name);

        var splitted_input = input
            .Select((line, idx) => (line, idx))
            .First(x => string.IsNullOrWhiteSpace(x.line));

        var dct = input
            .Take(splitted_input.idx)
            .Select(x => x.Split(": "))
            .ToDictionary(x => x[0], x => int.Parse(x[1]));

        var instructions = input
            .Skip(splitted_input.idx + 1);

        var q = new Queue<string>(instructions);

        while (q.Count > 0)
        {
            var instruction = q.Dequeue();

            var split = instruction
                .Split(" ")
                .Where(x => x != "->")
                .ToArray();


            if (!dct.TryGetValue(split[0], out var val1) || !dct.TryGetValue(split[2], out var val2))
            {
                q.Enqueue(instruction);
                continue;
            }

            var res = split[1] switch
            {
                "AND" => val1 & val2,
                "XOR" => val1 ^ val2,
                "OR" => val1 | val2,
                _ => throw new ArgumentException()
            };
            dct[split[3]] = res;
        }

        var arr = dct
            .Where(x => x.Key.StartsWith("z"))
            .OrderBy(x => x.Key)
            .Select(x => (long)x.Value)
            .ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            res_1 |= arr[i] << i;
        }

        return (res_1, res_2);
    }
}