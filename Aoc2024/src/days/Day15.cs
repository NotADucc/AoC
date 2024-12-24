namespace AoC.days;

public class Day15 : IRun<long, long>
{
    private Dictionary<char, (int, int)> dirs = new Dictionary<char, (int, int)>()
    {
        ['^'] = (-1, 0),
        ['>'] = (0, 1),
        ['<'] = (0, -1),
        ['v'] = (1, 0),
    };
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc15.txt");
        long res_1 = 0, res_2 = 0;
        bool first_half = true;
        var grid = new List<char[]>();
        int[] robot = [0, 0];

        var grid2 = new List<char[]>();
        int[] robot2 = [0, 0];

        var moves = new List<char>();
        foreach (var line in File.ReadAllLines(file_name))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                first_half = false;
                continue;
            }

            if (first_half)
            {
                int idx = line.IndexOf('@');
                if (idx > -1)
                {
                    robot = [grid.Count, idx];
                }
                grid.Add(line.Replace('@', '.').ToCharArray());

                string new_line = line
                    .Replace("#", "##")
                    .Replace("O", "[]")
                    .Replace(".", "..")
                    .Replace("@", "@.");

                idx = new_line.IndexOf('@');
                if (idx > -1)
                {
                    robot2 = [grid2.Count, idx];
                    new_line = new_line.Replace("@", ".");
                }
                grid2.Add(new_line.ToCharArray());
            }
            else
            {
                moves.AddRange(line);
            }
        }

        foreach (var move in moves)
        {
            var (can_move, loc) = CanMove(grid, robot, dirs[move]);
            if (can_move)
            {
                int[] pos = [robot[0] + dirs[move].Item1, robot[1] + dirs[move].Item2];
                if (loc[0] != pos[0] || loc[1] != pos[1])
                {
                    grid[loc[0]][loc[1]] = 'O';
                    grid[pos[0]][pos[1]] = '.';
                }
                robot = pos;
            }
        }

        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                Console.Write(grid[i][j]);
                if (grid[i][j] == 'O')
                    res_1 += 100 * i + j;
            }
            Console.WriteLine();
        }

        return (res_1, res_2);
    }

    private (bool b, int[] loc) CanMove(List<char[]> grid, int[] pos, (int, int) dir)
    {
        int[] new_pos = [pos[0] + dir.Item1, pos[1] + dir.Item2];
        var tile = grid[new_pos[0]][new_pos[1]];
        if (tile == '.')
        {
            return (true, new_pos);
        }

        if (tile == '#')
        {
            return (false, null!);
        }

        return CanMove(grid, new_pos, dir);
    }
}