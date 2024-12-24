namespace AoC.days;

public class Day23 : IRun<long, string>
{
    public (long, string) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc23.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadAllLines(file_name);

        var dct = input
            .Select(x => x.Split('-'))
            .Select(x => x.OrderBy(x => x).ToArray())
            .Concat(input.Select(x => x.Split('-')).Select(x => x.OrderByDescending(x => x).ToArray()))
            .GroupBy(x => x[0])
            .ToDictionary(x => x.Key, x => x.Select(y => y[1]).ToHashSet());
        
        var res = new HashSet<string>();

        foreach (var kvp in dct)
        {
            foreach (var val in kvp.Value)
            {
                var intersect = kvp.Value.Intersect(dct[val]).ToArray();

                if (intersect.Length == 0) 
                    continue;

                var t = intersect.Select(x => string.Join("", new string[] { kvp.Key, val, x }.OrderBy(x => x)))
                    .ToArray();

                foreach (var el in t)
                {
                    res.Add(el);
                }
            }
        }

        foreach (var item in res)
        {
            if (item[0] == 't' || item[2] == 't' || item[4] == 't')
            {
                res_1++;
            }
        }

        return (res_1, "");
    }
}