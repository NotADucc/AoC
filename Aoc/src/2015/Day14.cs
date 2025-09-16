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

        var (distance_traveled, point_per_tick) = travel(reindeers, DURATION);
        res_1 = distance_traveled.Max();
        res_2 = point_per_tick.Max();

        return (res_1, res_2);
    }


    private static (long[] distance_traveled, long[] point_per_tick) travel(Reindeer[] reindeers, int duration)
    {
        int n = reindeers.Length;
        long[] distance_trav = new long[n];
        long[] offset = new long[n];


        for (int i = 0; i < duration; i++)
        {
            for (int j = 0; j < n; j++)
                distance_trav[j] += reindeers[j].get_tick_distance();

            List<int> idxs = distance_trav
                .Select((val, idx) => new { idx, val })
                .GroupBy(x => x.val)
                .OrderByDescending(x => x.Key)
                .FirstOrDefault()?
                .Select(x => x.idx)
                .ToList() ?? [];

            foreach (int idx in idxs) 
                offset[idx]++;
        }

        for (int i = 0; i < n; i++) 
            reindeers[i].reset_distance();

        return (distance_trav, offset);
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


        private bool is_flying => !is_resting;
        private bool is_resting = false;
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
        public void reset_distance()
        {
            tick = 0;
            is_resting = false;
        }
        private void toggle_rest()
        {
            tick = 1;
            is_resting = !is_resting;
        }
    }
}