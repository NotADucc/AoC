using System.Collections.Generic;
using System.Drawing.Text;
using System.Xml.Linq;

namespace AoC._2015;

public class Day25 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc25.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadAllText(file_name);
        input = new string(input.Skip(58).ToArray());

        int row_idx = input.IndexOf("row ") + 4;
        int col_idx = input.IndexOf("column ") + 7;

        int row = int.Parse(
            input[row_idx..input.IndexOf(',')]
        );

        int column = int.Parse(
            input[col_idx..input.IndexOf('.')]
        );

        res_1 = find_code(row, column);
        //res_1 = find_code(1, 6);

        return (res_1, res_2);
    }

    private long find_code(int target_row, int target_column)
    {
        const long MUL = 252_533, MOD = 33_554_393;
        int row = 1, column = 1;
        long res = 20_151_125;

        while (row != target_row || column != target_column)
        {
            if (row == 1)
            {
                row = column + 1;
                column = 1;
            }
            else
            {
                row--;
                column++;
            }
            res = res * MUL % MOD;
        }

        return res;
    }

    private long find_code_rec(int target_row, int target_column)
    {
        const long MUL = 252_533, MOD = 33_554_393;

        long rec(int r, int c)
        {
            if (c == 1)
            {
                c = r - 1;
                r = 1;
            }
            else
            {
                r++;
                c--;
            }

            if (r == 1 && c == 1)
                return 20_151_125;

            return rec(r, c) * MUL % MOD;
        }

        target_row--;
        target_column++;

        return rec(target_row, target_column);
    }
}