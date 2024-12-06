using AoC;

public class Day1 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc1.txt");

        (int[] arr1, int[] arr2) = File.ReadAllLines(file_name)
            .Select(line => line.Split("   ").Select(int.Parse).ToArray())
            .Aggregate((new int[0], new int[0]),      
                (accu, pair) => (
                    accu.Item1.Concat([pair[0]]).ToArray(),
                    accu.Item2.Concat([pair[1]]).ToArray())
            );

        var res_1 = arr1
            .Order()
            .Zip(arr2.Order())
            .Select(x => Math.Abs(x.First - x.Second))
            .Sum();

        var freq = arr2
            .GroupBy(x => x)
            .ToDictionary(k => k.Key, v => v.Count());

        var res_2 = arr1
            .Select(x => { freq.TryGetValue(x, out var e); return x * e; })
            .Sum();


        Console.WriteLine($"Res 1: {res_1}"); // 2113135
        Console.WriteLine($"Res 2: {res_2}"); // 19097157

        return (res_1, res_2);
    }
}
