using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Printing;
using System.Text;

namespace AoC._2015;

public class Day10 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc10.txt");
        string num = File.ReadAllText(file_name);

        for (int i = 0; i < 40; i++)
        {
            num = elve_speak(num);
        }

        int res_1 = num.Length;
        
        for (int i = 0; i < 10; i++)
        {
            num = elve_speak(num);
        }

        return (res_1, num.Length);
    }

    private string elve_speak(string num)
    { 
        StringBuilder sb = new StringBuilder();

        int l = 0, r = 0, n = num.Length;

        while (r < n)
        {
            if (r < n)
            {
                while (r < n && num[l] == num[r])
                {
                    r++;
                }
            }
            sb.Append(r - l);
            sb.Append(num[l]);
            l = r;
        }

        return sb.ToString();
    }
}