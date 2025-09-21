using System.Text;

namespace AoC._2016;

public class Day05 : IRun<string, string>
{
    public (string, string) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc5.txt");

        var door_id = File.ReadAllText(file_name);
        Span<char> advanced_password = stackalloc char[8];
        string password = string.Empty;

        for (int i = 0; password.Length < 8 || advanced_password.Contains('\0'); i++)
        {
            var hash = CreateMD5(Create_key(door_id, i));
            if (prefix_0(hash))
            {
                if (password.Length < 8)
                    password += hash[5];

                int idx = hash[5] - '0';
                if (idx < advanced_password.Length)
                {
                    if (advanced_password[idx] == '\0')
                        advanced_password[idx] = hash[6];
                }
            }
        }

        return (password.ToLower(), new string(advanced_password).ToLower());
    }
    private static bool prefix_0(string hash)
        => hash[0] == '0'
        && hash[1] == '0'
        && hash[2] == '0'
        && hash[3] == '0'
        && hash[4] == '0';
    private static string Create_key(string base_input, int num)
        => $"{base_input}{num}";
    private static string CreateMD5(string input)
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes); // .NET 5 +
    }
}