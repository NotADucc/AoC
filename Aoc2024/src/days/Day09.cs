using AoC;

public class Day09 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetFilesDir(), "aoc9.txt");
        long res_1 = 0, res_2 = 0;
        
        var input = File.ReadAllText(file_name);
        var lst_1 = new List<int>();
        var lst_2 = new List<int>();
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
        lst_2 = new List<int>(lst_1);

        while (true)
        {
            while (lst_1[l] != -1) l++;
            while (lst_1[r] == -1) r--;
            if (l >= r) break;
            lst_1[l] ^= lst_1[r];
            lst_1[r] ^= lst_1[l];
            lst_1[l] ^= lst_1[r];
        }

        l = 0; r = lst_2.Count - 1;
        while (true)
        {
            while (lst_2[l] != -1) l++;
            while (lst_2[r] == -1) r--;
            if (l >= r) break;
            int temp_r = r;

            while (lst_2[temp_r] == lst_2[r]) temp_r--;

            while (l < temp_r)
            {
                while (lst_2[l] != -1) l++;
                int temp_l = l;
                while (lst_2[temp_l] == -1) temp_l++;
                if (l >= temp_r) break;
                if (temp_l - l >= r - temp_r)
                {
                    for (; temp_r < r; l++, r--)
                    {
                        lst_2[l] ^= lst_2[r];
                        lst_2[r] ^= lst_2[l];
                        lst_2[l] ^= lst_2[r];
                    }
                    break;
                }
                else
                {
                    l = temp_l;
                }
            }

            l = 0;
            r = temp_r;
        }

        for (int i = 0; i < lst_1.Count; i++)
        {
            if (lst_1[i] != -1) res_1 += (long)i * lst_1[i];
            if (lst_2[i] != -1) res_2 += (long)i * lst_2[i];
        }

        return (res_1, res_2);
    }
}