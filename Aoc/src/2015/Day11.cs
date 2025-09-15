using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Printing;
using System.Text;

namespace AoC._2015;

public class Day11 : IRun<string, string>
{
    public (string, string) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc11.txt");
        string pw = File.ReadAllText(file_name);
        string new_pw = get_next_pw(pw);
        return (new_pw, get_next_pw(new_pw));
    }

    private static readonly char[] BANNED_LETTERS = ['i', 'o', 'l'];
    private static bool is_staircase(char ch1, char ch2, char ch3) =>
        ch1 + 1 == ch2 && ch2 + 1 == ch3;
    private string get_next_pw(string pw)
    { 
        string valid_pw = string.Empty;

        for (int offset = 1;; offset++)
        {
            // treating the passwords like excel columns
            string test_pw = int_to_pw(pw_to_int(pw) + offset);
            HashSet<string> pairs = [];
            int n = test_pw.Length, staircase_count = 0, last_added_idx = -1;
            string last_added = string.Empty;
            bool is_valid = true;
            for (int i = 0; i < n; i++)
            {
                char ch = test_pw[i];
                if (BANNED_LETTERS.Contains(ch))
                {
                    is_valid = false; break;
                }

                if (i < n - 2)
                {
                    if (is_staircase(test_pw[i], test_pw[i + 1], test_pw[i + 2]))
                    {
                        staircase_count++;
                    }
                }

                if (i < n - 1)
                {
                    string key = test_pw.Substring(i, 2);
                    if (key[0] == key[1])
                    {
                        pairs.Add(key);
                    }
                }
            }

            if (is_valid && staircase_count > 0 && pairs.Count > 1)
            {
                valid_pw = test_pw;
                break;
            }
        }

        return valid_pw;
    }

    private const int _ASCII_BASE = 'a';
    private string int_to_pw(long pw_long)
    {
        StringBuilder sb = new StringBuilder();

        while (pw_long > 0)
        {
            long remainder = (pw_long - 1) % 26;

            char c = (char)(_ASCII_BASE + remainder);

            sb.Insert(0, c);

            pw_long = (pw_long - 1) / 26;
        }

        return sb.ToString();
    }

    public long pw_to_int(string pw)
    {
        long result = 0;
        long mpl = 1;
        for (int i = pw.Length - 1; i >= 0; --i)
        {
            result += (pw[i] - _ASCII_BASE + 1) * mpl;
            mpl *= 26;
        }
        return result;
    }
}