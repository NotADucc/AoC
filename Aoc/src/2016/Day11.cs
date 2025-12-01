
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AoC._2016;

public class Day11 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc11.txt");
        var lines = File.ReadAllLines(file_name);

        lines = """
            The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
            The second floor contains a hydrogen generator.
            The third floor contains a lithium generator.
            The fourth floor contains nothing relevant.
            """.Split("\n");

        long res_1 = 0, res_2 = 0;
        const int MAX_FLOORS = 4;
        Floor[] floors = new Floor[MAX_FLOORS];
        for (int i = 0; i < MAX_FLOORS; i++)
            floors[i] = new Floor();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var floor = floors[i];

            line = line.Substring(line.IndexOf("contains") + "contains".Length);

            var split = line
                .Replace("nothing relevant", "")
                .Split(" a ")
                .Select(x => x.Replace(" and", "").Replace(",", "").Replace(".", "").Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            for (int j = 0; j < split.Count; j++)
            {
                floor.AddRaw(split[j]);
            }
        }

        res_1 = Solve1(floors);

        return (res_1, res_2);
    }

    // elavator needs an item to move +1 or -1
    // max 2 items in elavator
    // chip without matching generator with random generator == fried
    private int Solve1(Floor[] floors)
    {
        int res = int.MaxValue;

        void rec(int depth, int steps, List<Elavator> elavator) 
        {
            if (depth > floors.Length || depth < 0 || steps >= res)
                return;

            if (depth == floors.Length)
            {
                if (Extensions.IsEmpty(floors))
                {
                    res = Math.Min(res, steps);
                    return;
                }
            }

            var floor = floors[depth];

            for (int i = 0; i < floor.Generators.Count; i++)
            {
                for (int j = 0; j < floor.Microchips.Count; j++)
                {
                    
                }
            }
        }

        rec(0, 0, []);

        return res;
    }

    private class Elavator
    {
        public List<string> Generators { get; set; } = [];
        public List<string> Microchips { get; set; } = [];

        public bool IsSafeToLeave()
        {
            Dictionary<string, int> freq = Generators
                .ToDictionary(x => x, _ => 1);

            foreach (var chip in Microchips)
            {
                if (!freq.ContainsKey(chip))
                    return false;
            }

            return true;
        }
        public bool CanMove() => TotalItems() >= 1;
        public bool OverCapacity() => TotalItems() > 2;
        public int TotalItems() => Generators.Count + Microchips.Count;
    }

    private class Floor
    {
        public List<string> Generators { get; set; } = [];
        public List<string> Microchips { get; set; } = [];

        public void AddRaw(string text)
        {
            if (text.Contains("generator"))
            {
                Generators.Add(text.Replace(" generator", ""));
            }
            else
            {
                Microchips.Add(text.Replace("-compatible microchip", ""));
            }
        }
        public bool IsSafeToLeave()
        {
            Dictionary<string, int> freq = Generators
                .ToDictionary(x => x, _ => 1);

            foreach (var chip in Microchips)
            {
                if (!freq.ContainsKey(chip))
                    return false;
            }

            return true;
        }
        public int TotalItems() => Generators.Count + Microchips.Count;
    }

    private static class Extensions
    {
        public static bool IsEmpty(Floor[] container)
        {
            for (int i = 0; i < container.Length - 1; i++)
            {
                if (container[i].TotalItems() > 0)
                    return false;
            }

            return true;
        }
    }
}