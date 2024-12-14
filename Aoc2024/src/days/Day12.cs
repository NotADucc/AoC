using AoC;

public class Day12 : IRun
{
    readonly (int, int)[] DIRS = [(-1, 0), (0, 1), (1, 0), (0, -1)];
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc12.txt");
        long res_1 = 0, res_2 = 0;

        var grid = File.ReadAllLines(file_name)
            .Select(line => line.ToCharArray())
            .ToArray();

        var visited = new HashSet<(int, int)>();
        int n = grid.Length;
        int m = grid[0].Length;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (visited.Contains((i, j)))
                    continue;

                var queue = new Queue<(int, int)>();
                queue.Enqueue((i, j));
                int count = 0;
                int perim = 0;
                var PERIM = new Dictionary<(int, int), HashSet<(int, int)>>();

                while (queue.Count > 0)
                {
                    var (r, c) = queue.Dequeue();
                    if (visited.Contains((r, c)))
                        continue;

                    visited.Add((r, c));
                    count++;

                    foreach (var (dir_r, dir_c) in DIRS)
                    {
                        int new_r = r + dir_r;
                        int new_c = c + dir_c;

                        if (new_r >= 0 && new_r < n && new_c >= 0 && new_c < m && grid[new_r][new_c] == grid[r][c])
                        {
                            queue.Enqueue((new_r, new_c));
                        }
                        else
                        {
                            perim++;
                            if (!PERIM.ContainsKey((dir_r, dir_c)))
                                PERIM[(dir_r, dir_c)] = new HashSet<(int, int)>();

                            PERIM[(dir_r, dir_c)].Add((r, c));
                        }
                    }
                }

                int sides = 0;
                foreach (var kvp in PERIM)
                {
                    var visited_perim = new HashSet<(int, int)>();
                    var vs = kvp.Value;
                    int old_sides = sides;

                    foreach (var (r, c) in vs)
                    {
                        if (visited_perim.Contains((r, c)))
                            continue;

                        sides++;
                        var q = new Queue<(int, int)>();
                        q.Enqueue((r, c));

                        while (q.Count > 0)
                        {
                            var (r2, c2) = q.Dequeue();
                            if (visited_perim.Contains((r2, c2)))
                                continue;

                            visited_perim.Add((r2, c2));

                            foreach (var (dir_r, dir_c) in DIRS)
                            {
                                int new_r2 = r2 + dir_r;
                                int new_c2 = c2 + dir_c;
                                if (vs.Contains((new_r2, new_c2)))
                                    q.Enqueue((new_r2, new_c2));
                            }
                        }
                    }
                }

                res_1 += count * perim;
                res_2 += count * sides;
            }
        }

        return (res_1, res_2);
    }
}