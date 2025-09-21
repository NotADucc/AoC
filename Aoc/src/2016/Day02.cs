namespace AoC._2016;

public class Day02 : IRun<long, string>
{
    public (long, string) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc2.txt");
        long res_1 = 0; string res_2 = string.Empty;

        var instructions = File.ReadAllLines(file_name);

        int[] coords_1 = [1, 1];
        int[] coords_2 = [2, 0];

        foreach (var instruction in instructions)
        {
            foreach (var dir in instruction)
            {
                coords_1 = translate_input_1(coords_1, dir);
                coords_2 = translate_input_2(coords_2, dir);
            }
            res_1 *= 10;
            res_1 += keypad_1[coords_1[0]][coords_1[1]];
            res_2 += keypad_2[coords_2[0]][coords_2[1]];
        }

        return (res_1, res_2);
    }
    private static int[] translate_move(char ch) => ch switch
    {
        'U' => [-1, 0],
        'D' => [1, 0],
        'L' => [0, -1],
        'R' => [0, 1],
        _ => throw new ArgumentException($"unknown char: {ch}"),
    };
    private static readonly int[][] keypad_1 =
    [
        [1, 2, 3],
        [4, 5, 6],
        [7, 8, 9],
    ];
    private static int[] translate_input_1(int[] coord, char ch)
    {
        int[] offset = translate_move(ch);

        for (int i = 0; i < coord.Length; i++)
        {
            offset[i] += coord[i];
            if (offset[i] < 0 || offset[i] > 2)
                offset[i] = coord[i];
        }

        return offset;
    }
    private static readonly char[][] keypad_2 =
    [
        ['\0', '\0', '1', '\0', '\0'],
        ['\0',  '2', '3', '4', '\0'],
        [ '5',  '6', '7', '8', '9'],
        ['\0',  'A', 'B', 'C', '\0'],
        ['\0', '\0', 'D', '\0', '\0'],
    ];
    private static int[] translate_input_2(int[] coord, char ch)
    {
        int[] offset = translate_move(ch);

        offset[0] += coord[0];
        if (offset[0] < 0 
            || offset[0] >= keypad_2.Length 
            || keypad_2[offset[0]][coord[1]] == '\0')
            offset[0] = coord[0];

        offset[1] += coord[1];
        if (offset[1] < 0 
            || offset[1] >= keypad_2.Length
            || keypad_2[coord[0]][offset[1]] == '\0')
            offset[1] = coord[1];

        return offset;
    }
}