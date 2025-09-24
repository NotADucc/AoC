
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC._2016;

public class Day09 : IRun<long, long>
{
    private static readonly Regex regex = new Regex(@"\((\d+)x(\d+)\)", RegexOptions.Compiled);
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc9.txt");
        long res_1 = 0, res_2 = 0;

        var compressed_data = File.ReadAllText(file_name).TrimEnd();
        int n = compressed_data.Length;

        for (int i = 0; i < n; i++)
        {
            if (compressed_data[i] != '(')
            {
                res_1++;
                continue;
            }

            var match = regex.Match(compressed_data, i);

            int take = int.Parse(match.Groups[1].Value);
            int count = int.Parse(match.Groups[2].Value);
            int len = match.Length;

            int add = take * count;
            res_1 += add;

            i += len - 1;
            i += take;
        }

        res_2 = get_res_2(compressed_data, 0, n);

        return (res_1, res_2);
    }
    private static long get_res_2(string input, int start, int end)
    {
        long res = 0;

        for (int i = start; i < end; i++)
        {
            if (input[i] == '(')
            {
                var match = regex.Match(input, i);

                int take = int.Parse(match.Groups[1].Value);
                int count = int.Parse(match.Groups[2].Value);

                int len = match.Length;
                start = i + len;
                int new_end = start + take;

                res += get_res_2(input, start, new_end) * count;

                i = new_end - 1;
            }
            else
            {
                res++;
            }
        }

        return res;
    }
}