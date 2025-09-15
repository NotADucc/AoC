using System.Collections.Generic;

namespace AoC._2015;

public class Day04 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc4.txt");
        string data = File.ReadAllText(file_name);

        long res_1 = 0, res_2 = 0;
        while (!has_prefix(CreateMD5(create_key(data, res_1)), true)) res_1++;
        res_2 = res_1;
        while (!has_prefix(CreateMD5(create_key(data, res_2)), false)) res_2++;

        return (res_1, res_2);
    }

    private static bool has_prefix(string hash, bool sol_1)
        => sol_1 
        ? hash[0] == '0' && hash[1] == '0' && hash[2] == '0' && hash[3] == '0' && hash[4] == '0'
        : hash[0] == '0' && hash[1] == '0' && hash[2] == '0' && hash[3] == '0' && hash[4] == '0' && hash[5] == '0';
    private static string create_key(string b, long suffix) => $"{b}{suffix}";
    public static string CreateMD5(string input)
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes); // .NET 5 +
    }
}