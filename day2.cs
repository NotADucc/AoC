public class Program
{
    private static void Main(string[] args)
    {
        const string file_name = @"";

        bool CheckSafety(List<int> lst, Func<int, int, bool> pred) => lst
            .Zip(lst.Skip(1))
            .All(x => x.First != x.Second && pred(x.First, x.Second));

        Func<int, int, bool> GivePredicate(bool input) => input
                ? (prev, curr) => prev < curr && prev + 3 >= curr
                : (prev, curr) => prev > curr && prev - 3 <= curr;

        int res_1 = 0, res_2 = 0;

        foreach (string line in File.ReadAllLines(file_name))
        {
            var lst = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            res_1 += CheckSafety(lst, GivePredicate(lst[0] < lst[^1])) ? 1 : 0;
            res_2 += lst
                .Select((_, index) => index)
                .Where(index =>
                {
                    var tempList = lst.Where((_, idx) => idx != index).ToList();

                    return CheckSafety(tempList, GivePredicate(tempList[0] < tempList[^1]));
                })
                .Any() ? 1 : 0;
        }

        Console.WriteLine($"Res 1 : {res_1}");
        Console.WriteLine($"Res 2 : {res_2}");
    }
}

