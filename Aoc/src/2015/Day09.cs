namespace AoC._2015;

public class Day09 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc9.txt");
        string[] destinations = File.ReadAllLines(file_name);
        long res_1 = 0, res_2 = 0;

        PriorityQueue<(int w, string s, string e), int> q = new();
        Dictionary<string, List<(int w, string s, string e)>> nodes = new();
        HashSet<string> visited = new();
        foreach (string destination in destinations)
        {
            var split = destination.Split(" = ");
            var locs = split[0].Split(" to ");

            int weight = int.Parse(split[1]);

            if (!nodes.TryAdd(locs[0], [(weight, locs[0], locs[1])]))
                nodes[locs[0]].Add((weight, locs[0], locs[1]));

            if (!nodes.TryAdd(locs[1], [(weight, locs[1], locs[0])]))
                nodes[locs[1]].Add((weight, locs[1], locs[0]));
        }


        foreach (var kvp in nodes)
        {
            visited.Add(kvp.Key);
            calc_paths(
                nodes,
                visited,
                kvp.Key,
                0,
                ref res_1,
                ref res_2
            );
            visited.Remove(kvp.Key);
        }

        return (res_1, res_2);
    }

    private void calc_paths(
        Dictionary<string, List<(int w, string s, string e)>> nodes,
        HashSet<string> visited,
        string current_node,
        int current_cost,
        ref long shortest_res,
        ref long longest_res
    )
    {
        if (visited.Count == nodes.Count)
        {
            shortest_res = shortest_res == 0 ? current_cost : Math.Min(shortest_res, current_cost);
            longest_res = Math.Max(longest_res, current_cost);
            return;
        }

        foreach (var (w, s, e) in nodes[current_node])
        {
            if (visited.Contains(e))
                continue;

            visited.Add(e);
            calc_paths(
                nodes,
                visited,
                e,
                current_cost + w,
                ref shortest_res,
                ref longest_res
            );
            visited.Remove(e);
        }
    }
}