using AoC;
using System.Text.RegularExpressions;

public class Day03 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc3.txt");
        Regex rgx = new Regex("mul[(]\\d{1,3},\\d{1,3}[)]|do[(][)]|don't[(][)]", RegexOptions.Compiled);
        bool mul = true;

        var res = File.ReadAllLines(file_name)
            .Select(line =>
                rgx.Matches(line)
                    .Aggregate((0L, 0L), (curr, next) =>
                    {
                        var split = next.Value.Split(',');
                        if (split.Length == 2)
                        {
                            long res = long.Parse(split[0].Substring(4)) * int.Parse(split[1].Substring(0, split[1].Length - 1));
                            curr.Item1 += res;
                            curr.Item2 += mul ? res : 0L;
                        }
                        else
                        {
                            mul = next.Value.Equals("do()");
                        }
                        return curr;
                    })
            ).ToArray();

        long res_1 = res.Sum(x => x.Item1), res_2 = res.Sum(x => x.Item2);

        return (res_1, res_2);
    }
}
