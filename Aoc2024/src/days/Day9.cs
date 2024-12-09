using AoC;

public class Day9 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc9.txt");
        long res_1 = 0, res_2 = 0;
        
        var input = File.ReadAllText(file_name);
        List<int> lst_1 = new List<int>();
        bool is_file = true;
        int file_id = 0;
        foreach (var ch in input)
        {
            int len = ch - '0';
            int val = is_file ? file_id++ : -1;
            while (len > 0)
            {
                lst_1.Add(val);
                len--;
            }
            is_file = !is_file;
        }
        int l = 0, r = lst_1.Count - 1;
        List<int> lst_2 = new List<int>(lst_1);
        while (true)
        {
            while (lst_1[l] != -1) l++;
            while (lst_1[r] == -1) r--;
            if (l >= r) break;
            lst_1[l] ^= lst_1[r];
            lst_1[r] ^= lst_1[l];
            lst_1[l] ^= lst_1[r];
        }

        for (int i = 0; i < lst_1.Count; i++)
        {
            if (lst_1[i] != -1)
            {
                res_1 += (long)i * lst_1[i];
                res_2 += (long)i * lst_2[i];
            }
        }

        return (res_1, res_2);
    }
}