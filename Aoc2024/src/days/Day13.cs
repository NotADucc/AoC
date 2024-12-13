using AoC;

public class Day13 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc13.txt");
        long res_1 = 0, res_2 = 0;
        var machine = new ClawMachine();
        foreach (var line in File.ReadAllLines(file_name))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var splitted = line.Split(',', StringSplitOptions.TrimEntries);
            var coords = splitted
                .Select(x => x.Split(['+', '='])[1])
                .Select(int.Parse)
                .ToArray();

            machine.Add(coords[0], coords[1]);
            if (machine.IsFilled())
            { 
                res_1 += machine.Solve();
                res_2 += machine.Solve(true);

                machine = new ClawMachine();
            }
        }

        return (res_1, res_2);
    }

    class ClawMachine
    {
        public static int COST_A = 3;
        public static int COST_B = 1;
        public static long OOPSIE = 10000000000000;
        public Location ButtonA;
        public Location ButtonB;
        public Location Prize;
        public void Add(int x, int y)
        {
            if (ButtonA is null)
            {
                ButtonA = new Location(x, y);
            }
            else if (ButtonB is null)
            {
                ButtonB = new Location(x, y);
            }
            else if (Prize is null)
            {
                Prize = new Location(x, y);
            }
        }
        public bool IsFilled() 
            => ButtonA is not null && ButtonB is not null && Prize is not null;

        public long Solve(bool flag = false)
        {
            decimal x_1 = ButtonA.X;
            decimal x_2 = ButtonB.X;

            decimal y_1 = ButtonA.Y;
            decimal y_2 = -ButtonB.Y;

            decimal x_tot = Prize.X + (flag ? OOPSIE : 0);
            decimal y_tot = Prize.Y + (flag ? OOPSIE : 0);

            decimal div = x_1 / y_1;
            x_2 /= div;
            x_tot /= div;
            x_tot -= y_tot;

            y_2 += x_2;
            x_tot /= y_2;
            

            long res_x = (long)Math.Round(x_tot);
            long res_y = ((Prize.X + (flag ? OOPSIE : 0)) - (ButtonB.X * res_x)) / ButtonA.X;

            long p_x = res_y * ButtonA.X + res_x * ButtonB.X;
            long p_y = res_y * ButtonA.Y + res_x * ButtonB.Y;
            return p_x == Prize.X + (flag ? OOPSIE : 0) && p_y == Prize.Y + (flag ? OOPSIE : 0) ? (res_x * COST_B) + (res_y * COST_A) : 0L;
        }
    }
    class Location
    {
        public long X;
        public long Y;

        public Location(long x, long y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}