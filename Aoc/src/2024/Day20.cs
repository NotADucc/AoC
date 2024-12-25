using System.Net.Mime;

namespace AoC._2024;

public class Day20 : IRun<long, long>
{
    private readonly int[][] dirs = [[1, 0], [-1, 0], [0, 1], [0, -1]];
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc20.txt");
        long res_1 = 0, res_2 = 0;
        int[] start = [], end = [];
        List<char[]> grid = new List<char[]>();
        foreach (var line in File.ReadAllLines(file_name))
        {
            var s_idx = line.IndexOf('S');
            if (s_idx > -1)
            {
                start = [grid.Count, s_idx];
            }
            var e_idx = line.IndexOf('E');
            if (e_idx > -1)
            {
                end = [grid.Count, e_idx];
            }
            grid.Add(line.ToCharArray());
        }

        void BuildGrid(List<char[]> grid, int[][] dist, int[] start, int[] end)
        {
            (int r, int c) = (start[0], start[1]);
            dist[r][c] = 0;

            while (r != end[0] || c != end[1])
            { 
                for (int i = 0; i < dirs.Length; i++)
                {
                    (int nr, int nc) = (r + dirs[i][0], c + dirs[i][1]);
                    if (nr < 0 || nc < 0 || nr >= grid.Count || nc >= grid[nr].Length || grid[nr][nc] == '#' || dist[nr][nc] != -1)
                    {
                        continue;
                    }
                    dist[nr][nc] = dist[r][c] + 1;
                    r = nr;
                    c = nc;
                }
            }
        }

        long Phase(int[][] distances, int phase_duration)
        {
            const int SKIP = 100;
            long res = 0;
            for (int r = 0; r < distances.Length; r++)
            {
                for (int c = 0; c < distances[r].Length; c++)
                {
                    if (grid[r][c] == '#') continue;
                    for (int radius = 2; radius <= phase_duration; radius++)
                    {
                        for (int rr = 0; rr <= radius; rr++)
                        {
                            int cc = radius - rr;
                            foreach ((int nr, int nc) in new HashSet<(int, int)> { (r + rr, c + cc), (r + rr, c - cc), (r - rr, c + cc), (r - rr, c - cc) })
                            {
                                if (nr < 0 || nc < 0 || nr >= grid.Count || nc >= grid[nr].Length || grid[nr][nc] == '#')
                                {
                                    continue;
                                }

                                if (distances[r][c] - distances[nr][nc] >= SKIP + radius)
                                {
                                    res++;
                                }
                            }
                        }
                    }

                }
            }
            return res;
        }

        var distances = new int[grid.Count][];

        for (int i = 0; i < grid.Count; i++)
        {
            distances[i] = new int[grid[i].Length];
            for (int j = 0; j < grid[i].Length; j++)
            {
                distances[i][j] = -1;
            }
        }

        BuildGrid(grid, distances, start, end);

        res_1 = Phase(distances, 2);
        res_2 = Phase(distances, 20);

        return (res_1, res_2);
    }
}