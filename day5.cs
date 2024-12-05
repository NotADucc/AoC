public class Program
{
    private static void Main(string[] args)
    {
        const string file_name = @"";
        int res_1 = 0, res_2 = 0;

        bool first_half = false;
        var order = new Dictionary<int, List<int>>();

        List<int> BackTrack(Dictionary<int, List<int>> dct, List<int> curr, List<int> lst)
        {
            if (lst.Count == 0)
            {
                return curr;
            }

            for (int i = 0; i < lst.Count; i++)
            {
                if (curr.Count <= 0 || (dct.TryGetValue(lst[i], out var t) && t.Contains(curr[curr.Count - 1])))
                {
                    int val = lst[i];
                    curr.Add(val);
                    lst.RemoveAt(i);
                    List<int> result = BackTrack(dct, curr, lst);
                    if (result != null) return result;
                    curr.RemoveAt(curr.Count - 1);
                    lst.Insert(i, val);
                }
            }

            return null;
        }


        foreach (var line in File.ReadAllLines(file_name))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                first_half = true;
                continue;
            }

            if (!first_half)
            {
                var split = line.Split('|').Select(int.Parse).ToArray();
                order.TryAdd(split[0], new List<int>());
                order[split[0]].Add(split[1]);
            }
            else
            {
                var split = line.Split(',').Select(int.Parse).ToList();
                bool contains_mistake = false;
                for (int i = 0; i < split.Count - 1; i++)
                {
                    if (!order.TryGetValue(split[i], out var lst) || !lst.Contains(split[i + 1]))
                    {
                        contains_mistake = true;
                        split = BackTrack(order, [], split);
                        break;
                    }
                }

                int half = split[split.Count >> 1];
                if (!contains_mistake)
                {
                    res_1 += half;
                }
                else 
                {
                    res_2 += half;
                }
            }
        }

        Console.WriteLine($"Res 1 : {res_1}");
        Console.WriteLine($"Res 2 : {res_2}");
    }
}
