using System.Text;

namespace AoC._2015;

public class Day08 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc8.txt");
        string[] literals = File.ReadAllLines(file_name);
        long res_1 = 0, res_2 = 0, total_literal_len = 0;

        foreach (string literal in literals)
        {
            total_literal_len += literal.Length;
            res_1 += get_code_length(literal);

            string longer_literal = expand_literal(literal);
            res_2 += longer_literal.Length;
        }

        return (total_literal_len - res_1, res_2 - total_literal_len);
    }

    private int get_code_length(string literal)
    {
        // escaped sym: \\ \" \x__    
        int res = 0;

        for (int i = 0; i < literal.Length; i++)
        {
            if (literal[i] == '\"')
            {
                continue;
            }

            res++;
            if (literal[i] != '\\') { }
            else if (literal[i + 1] == '\\' || literal[i + 1] == '\"')
            {
                i++;
            }
            else 
            {
                i+=3;
            }
        }

        return res;
    }
    private string expand_literal(string literal)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append('\"');
        // escapes sym: \ "    
        for (int i = 0; i < literal.Length; i++)
        {
            if (literal[i] == '\"')
            {
                sb.Append("\\\"");
            }
            else if (literal[i] == '\\')
            {
                sb.Append("\\\\");
            }
            else
            {
                sb.Append(literal[i]);
            }
        }

        sb.Append('\"');
        return sb.ToString();
    }
}