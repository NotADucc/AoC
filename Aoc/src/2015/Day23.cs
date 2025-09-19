using System.Xml.Linq;

namespace AoC._2015;

public class Day23 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc23.txt");
        long res_1 = 0, res_2 = 0;

        var instructions = File.ReadAllLines(file_name);

        res_1 = run_instructions(instructions);
        res_2 = run_instructions(instructions, 1);  

        return (res_1, res_2);
    }

    private long run_instructions(string[] instructions, int a_start = 0)
    {
        long[] registers = [a_start, 0]; char offset = 'a';
        static int return_jump_idx(string instruction) 
        {
            int positive = instruction[0] == '+' ? 1 : -1;
            int jump_offset = int.Parse(instruction.Substring(1));
            return (jump_offset - positive) * positive;
        }

        for (int i = 0; i < instructions.Length; i++)
        {
            string instruction = instructions[i];
            var split = instruction.Split(' ');
            string cmd = split[0];
            int register_loc = split[1][0] - offset;

            if (cmd.Equals("hlf"))
            {
                registers[register_loc] /= 2;
            }
            else if (cmd.Equals("tpl"))
            {
                registers[register_loc] *= 3;
            }
            else if (cmd.Equals("inc"))
            {
                registers[register_loc]++;
            }
            else if (cmd.Equals("jmp"))
            {
                i += return_jump_idx(split[1]);
            }
            else if (cmd.Equals("jie"))
            {
                register_loc = split[1][0] - offset;
                if ((registers[register_loc] & 1) == 0)
                {
                    i += return_jump_idx(split[2]);
                }
            }
            else if (cmd.Equals("jio"))
            {
                register_loc = split[1][0] - offset;
                if (registers[register_loc] == 1)
                {
                    i += return_jump_idx(split[2]);
                }
            }
            else
            {
                throw new ArgumentException("unknown instruction");
            }
        }

        return registers[1];
    }
}