using AoC;

public class Day7 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc7.txt");
        int res_1 = 0, res_2 = 0;

        foreach (var line in File.ReadAllLines(file_name))
        {
            var split = line.Split([':', ' '], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);
        }

        return (res_1, res_2);
    }
}