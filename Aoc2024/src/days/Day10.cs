using AoC;

public class Day10 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc10.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadLines(file_name)
            .Select(line => line.ToCharArray().Select(x => x.CharToDigit()).ToArray())
            .ToArray();


        int DFS(int[][] input, int i, int j, int prev_val, HashSet<(int, int)> visited, bool flag) 
        { 
            if (i < 0 || j < 0 || i >= input.Length || j >= input[i].Length || prev_val + 1 != input[i][j] || (flag && visited.Contains((i, j)))) 
                return 0;

            if (input[i][j] == 9)
            {
                visited.Add((i, j));
                return 1;
            }

            int val = DFS(input, i - 1, j, input[i][j], visited, flag)
                    + DFS(input, i + 1, j, input[i][j], visited, flag)
                    + DFS(input, i, j - 1, input[i][j], visited, flag)
                    + DFS(input, i, j + 1, input[i][j], visited, flag);

            return val;
        }
        
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] != 0) 
                    continue;

                res_1 += DFS(input, i, j, -1, [], true);
                res_2 += DFS(input, i, j, -1, [], false);
            }
        }

        return (res_1, res_2);
    }
}