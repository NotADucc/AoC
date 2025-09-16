using System.Drawing.Printing;
using System.Dynamic;
using System.IO.IsolatedStorage;
using System.Text.Json;

namespace AoC._2015;

public class Day14 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc14.txt");
        string[] instructions = File.ReadAllLines(file_name);
        long res_1 = 0, res_2 = 0;
        const int DURATION = 2503;

        var reindeers = instructions
            .Select(extract_instruction)
            .ToArray();

        res_1 = travel(reindeers, DURATION, false).Max();
        res_2 = travel(reindeers, DURATION, true).Max();

        return (res_1, res_2);
    }


    private static long[] travel(Reindeer[] reindeers, int duration, bool sol_2)
    {
        int n = reindeers.Length;
        long[] distance_trav = new long[n];
        long[] offset = new long[n];


        for (int i = 0; i < duration; i++)
        {
            for (int j = 0; j < n; j++)
            {
                distance_trav[j] += reindeers[j].get_tick_distance();
            }

            if (sol_2)
            {
                long max_val = distance_trav[0];
                List<int> idxs = [0];

                for (int j = 1; j < n; j++)
                {
                    if (distance_trav[j] == max_val)
                    {
                        idxs.Add(j);
                    }
                    else if (distance_trav[j] > max_val)
                    {
                        idxs.Clear();
                        idxs.Add(j);
                        max_val = distance_trav[j];
                    }
                }

                foreach (int idx in idxs)
                {
                    offset[idx]++;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            reindeers[i].reset();
        }

        return !sol_2 ? distance_trav : offset;
    }

    private static Reindeer extract_instruction(string instruction)
    {
        var splitted_space = instruction
            .Split(' ');

        int speed = int.Parse(splitted_space[3]);
        int speed_duration = int.Parse(splitted_space[6]);
        int rest = int.Parse(splitted_space[^2]);

        return new Reindeer()
        {
            name = splitted_space[0],
            speed = speed,
            duration = speed_duration,
            rest = rest
        };
    }

    private class Reindeer
    {
        public string name { get; set; }
        public int speed { get; set; }
        public int duration { get; set; }
        public int rest { get; set; }


        public bool is_flying => !is_resting;
        public bool is_resting { get; set; } = false;


        private int tick = 0;
        public int get_tick_distance()
        {
            tick++;
            if (is_resting)
            {
                if (tick <= rest)
                    return 0;

                toggle_rest();
                return speed;
            }
            else
            {
                if (tick <= duration)
                    return speed;

                toggle_rest();
                return 0;
            }
        }
        private void toggle_rest()
        {
            tick = 1;
            is_resting = !is_resting;
        }

        public void reset()
        {
            tick = 0;
            is_resting = false;
        }
    }
}