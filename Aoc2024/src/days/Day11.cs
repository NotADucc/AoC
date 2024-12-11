using AoC;

public class Day11 : IRun
{
    public (long, long) Run()
    {
        (bool, long, long) IsEven(long num)
        { 
            var str = num.ToString();
            var len = str.Length >> 1;
            return (str.Length & 1) == 0 
                ? (true, long.Parse(str.Substring(0, len)), long.Parse(str.Substring(len))) 
                : (false, -1, -1);            
        }

        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc11.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadLines(file_name)
            .Select(line => line.Split().Select(long.Parse))
            .SelectMany(x => x)
            .ToArray();

        var lst_1 = new List<long>(input);

        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < lst_1.Count; j++)
            {
                if (lst_1[j] == 0)
                {
                    lst_1[j] = 1;
                    continue;
                }
                var res = IsEven(lst_1[j]);
                if (res.Item1)
                {
                    lst_1[j] = res.Item2;
                    lst_1.Insert(j + 1, res.Item3);
                    j++;
                }
                else 
                {
                    lst_1[j] *= 2024;
                }
            }
        }

        return (lst_1.Count, res_2);
    }
}