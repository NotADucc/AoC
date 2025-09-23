using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AoC._2016;

public class Day07 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc7.txt");
        long res_1 = 0, res_2 = 0;
        var ip7s = File.ReadAllLines(file_name);

        foreach (var ip7 in ip7s)
        {
            if (is_tls(ip7))
                res_1++;
            if (is_ssl(ip7))
                res_2++;
        }

        return (res_1, res_2);
    }

    private static bool is_tls(ReadOnlySpan<char> ip7)
    {
        bool in_brackets = false, is_tls = false;

        for (int i = 0; i < ip7.Length - 3; i++)
        {
            char ch = ip7[i];
            if (ch.Equals('[') || ch.Equals(']'))
            { 
                in_brackets = ch.Equals('[');
                continue;
            }

            bool is_chunk_tls = ch.Equals(ip7[i + 3]) 
                && ip7[i + 1].Equals(ip7[i + 2])
                && !ch.Equals(ip7[i + 1]);
            is_tls |= is_chunk_tls;
            if (in_brackets && is_chunk_tls)
                return false;
        }

        return is_tls;
    }

    private static bool is_ssl(string ip7)
    {
        HashSet<string> babs = new();
        int slice = 0;
        while (true)
        {
            int start_idx = ip7.IndexOf("[", slice);
            if (start_idx == -1)
            {
                break;
            }
            slice = start_idx + 1;
            for (int i = slice; i < ip7.Length - 3; i++)
            {
                char ch = ip7[i];
                if (ch.Equals(']'))
                    break;

                if (ip7[i].Equals(ip7[i + 2]) && !ip7[i].Equals(ip7[i + 1]))
                { 
                    babs.Add($"{ip7[i + 1]}{ip7[i]}{ip7[i + 1]}");
                }
            }
        }

        bool in_brackets = false;

        for (int i = 0; i < ip7.Length - 2; i++)
        {
            char ch = ip7[i];
            if (ch.Equals('[') || ch.Equals(']'))
            {
                in_brackets = ch.Equals('[');
                continue;
            }

            if (in_brackets)
                continue;

            if (babs.Contains(ip7.Substring(i, 3)))
                return true;
        }

        return false;
    }
}