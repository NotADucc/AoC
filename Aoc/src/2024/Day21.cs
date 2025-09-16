namespace AoC._2024;

public class Day21 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc21.txt");
        long res_1 = 0, res_2 = 0;

        var input = File.ReadAllLines(file_name);

        foreach (var code in input)
        {
            var pads = new Pads();
            res_1 += pads.Solve(code);
        }

        return (res_1, res_2);
    }

    private class Pads
    { 
        public List<RobotPad> robotspads = [];
        public KeyPad keypad = new KeyPad();
        public Pads() 
        {
            for (int i = 0; i < 2; i++)
            {
                robotspads.Add(new RobotPad());
            }
        }
        public long Solve(string code)
        {
            return 0L;
        }
    }
    private class RobotPad : IPad
    {
        //robot
        //   ^ A -> start
        // < v >
        public int r = 0;
        public int c = 0;
        private readonly Dictionary<(char, char), int> mem = [];
        public char TranslateToChar() => r switch
        {
            0 when c == 1 => '^',
            0 when c == 2 => 'A',
            1 when c == 0 => '<',
            1 when c == 1 => 'v',
            1 when c == 2 => '>',
            _ => throw new ArgumentException("Out of bounds"),
        };
        public (int, int) TranslateToCoords(char ch) => ch switch
        {
            '^' => (0, 1),
            '<' => (1, 0),
            'v' => (1, 1),
            '>' => (1, 2),
            _ => throw new ArgumentException("Out of bounds"),
        };
        public int Distance(char to_char)
        {
            char curr = TranslateToChar();
            if (mem.ContainsKey((curr, to_char)))
            {
                (int, int) to_loc = TranslateToCoords(to_char);
                mem[(curr, to_char)] = Math.Abs(r - to_loc.Item1) + Math.Abs(c - to_loc.Item2);
            }
            return mem[(curr, to_char)];
        }
    }
    private class KeyPad : IPad
    {
        // keypad
        // 7 8 9
        // 4 5 6
        // 1 2 3
        //   0 A -> start
        public int r = 0;
        public int c = 0;
        private readonly Dictionary<(char, char), int> mem = [];
        public char TranslateToChar() => r switch
        {
            0 when c == 0 => '7',
            0 when c == 1 => '8',
            0 when c == 2 => '9',
            1 when c == 0 => '4',
            1 when c == 1 => '5',
            1 when c == 2 => '6',
            2 when c == 0 => '1',
            2 when c == 1 => '2',
            2 when c == 2 => '3',
            3 when c == 1 => '0',
            3 when c == 2 => 'A',
            _ => throw new ArgumentException("Out of bounds"),
        };
        public (int, int) TranslateToCoords(char ch) => ch switch
        {
            '0' => (3, 1),
            '1' => (2, 0),
            '2' => (2, 1),
            '3' => (2, 2),
            '4' => (1, 0),
            '5' => (1, 1),
            '6' => (1, 2),
            '7' => (0, 0),
            '8' => (0, 1),
            '9' => (0, 2),
            _ => throw new ArgumentException("Out of bounds"),
        };
        public int Distance(char to_char)
        {
            char curr = TranslateToChar();
            if (mem.ContainsKey((curr, to_char)))
            {
                (int, int) to_loc = TranslateToCoords(to_char);
                mem[(curr, to_char)] = Math.Abs(r - to_loc.Item1) + Math.Abs(c - to_loc.Item2);
            }
            return mem[(curr, to_char)];
        }
    }
    private interface IPad 
    {
        // marker incase i need it or sth
    }
}