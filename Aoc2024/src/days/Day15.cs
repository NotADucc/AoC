using AoC;

public class Day15 : IRun<long>
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
            var dir = dirs[move];
            var pot_loc = grid[robot[0] + dir.Item1][robot[1] + dir.Item2];
            if (pot_loc == '.')
            {
                robot[0] += dir.Item1;
                robot[1] += dir.Item2;
            }
            else if (pot_loc == 'O')
            {
                int start_r = robot[0] + dir.Item1;
                int start_c = robot[1] + dir.Item2;

                while (grid[start_r][start_c] != '.' && grid[start_r][start_c] != '#')
                {
                    start_r += dir.Item1;
                    start_c += dir.Item2;

                    if (start_r < 0 || start_r >= grid.Count || start_c < 0 || start_c >= grid[start_r].Length)
                        break;
                }

                if (start_r < 0 || start_r >= grid.Count || start_c < 0 || start_c >= grid[start_r].Length)
                    continue;

                if (grid[start_r][start_c] == '.')
                {
                    grid[robot[0] + dir.Item1][robot[1] + dir.Item2] = '.';
                    grid[start_r][start_c] = 'O';
                    robot[0] += dir.Item1;
                    robot[1] += dir.Item2;
                }
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
}