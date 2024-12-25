namespace AoC._2024;

public class Day04 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc4.txt");
        int res_1 = 0, res_2 = 0;

        bool MatchXMAS(char c1, char c2, char c3, char c4)
            => c1 == 'X' && c2 == 'M' && c3 == 'A' && c4 == 'S' || c1 == 'S' && c2 == 'A' && c3 == 'M' && c4 == 'X';

        bool MatchMAS(char c1, char c2, char c3)
            => c1 == 'M' && c2 == 'A' && c3 == 'S' || c1 == 'S' && c2 == 'A' && c3 == 'M';

        string[] puzzle = File.ReadAllLines(file_name).ToArray();

        for (int i = 0; i < puzzle.Length; i++)
        {
            for (int j = 0; j < puzzle[i].Length; j++)
            {
                if (j + 3 < puzzle[i].Length && MatchXMAS(puzzle[i][j], puzzle[i][j + 1], puzzle[i][j + 2], puzzle[i][j + 3]))
                    res_1++;
                if (i + 3 < puzzle.Length && MatchXMAS(puzzle[i][j], puzzle[i + 1][j], puzzle[i + 2][j], puzzle[i + 3][j]))
                    res_1++;
                if (i + 3 < puzzle.Length && j + 3 < puzzle[i].Length && MatchXMAS(puzzle[i][j], puzzle[i + 1][j + 1], puzzle[i + 2][j + 2], puzzle[i + 3][j + 3]))
                    res_1++;
                if (i - 3 >= 0 && j + 3 < puzzle[i].Length && MatchXMAS(puzzle[i][j], puzzle[i - 1][j + 1], puzzle[i - 2][j + 2], puzzle[i - 3][j + 3]))
                    res_1++;

                if (i + 2 < puzzle.Length && j + 2 < puzzle[i].Length && MatchMAS(puzzle[i][j], puzzle[i + 1][j + 1], puzzle[i + 2][j + 2]) && MatchMAS(puzzle[i][j + 2], puzzle[i + 1][j + 1], puzzle[i + 2][j]))
                    res_2++;
            }
        }

        return (res_1, res_2);
    }
}