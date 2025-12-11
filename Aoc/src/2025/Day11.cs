using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AoC._2025;

public class Day11 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc11.txt");
        long res_1 = 0, res_2 = 0;

        // dp memo?
        // dont think prefix sum ish solution will help here
        var lines = File.ReadAllLines(file_name);

        Dictionary<string, HashSet<string>> routes = [];

        foreach (var line in lines)
        {
            var split = line.Split(":");
            routes.Add(split[0], 
                split[1]
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToHashSet()
            );
        }

        res_1 = rec1("you", routes);
        res_2 = rec2("svr", routes);

        return (res_1, res_2);
    }

    private long rec1(string start, Dictionary<string, HashSet<string>> routes)
    {
        const string END = "out";
        long res = 0;

        void inner(string node) 
        {
            if (node.Equals(END))
            {
                res++;
                return;
            }
            if (routes.TryGetValue(node, out var start_routes))
            {
                foreach (var route in start_routes)
                {
                    inner(route);
                }
            }
        }

        inner(start);

        return res;
    }

    private long rec2(string start, Dictionary<string, HashSet<string>> routes)
    {
        const string END = "out";
        Dictionary<string, long> memo = [];

        long inner(string prev, string curr, HashSet<string> visited)
        {
            string key = $"{prev}:{curr}:{visited.Contains("dac")}:{visited.Contains("fft")}";
            if (curr.Equals(END))
            {
                int t = 0;
                if (visited.Contains("dac") && visited.Contains("fft"))
                    t++;

                memo[key] = t;
                return t;
            }

            if (memo.TryGetValue(key, out long value))
            { 
                return value;
            }

            long res = 0;
            if (routes.TryGetValue(curr, out var start_routes))
            {
                foreach (var route in start_routes)
                {
                    visited.Add(route);
                    res += inner(curr, route, visited);
                    visited.Remove(route);
                }
            }

            memo[key] = res;

            return res;
        }

        var inn = inner(string.Empty, start, [start]);

        return inn;
    }
}