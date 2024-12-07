using AoC;

public class Day7 : IRun
{
    public (long, long) Run()
    {

        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc7.txt");

        int res_1 = 0, res_2 = 0;

        Console.WriteLine($"Res 1 : {res_1}"); // 
        Console.WriteLine($"Res 2 : {res_2}"); // 

        return (res_1, res_2);
    }
}