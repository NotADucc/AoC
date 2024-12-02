public class Program
{
    private static void Main(string[] args)
    {
        const string file_name = @"";

        bool CheckSafety(List<int> lst, Func<int, int, bool> func)
        {
            for (int i = 1; i < lst.Count; i++)
            {
                if (lst[i - 1] == lst[i] || !func(lst[i - 1], lst[i]))
                {
                    return false;
                }
            }
            return true;
        }

        int res_1 = 0, res_2 = 0;

        foreach (string line in File.ReadAllLines(file_name))
        {
            var lst = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            Func<int, int, bool> func = lst[0] < lst[^1]
                ? (prev, curr) => prev < curr && prev + 3 >= curr
                : (prev, curr) => prev > curr && prev - 3 <= curr;

            if (CheckSafety(lst, func))
            {
                res_1++;
                res_2++;
            }
            else 
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    int temp = lst[i];
                    lst.RemoveAt(i);

                    func = lst[0] < lst[^1]
                        ? (prev, curr) => prev < curr && prev + 3 >= curr
                        : (prev, curr) => prev > curr && prev - 3 <= curr;

                    if (CheckSafety(lst, func))
                    {
                        res_2++;
                        break;
                    }

                    lst.Insert(i, temp);
                }
            }
        }

        Console.WriteLine($"Res 1 : {res_1}");
        Console.WriteLine($"Res 2 : {res_2}");
    }
}
