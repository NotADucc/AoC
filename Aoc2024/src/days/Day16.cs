using AoC;

public class Day16 : IRun<long>
{
    private int[][] dirs = [[0, 1],[1, 0],[0, -1],[-1, 0]];
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc16.txt");
        long res_1 = 0, res_2 = 0;

        int start_i = 0, start_j = 0, end_i = 0, end_j = 0;
        List<char[]> grid = new List<char[]>();
        foreach (var line in File.ReadAllLines(file_name))
        {
            int idx = line.IndexOf("S");
            if (idx > -1)
            {
                start_i = grid.Count;
                start_j = idx;
            }
            idx = line.IndexOf("E");
            if (idx > -1)
            {
                end_i = grid.Count;
                end_j = idx;
            }

            grid.Add(line.ToCharArray());
        }

        var q = new PriorityQueue<(int i, int j, int dir, int cost), int>();
        var visited = new HashSet<(int, int, int)>();
        var weight = new Dictionary<(int, int), int>();
        q.Enqueue((start_i, start_j, 0, 0), 0);
        while (true)
        {
            var deq = q.Dequeue();
            if (deq.i == end_i && deq.j == end_j)
            {
                res_1 = deq.cost;
                break;
            }

            if (!weight.TryAdd((deq.i, deq.j), deq.cost))
                weight[(deq.i, deq.j)] = deq.cost;

            for (int k = 0; k < dirs.Length; k++)
            {
                int new_dir = (deq.dir + k) % dirs.Length;
                int[] offset = dirs[new_dir];
                int turn_cost = k == 2 ? 2 : k % 2;
                int i = deq.i + offset[0], j = deq.j + offset[1];

                if (i < 0 || j < 0 || i >= grid.Count || j >= grid[i].Length || grid[i][j] == '#' || !visited.Add((i, j, new_dir)))
                    continue;

                int new_cost = deq.cost + 1 + turn_cost * 1000;

                q.Enqueue((i, j, new_dir, new_cost), new_cost);
            }
        }
        
        return (res_1, res_2);
    }
}