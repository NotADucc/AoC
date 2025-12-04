namespace AoC._2025;

public class Day04 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc4.txt");
        long res_1 = 0, res_2 = 0;
        var lines = File.ReadAllLines(file_name)
            .Select(x => x.ToCharArray())
            .ToArray();

        res_1 = get_rolls(lines);
        res_2 = get_rolls(lines, true);

        return (res_1, res_2);
    }

    private const int MAX_ROLLS = 4;
    private static int get_rolls(char[][] lines, bool is_res_2 = false)
    {
        int res = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            bool removed_roll = false;
            for (int j = 0; j < line.Length; j++)
            {
                var ch = line[j];
                if (ch == '.') continue;

                int min_h = Math.Max(i - 1, 0);
                int min_w = Math.Max(j - 1, 0);

                int max_h = Math.Min(i + 1, lines.Length - 1);
                int max_w = Math.Min(j + 1, line.Length - 1);

                int count = 0;
                for (int k = min_h; k <= max_h; k++)
                {
                    for (int l = min_w; l <= max_w; l++)
                    {
                        if (k == i && l == j) continue;

                        if (lines[k][l] == '@')
                            count++;
                    }
                }

                if (count < MAX_ROLLS)
                {
                    res++;
                    if (is_res_2)
                    { 
                        line[j] = '.';
                        removed_roll = true;
                    }
                }
            }
            if (removed_roll) i = -1;
        }
        return res;
    }
}