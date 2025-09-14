namespace AoC._2015;

public class Day01 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc1.txt");
        long res_1 = 0, res_2 = 0;

        string blob = File.ReadAllText(file_name);

        for (int i = 0; i < blob.Length; i++)
        {
            char ch = blob[i];
            int offset = ch.Equals('(') ? 1 : -1;
            res_1 += offset;
            if (res_2 == 0 && res_1 == -1)
                res_2 = i + 1;
        }


        return (res_1, res_2);
    }
}