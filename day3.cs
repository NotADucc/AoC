using System.Text.RegularExpressions;

public class Program
{
    private static void Main(string[] args)
    {
        const string file_name = @"";
        Regex rgx = new Regex("mul[(]\\d{1,3},\\d{1,3}[)]|do[(][)]|don't[(][)]");
        bool mul = true;

        var res = File.ReadAllLines(file_name)
            .Select(line =>
                rgx.Matches(line)
                    .Select(x =>
                    {
                        var split = x.Value.Split(',');
                        if (split.Length == 2)
                        {
                            long res = long.Parse(split[0].Substring(4)) * int.Parse(split[1].Substring(0, split[1].Length - 1));
                            return new { res_1 = res, res_2 = mul ? res : 0L };
                        }
                        else 
                        {
                            mul = x.Value.Equals("do()");
                            return new { res_1 = 0L, res_2 = 0L };
                        }
                    })
            ).SelectMany(x => x)
            .ToArray();

        Console.WriteLine($"Res 1 : {res.Sum(x => x.res_1)}");
        Console.WriteLine($"Res 2 : {res.Sum(x => x.res_2)}");
    }
}
