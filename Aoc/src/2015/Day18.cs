using System;

namespace AoC._2015;

public class Day18 : IRun<long, long>
{
    private const int GRID_SIZE = 100, STEPS = 100;
    private const char ON = '#', OFF = '.';
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc18.txt");
        long res_1 = 0, res_2 = 0;

        char[,] grid_1 = new char[GRID_SIZE, GRID_SIZE];
        char[,] grid_2 = new char[GRID_SIZE, GRID_SIZE];

        int row = 0;
        foreach (string line in File.ReadAllLines(file_name))
        {
            for (int i = 0; i < line.Length; i++)
            {
                grid_1[row, i] = line[i];
                grid_2[row, i] = line[i];
            }
            row++;
        }
        int n = grid_2.GetLength(0), m = grid_2.GetLength(1);
        grid_2[0, 0] = grid_2[n - 1, 0] = grid_2[0, m - 1] = grid_2[n - 1, m - 1] = ON;


        for (int i = 0; i < STEPS; i++)
        {
            grid_1 = next_step(grid_1, false);
            grid_2 = next_step(grid_2, true);
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (grid_1[i, j].Equals(ON))
                    res_1++;

                if (grid_2[i, j].Equals(ON))
                    res_2++;
            }
        }

        return (res_1, res_2);
    }

    private char[,] next_step(char[,] current_grid, bool sticky_sides)
    {
        int n = current_grid.GetLength(0), m = current_grid.GetLength(1);
        char[,] new_grid = new char[n, m];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                new_grid[i, j] = get_light_symbol(current_grid, i, j, sticky_sides);
            }
        }

        return new_grid;
    }

    private char get_light_symbol(char[,] grid, int row, int column, bool sticky_sides)
    {
        int n = grid.GetLength(0), m = grid.GetLength(1);
        if (
            sticky_sides
            && ((row == 0 && column == 0)
            || (row == 0 && column == m - 1)
            || (row == n - 1 && column == 0)
            || (row == n - 1 && column == m - 1))
        )
            return ON;

        char ch = grid[row, column];
        bool is_on = ch.Equals(ON);
        int neighbours_amount_on = 0;

        int min_row = Math.Max(row - 1, 0);
        int max_row = Math.Min(row + 1, n - 1);

        int min_column = Math.Max(column - 1, 0);
        int max_column = Math.Min(column + 1, m - 1);

        for (int i = min_row; i <= max_row; i++)
        {
            for (int j = min_column; j <= max_column; j++)
            {
                if (i == row && j == column)
                    continue;

                if (grid[i, j].Equals(ON))
                    neighbours_amount_on++;
            }
        }

        char new_symbol;
        if (is_on)
        {
            new_symbol = neighbours_amount_on == 2 || neighbours_amount_on == 3 ? ON : OFF;
        }
        else
        {
            new_symbol = neighbours_amount_on == 3 ? ON : OFF;
        }

        return new_symbol;
    }
}