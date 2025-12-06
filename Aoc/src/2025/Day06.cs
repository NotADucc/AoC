using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AoC._2025;

public class Day06 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc6.txt");
        long res_1 = 0, res_2 = 0;
        
        // it's already aligned....
        var lines = File.ReadAllLines(file_name);
        var worksheet = get_symbols(lines[^1]);

        for (int i = 0; i < lines.Length - 1; i++)
        {
            var line = lines[i];
            var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < split.Length; j++)
            {
                worksheet[j].Add(split[j]);
            }
        }
    
        res_1 = worksheet.Sum(x => x.Result);

        int max_len = lines.Max(line => line.Length);
        List<long> nums = new List<long>();
        var sb = new StringBuilder(max_len);
        var skip_cnt = 0;

        for (int i = 0; i <= max_len; i++)
        {
            bool is_skip = true;
            if (i < max_len)
            { 
                for (int j = 0; j < lines.Length - 1; j++)
                {
                    char dig = lines[j][i];
                    if (!dig.Equals(' '))
                    {
                        is_skip = false;
                        sb.Append(dig);
                    }
                }
            }

            if (is_skip)
            {
                Func<long, long, long> func = worksheet[skip_cnt].Symbol.Equals('+')
                    ? (x, y) => x + y
                    : (x, y) => x * y;

                res_2 += nums.Aggregate((x, y) => func(x, y));
                nums.Clear();
                skip_cnt++;
            }
            else 
            {
                nums.Add(long.Parse(sb.ToString()));
                sb.Clear();
            }
        }

        return (res_1, res_2);
    }
    private static List<FishMath> get_symbols(string line)
    {
        var worksheet = new List<FishMath>();
        var symbols = line;
        var symbol_split = symbols.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        for (int j = 0; j < symbol_split.Length; j++)
        {
            var symbol = symbol_split[j];
            worksheet.Add(new(symbol[0]));
        }

        return worksheet;
    }
    private class FishMath
    {
        public FishMath(char symbol)
        {
            Symbol = symbol;
            if (Symbol.Equals('*'))
                Result = 1;
        }

        public char Symbol { get; set; }
        public List<string> RawNums { get; set; } = [];
        public List<int> Nums { get; set; } = [];
        public long Result { get; set; }
        public void Add(string num)
        {
            RawNums.Add(num);
            var parsed = long.Parse(num.Trim());
            if (Symbol.Equals('*'))
                Result *= parsed;
            else
                Result += parsed;
        }
    }
}