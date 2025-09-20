namespace AoC._2016;

public class Day01 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc1.txt");
        long res_1 = 0, res_2 = 0;

        var instructions = File
            .ReadAllText(file_name)
            .Split(',', StringSplitOptions.TrimEntries)
            .ToArray();

        int[] coords = [0, 0];
        int facing_direction = 0;
        HashSet<(int, int)> visited = new();
        bool visited_twice = false;

        foreach (var instruction in instructions)
        {
            int direction = instruction[0] == 'R' ? 1 : -1;
            int true_direction = facing_direction <= 1 ? direction : direction * -1;
            int offset = int.Parse(instruction[1..]);

            int idx = (facing_direction & 1) == 0 ? 1 : 0;

            for (int i = 0; i < offset; i++)
            {
                coords[idx] += 1 * true_direction;
                if (!visited.Add((coords[0], coords[1])) && !visited_twice)
                {
                    visited_twice = true;
                    res_2 = Math.Abs(coords[0]) + Math.Abs(coords[1]);
                }
            }

            facing_direction += direction;
            if (facing_direction >= 4) 
                facing_direction = 0;
            if (facing_direction <= -1)
                facing_direction = 3;
        }

        res_1 = Math.Abs(coords[0]) + Math.Abs(coords[1]);

        return (res_1, res_2);
    }
}