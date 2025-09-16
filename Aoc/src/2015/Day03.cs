namespace AoC._2015;

public class Day03 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc3.txt");
        int[] sol_1_coord = [0, 0];
        int[] sol_2_coord_real = [0, 0];
        int[] sol_2_coord_robot  = [0, 0];
        bool sol_2_real_turn = true;

        string data = File.ReadAllText(file_name);
        Dictionary<string, int> sol_1_freq = new()
        {
            { create_key(sol_1_coord[0], sol_1_coord[1]), 1 }
        };

        Dictionary<string, int> sol_2_freq = new()
        {
            { create_key(sol_2_coord_real[0], sol_2_coord_real[1]), 2 }
        };

        foreach (char ch in data)
        {
            var (x, y) = give_offset(ch);
            sol_1_coord[0] += x;
            sol_1_coord[1] += y;

            string coord_string = create_key(sol_1_coord[0], sol_1_coord[1]);
            if (!sol_1_freq.TryAdd(coord_string, 1))
                sol_1_freq[coord_string]++;


            int[] sol_2_coord = sol_2_real_turn ? sol_2_coord_real : sol_2_coord_robot;
            sol_2_real_turn = !sol_2_real_turn;

            sol_2_coord[0] += x;
            sol_2_coord[1] += y;

            coord_string = create_key(sol_2_coord[0], sol_2_coord[1]);
            if (!sol_2_freq.TryAdd(coord_string, 1))
                sol_2_freq[coord_string]++;
        }

        return (sol_1_freq.Count, sol_2_freq.Count);
    }

    private static (int x, int y) give_offset(char direction)
        => direction switch
    {
        '^' => (1, 0),
        'v' => (-1, 0),
        '>' => (0, 1),
        '<' => (0, -1),
        _ => throw new ArgumentException($"unknown direction: {direction}"),
    };
    private static string create_key(int x, int y) => $"{x}:{y}";
}