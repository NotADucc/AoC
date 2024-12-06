using AoC;

public class Day2 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc2.txt");

        bool CheckSafety(List<int> lst)
        {
            Func<int, int, bool> pred = lst[0] < lst[^1]
                ? (prev, curr) => prev < curr && prev + 3 >= curr
                : (prev, curr) => prev > curr && prev - 3 <= curr;

            return lst
                .Zip(lst.Skip(1))
                .All(x => x.First != x.Second && pred(x.First, x.Second));
        }

        var res = File.ReadAllLines(file_name)
            .Select(line =>
            {
                var lst = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                int res_1 = CheckSafety(lst) ? 1 : 0;
                int res_2 = res_1 == 1 ? 1 : lst
                    .Select((_, index) => index)
                    .Where(index => CheckSafety(lst.Where((_, idx) => idx != index).ToList()))
                    .Any() ? 1 : 0;

                return new { res_1, res_2 };
            }).ToArray();
        
        int res_1 = res.Sum(x => x.res_1), res_2 = res.Sum(x => x.res_2);

        Console.WriteLine($"Res 1 : {res_1}"); // 564
        Console.WriteLine($"Res 2 : {res_2}"); // 604

        return (res_1, res_2);
    }
}
