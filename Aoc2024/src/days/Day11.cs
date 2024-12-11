using AoC;

public class Day11 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc11.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadLines(file_name)
            .Select(line => line.Split().Select(int.Parse))
            .SelectMany(x => x)
            .ToArray();

        var lst_1 = new List<int>(input);



        return (lst_1.Count, res_2);
    }
}