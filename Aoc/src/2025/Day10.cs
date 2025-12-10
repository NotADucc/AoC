using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AoC._2025;

public class Day10 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc10.txt");
        long res_1 = 0, res_2 = 0;

        // dp memo?
        // dont think prefix sum ish solution will help here
        var lines = File.ReadAllLines(file_name);
        var machines = transform(lines)
            .ToList();

        res_1 = rec(machines, (machine) => machine.is_done());
        //res_2 = rec(machines, (machine) => machine.has_reached_joltage());

        return (res_1, res_2);
    }
    private long rec(List<Machine> machines, Predicate<Machine> pred) 
    {
        long res = 0;
        Dictionary<(string key, int idx), long> memo = new();

        void inner(Machine machine, long current_cost, ref long best_cost, int idx)
        {
            if (current_cost >= best_cost)
                return;

            //var key = string.Join(',', machine.ButtonsPressedCurrentState);
            //var state = (key, idx);

            //if (memo.TryGetValue(state, out long best) && best <= current_cost)
            //    return;

            //memo[state] = current_cost;

            if (pred.Invoke(machine))
            {
                best_cost = Math.Min(best_cost, current_cost);
            }
            else 
            {
                //if (machine.has_exceeded_joltage())
                //{
                //    return;
                //}

                for (int i = idx; i < machine.Buttons.Count; i++)
                {
                    machine.press(i, false);
                    //inner(machine, current_cost + 1, ref best_cost, i);
                    inner(machine, current_cost + 1, ref best_cost, i + 1);
                    machine.press(i, true);
                }
            }
        }

        foreach (var machine in machines)
        {
            long best_cost = long.MaxValue;
            inner(machine, 0, ref best_cost, 0);
            res += best_cost;
        }

        return res;
    }
    private IEnumerable<Machine> transform(string[] lines)
    {
        foreach (var line in lines)
        {
            var split = line.Split(" ");
            yield return new Machine(split[0], split.Skip(1).Take(split.Length - 2), split[^1]);
        }
    }
    private class Machine 
    {
        private const char ON = '#';
        public bool[] IdealLightState { get; set; }
        public bool[] CurrentLightState { get; set; }
        public List<Switch> Buttons { get; set; } = [];
        public int[] ButtonsPressedIdealState { get; set; }
        public int[] ButtonsPressedCurrentState { get; set; }

        public Machine(string state, IEnumerable<string> buttons, string joltages)
        {
            int n = state.Length - 2;

            IdealLightState = state
                .Select(x => x.Equals(ON))
                .Skip(1)
                .Take(n)
                .ToArray();
            CurrentLightState = new bool[n];
            ButtonsPressedIdealState = joltages
                .Replace("{", string.Empty)
                .Replace("}", string.Empty)
                .Split(",")
                .Select(x => int.Parse(x))
                .ToArray();
            ButtonsPressedCurrentState = new int[n];

            foreach (var button in buttons)
            {
                var idxs = button
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty)
                    .Split(",")
                    .Select(x => int.Parse(x))
                    .ToList();

                Buttons.Add(new Switch() { Idxs = idxs });
            }
        }
        public void press(int idx, bool reverse)
        {
            if (idx >= Buttons.Count)
                return;

            var button = Buttons[idx];
            var offset = reverse ? -1 : 1;

            for (int i = 0; i < button.Idxs.Count; i++)
            {
                var sw = button.Idxs[i];
                CurrentLightState[sw] = !CurrentLightState[sw];
                ButtonsPressedCurrentState[sw] += offset;
            }
        }
        public bool is_done()
        {
            for (int i = 0; i < IdealLightState.Length; i++)
            {
                if (IdealLightState[i] != CurrentLightState[i])
                    return false;
            }
            return true;
        }

        public bool has_exceeded_joltage()
        {
            for (int i = 0; i < ButtonsPressedIdealState.Length; i++)
            {
                if (ButtonsPressedCurrentState[i] > ButtonsPressedIdealState[i])
                    return true;
            }
            return false;
        }

        public bool has_reached_joltage()
        {
            for (int i = 0; i < ButtonsPressedIdealState.Length; i++)
            {
                if (ButtonsPressedCurrentState[i] != ButtonsPressedIdealState[i])
                    return false;
            }
            return true;
        }
    }
    private class Switch 
    {
        public List<int> Idxs { get; set; } = [];
    }
}