namespace AoC.days;

public class Day25 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc25.txt");
        long res_1 = 0, res_2 = 0;
        const int SIZE = 7;
        var locks = new List<Lock>();
        var keys = new List<Key>();
        var input = File.ReadAllLines(file_name)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        for (int i = 0; i < input.Length; i += SIZE)
        {
            var pad = input
                .Skip(i)
                .Take(SIZE)
                .ToList();
            if (pad[0][0] == '#')
            {
                locks.Add(new Lock(pad));
            }
            else 
            {
                keys.Add(new Key(pad));
            }
        }

        for (int i = 0; i < locks.Count; i++)
        {
            for (int j = 0; j < keys.Count; j++)
            {
                if (locks[i].Fit(keys[j]))
                { 
                    res_1++;
                }
            }
        }

        return (res_1, res_2);
    }
    private class Lock 
    { 
        private List<int> pins = [];
        private const char LOCK_SIGN = '#';
        private readonly int SIZE = 0;
        public Lock(List<string> input)
        {
            if (input is null || input.Count == 0 || input[0][0] != LOCK_SIGN)
            {
                throw new ArgumentException("Incorrect lock");
            }
            SIZE = input.Count - 2;
            pins = Enumerable.Repeat(0, input[0].Length).ToList();
            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 1; j < input.Count; j++)
                {
                    if (input[j][i] != LOCK_SIGN)
                    { 
                        continue;
                    }
                    pins[i]++;
                }
            }
        }

        public bool Fit(Key key) 
        {
            var key_pins = key.GetPins();
            for (int i = 0; i < pins.Count; i++)
            {
                if (key_pins[i] + pins[i] > SIZE)
                {
                    return false;
                }
            }
            return true;
        }
    }

    private class Key
    {
        private List<int> pins = [];
        private const char KEY_SIGN = '.';
        private readonly int SIZE = 0;
        public Key(List<string> input)
        {
            if (input is null || input.Count == 0 || input[0][0] != KEY_SIGN)
            {
                throw new ArgumentException("Incorrect key");
            }
            SIZE = input.Count - 2;
            pins = Enumerable.Repeat(SIZE, input[0].Length).ToList();
            for (int i = 0; i < input[0].Length; i++)
            {
                for (int j = 1; j < input.Count; j++)
                {
                    if (input[j][i] != KEY_SIGN)
                    {
                        continue;
                    }
                    pins[i]--;
                }
            }
        }
        public IReadOnlyList<int> GetPins() => pins.AsReadOnly();
    }
}