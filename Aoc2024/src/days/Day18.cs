using AoC;

public class Day18 : IRun<long, (long, long)>
{
    private readonly int[][] dirs = [[0, 1],[1, 0],[0, -1],[-1, 0]];
    private readonly int GRID_SIZE = 70, CORRUPT_COUNT = 1024;
    public (long, (long, long)) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc18.txt");
        long res_1 = 0, res_2 = 0;

        var base_corrupted = File.ReadAllLines(file_name)
            .Select(x => x.Split(',').Select(int.Parse).ToArray())
            .Select(x => (x[0], x[1]))
            .ToArray();

        int Dijkstra(HashSet<(int, int)> corrupted)
        {
            var q = new PriorityQueue<(int r, int c, int l), int>();
            var visited = new HashSet<(int r, int c)>();
            q.Enqueue((0, 0, 0), 0);

            while (q.Count > 0)
            {
                var (r, c, l) = q.Dequeue();
                if (r == GRID_SIZE && c == GRID_SIZE)
                {
                    return l;
                }

                for (int i = 0; i < dirs.Length; i++)
                {
                    int rr = r + dirs[i][0], cc = c + dirs[i][1];

                    if (rr < 0 || cc < 0 || rr > GRID_SIZE || cc > GRID_SIZE || corrupted.Contains((rr, cc)) || !visited.Add((rr, cc)))
                    {
                        continue;
                    }

                    q.Enqueue((rr, cc, l + 1), l + 1);
                }
            }

            return -1;
        }

        res_1 = Dijkstra(base_corrupted.Take(CORRUPT_COUNT).ToHashSet());
        int l = CORRUPT_COUNT, r = base_corrupted.Length - 1;

        while (l <= r)
        {
            int mid = (l + r) >> 1;
            int ans = Dijkstra(base_corrupted.Take(mid).ToHashSet());
            if (ans > 0)
            {
                res_2 = mid;
                l = mid + 1;       
            }
            else
            {
                r = mid - 1;
            }
        }

        return (res_1, base_corrupted[res_2]);
    }
}