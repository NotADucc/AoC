using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AoC._2016;

public class Day08 : IRun<long, string>
{
    public (long, string) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc8.txt");
        const int MAX_WIDTH = 50, MAX_LENGTH = 6;
        long res_1 = 0, res_2 = 0;

        var screen = new char[MAX_LENGTH, MAX_WIDTH];
        var instructions = File.ReadAllLines(file_name);

        foreach (var instruction in instructions)
        {
            var split = instruction.Split(' ');
            if (split.Length == 2)
            {
                var size = split[1].Split('x');
                var x = int.Parse(size[0]);
                var y = int.Parse(size[1]);
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        screen[i, j] = '#';
                    }
                }
            }
            else if (split[1].Equals("row"))
            {
                int target_length = screen.GetLength(1);
                int target = int.Parse(split[2][2..]);
                int offset = int.Parse(split[^1]);

                var new_slice = new char[target_length];
                for (int i = 0; i < target_length; i++)
                {
                    int idx = (i + offset) % target_length;
                    new_slice[idx] = screen[target, i];
                    screen[target, i] = '\0';
                }

                for (int i = 0; i < new_slice.Length; i++)
                {
                    screen[target, i] = new_slice[i];
                }               
            }
            else 
            {
                int target_length = screen.GetLength(0);
                int target = int.Parse(split[2][2..]);
                int offset = int.Parse(split[^1]);

                var new_slice = new char[target_length];
                for (int i = 0; i < target_length; i++)
                {
                    int idx = (i + offset) % target_length;
                    new_slice[idx] = screen[i, target];
                    screen[i, target] = '\0';
                }

                for (int i = 0; i < new_slice.Length; i++)
                {
                    screen[i, target] = new_slice[i];
                }
            }
        }



        for (int i = 0; i < screen.GetLength(0); i++)
        {
            for (int j = 0; j < screen.GetLength(1); j++)
            {
                if (screen[i, j] == '#')
                    res_1 += 1;
                else
                    screen[i, j] = '.';
            }
        }

        screen.Print(" ");

        return (res_1, "ZJHRKCPLYJ");
    }
}