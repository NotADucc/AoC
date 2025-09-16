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

    public static string get_next_pw(string start)
    {
        char[] pw = start.ToCharArray();

        do
        {
            increment_pw(pw);
        } while (!is_valid(pw));

        return new string(pw);
    }

    private static bool is_valid(char[] pw)
    {
        bool has_stair = false;
        HashSet<char> pairs = new();

        for (int i = 0; i < pw.Length; i++)
        {
            if (BANNED_LETTERS.Contains(pw[i]))
                return false;

            if (i < pw.Length - 2 && pw[i] + 1 == pw[i + 1] && pw[i + 1] + 1 == pw[i + 2])
            {
                has_stair = true;
            }

            if (i < pw.Length - 1 && pw[i] == pw[i + 1])
            {
                pairs.Add(pw[i]);
            }
        }

        return has_stair && pairs.Count > 1;
    }

    private static void increment_pw(char[] pw)
    {
        int i = pw.Length - 1;
        while (i >= 0)
        {
            if (pw[i] == 'z')
            {
                pw[i] = 'a';
                i--;
            }
            else
            {
                pw[i]++;
                break;
            }
        }
    }
}