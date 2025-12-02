namespace AoC._2025;

public class Day02 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc2.txt");
        long res_1 = 0, res_2 = 0;

        // read list
        // split on ,
        // take half and compare if it contains repeating nums
        //   if uneven can skip check?
        // would bruteforce solution, is there an elegant way?

        List<long> invalid_ids = [];
        var blob = File.ReadAllText(file_name);
        var lines = blob.Split(',');

        res_1 = solution_1(lines).Sum();
        res_2 = solution_2(lines).Sum();

        return (res_1, res_2);
    }

    private List<long> solution_1(string[] lines)
    {
        List<long> invalid_ids = [];
        foreach (var line in lines)
        {
            var ranges = line
                .Split('-')
                .Select(long.Parse)
                .ToArray();
            long start = ranges[0];
            long end = ranges[1];

            for (long i = start; i <= end; i++)
            {
                var (len, is_even_len) = has_even_length(i);
                if (!is_even_len) continue;

                long first_half = i / (long)Math.Pow(10, len / 2);
                long second_half = i % (long)Math.Pow(10, len / 2);
                if (first_half == second_half)
                    invalid_ids.Add(i);
            }
        }
        return invalid_ids;
    }

    private List<long> solution_2(string[] lines)
    {
        List<long> invalid_ids = [];
        foreach (var line in lines)
        {
            var ranges = line
                .Split('-')
                .Select(long.Parse)
                .ToArray();
            long start = ranges[0];
            long end = ranges[1];
            // either tostring everything
            // or be smart and do some string manip
            // im not doing the smart way el hehe

            for (long i = start; i <= end; i++)
            {
                string num = i.ToString();
                var num_len = num.Length;

                // 12341234
                // what if u have 
                // 124512
                // ig it has to repeat all the way


                for (int comparison_len = 1; comparison_len < num_len; comparison_len++)
                {
                    bool matches = true;
 
                    for (int num_idx = comparison_len; num_idx < num_len; num_idx += comparison_len)
                    {
                        if (num_len % comparison_len != 0)
                        {
                            matches = false;
                            continue;
                        }

                        var compare_against = num.Substring(0, comparison_len);

                        if (num_idx + comparison_len > num_len)
                        {
                            matches = false;
                            continue;
                        }

                        var sub = num.Substring(num_idx, comparison_len);
                        if (!compare_against.Equals(sub))
                        {
                            matches = false;
                            break;
                        }
                    }
                    if (matches)
                    {
                        invalid_ids.Add(i);
                        break;
                    }
                }
            }
        }
        return invalid_ids;
    }

    private (int, bool) has_even_length(long num)
    {
        int count = 0;
        while (num > 0)
        {
            num /= 10;
            count++;
        }
        return (count, (count & 1) == 0);
    }
}