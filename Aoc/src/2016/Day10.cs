
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC._2016;

public class Day10 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc10.txt");
        long res_1 = 0, res_2 = 0;

        var lines = File.ReadAllLines(file_name);
        Dictionary<int, List<int>> bot_cache = [];
        Dictionary<int, int[]> bot_instructions = [];
        Dictionary<int, int> config = [];
        Queue<(int val, int bot)> q = [];

        build_stuff(
            bot_cache,
            bot_instructions,
            config,
            q,
            lines
        );

        void give(
            Dictionary<int, List<int>> bot_cache,
            Dictionary<int, int[]> bot_instructions,
            Dictionary<int, int> config,
            int bot,
            int value
        )
        {
            bot_cache[bot].Add(value);

            if (bot_cache[bot].Count <= 1)
                return;

            int[] vals = bot_cache[bot][0] <= bot_cache[bot][1]
                ? [bot_cache[bot][0], bot_cache[bot][1]]
                : [bot_cache[bot][1], bot_cache[bot][0]];

            if (vals[0] == 17 && vals[1] == 61)
                res_1 = bot;

            var instructions = bot_instructions[bot];

            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i] < 0)
                {
                    config.Add(~instructions[i], vals[i]);
                }
                else
                {
                    give(
                        bot_cache,
                        bot_instructions,
                        config,
                        instructions[i],
                        vals[i]
                    );
                }
            }
        }

        while (q.Count > 0)
        {
            var (val, bot) = q.Dequeue();
            give(bot_cache, bot_instructions, config, bot, val);
        }

        res_2 = config[0] * config[1] * config[2];

        return (res_1, res_2);
    }

    private static void build_stuff(
        Dictionary<int, List<int>> bot_cache,
        Dictionary<int, int[]> bot_instructions,
        Dictionary<int, int> config,
        Queue<(int val, int bot)> q,
        string[] lines
    )
    {
        foreach (var line in lines)
        {
            var splitted = line.Split(' ');
            if (line[0] == 'v')
            {
                q.Enqueue((int.Parse(splitted[1]), int.Parse(splitted[^1])));
            }
            else
            {
                int low = int.Parse(splitted[6]);
                int high = int.Parse(splitted[^1]);
                if (splitted[5] == "output") low = ~low;
                if (splitted[^2] == "output") high = ~high;

                bot_cache.Add(int.Parse(splitted[1]), []);
                bot_instructions.Add(int.Parse(splitted[1]), [low, high]);
            }
        }
    }
}