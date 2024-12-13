using AoC;

public class Day13 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc13.txt");
        long res_1 = 0, res_2 = 0;
        var lst = new List<ClawMachine>();
        var loc = new ClawMachine();
        foreach (var line in File.ReadAllLines(file_name))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                lst.Add(loc);
                loc = new ClawMachine();
                continue;
            }

        }

        return (res_1, res_2);
    }

    class ClawMachine
    {
        public Location ButtonA;
        public Location ButtonB;
        public Location LocationA;
        public Location LocationB;
    }
    class Location
    {
        public int a;
        public int b;
    }
}