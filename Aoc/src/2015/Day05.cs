using System.Collections.Generic;

namespace AoC._2015;

public class Day05 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc5.txt");
        long res_1 = 0, res_2 = 0;

        foreach (string word in File.ReadAllLines(file_name))
        {
            if (sol_1(word)) res_1++;
            if (sol_2(word)) res_2++;
        }

        return (res_1, res_2);
    }

    private static bool is_vowel(char ch)
        => ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u';

    private static bool is_bad(char ch1, char ch2)
    => (ch1 == 'a' && ch2 == 'b') 
        || (ch1 == 'c' && ch2 == 'd')
        || (ch1 == 'p' && ch2 == 'q')
        || (ch1 == 'x' && ch2 == 'y');

    private static bool sol_1(string word)
    {
        bool is_nice = true;
        int vowel_count = 0, double_count = 0, n = word.Length;
        for (int i = 0; i < n; i++)
        {
            if (i < n - 1)
            {
                if (is_bad(word[i], word[i + 1]))
                {
                    is_nice = false;
                    break;
                }
                if (word[i] == word[i + 1]) double_count++;
            }
            if (is_vowel(word[i])) vowel_count++;
        }
        return is_nice && vowel_count >= 3 && double_count >= 1;
    }

    private static bool sol_2(string word)
    {
        Dictionary<string, int> freq = [];
        int n = word.Length;
        // build freq table
        string last_added = string.Empty;
        int last_added_idx = -1;
        for (int i = 0; i < n - 1; i++)
        {
            string key = word.Substring(i, 2);
            if (!freq.TryAdd(key, 1))
            {
                if (last_added_idx == i - 1 && last_added.Equals(key)) continue;
                freq[key]++;
            }
            else 
            {
                last_added = key;
                last_added_idx = i;
            }
        }

        bool is_nice = false;
        for (int i = 0; i < n - 2; i++)
        {
            if (word[i] == word[i + 2])
            { 
                is_nice = true;
                break;
            }
        }

        return is_nice && freq.Any(x => x.Value > 1);
    }
}