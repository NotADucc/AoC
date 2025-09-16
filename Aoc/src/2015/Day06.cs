namespace AoC._2015;

public class Day06 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc6.txt");
        const int ARR_SIZE = 1000;
        int[,] res_1 = new int[ARR_SIZE, ARR_SIZE], res_2 = new int[ARR_SIZE, ARR_SIZE];
        foreach (string line in File.ReadAllLines(file_name))
        {
            Light light = Light.translate(line);

            for (int i = light.start_x; i <= light.end_x; i++)
            {
                for (int j = light.start_y; j <= light.end_y; j++)
                {
                    res_1[i, j] = light.f(res_1[i, j]);
                    res_2[i, j] = Math.Max(0, res_2[i, j] + light.offset);
                }
            }
        }

        return (
            res_1.Cast<int>().AsParallel().Sum(), 
            res_2.Cast<int>().AsParallel().Sum()
        );
    }

    private class Light
    {
        public int start_x, end_x;
        public int start_y, end_y;
        public int offset;
        public Func<int, int> f;
        public static Light translate(string line)
        {
            (int offset_line, Func<int, int> pred, int offset_grid) = line switch
            {
                var l when l.StartsWith("turn on")  
                    => ("turn on ".Length, new Func<int, int>(b => 1), 1),
                var l when l.StartsWith("turn off") 
                    => ("turn off ".Length, new Func<int, int>(b => 0), -1),
                _ => ("toggle ".Length, new Func<int, int>(b => b == 1 ? 0 : 1), 2)
            };

            var coords = line.Substring(offset_line).Split(" through ");
            var coord_1 = coords[0].Split(',');
            var coord_2 = coords[1].Split(',');
            return new Light
            {
                start_x = int.Parse(coord_1[0]),
                end_x = int.Parse(coord_2[0]),
                start_y = int.Parse(coord_1[1]),
                end_y = int.Parse(coord_2[1]),
                f = pred,
                offset = offset_grid
            };
        }
    }
}