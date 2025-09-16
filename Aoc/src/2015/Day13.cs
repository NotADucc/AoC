using System.Drawing.Printing;
using System.Dynamic;
using System.IO.IsolatedStorage;
using System.Text.Json;

namespace AoC._2015;

public class Day13 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc13.txt");
        string[] instructions = File.ReadAllLines(file_name);
        long res_1 = 0, res_2 = 0;

        Dictionary<string, Dictionary<string ,int>> happiness_dct = new();
        List<string> seated = new();
        foreach (var instruction in instructions)
        {
            var (target, next_to_target, happiness) = extract_instruction(instruction);

            if (!happiness_dct.TryAdd(target, new Dictionary<string, int>() {  { next_to_target, happiness } }))
            {
                happiness_dct[target].Add(next_to_target, happiness);
            }
        }

        foreach (var kvp in happiness_dct)
        {
            seated.Add(kvp.Key);
            seat_people(
                happiness_dct,
                seated,
                kvp.Key,
                0,
                ref res_1
            );
            seated.Remove(kvp.Key);
        }

        var attendees = happiness_dct.Keys.ToArray();
        happiness_dct.Add("me", []);
        foreach (var attendee in attendees)
        {
            happiness_dct["me"].Add(attendee, 0);
            happiness_dct[attendee].Add("me", 0);
        }

        foreach (var kvp in happiness_dct)
        {
            seated.Add(kvp.Key);
            seat_people(
                happiness_dct,
                seated,
                kvp.Key,
                0,
                ref res_2
            );
            seated.Remove(kvp.Key);
        }

        return (res_1, res_2);
    }

    private static void seat_people(
        Dictionary<string, Dictionary<string, int>> happiness,
        List<string> seated,
        string current_attendee,
        long current_happiness,
        ref long most_happy
    )
    {
        if (happiness.Count == seated.Count && seated[0] == current_attendee)
        {
            most_happy = Math.Max(most_happy, current_happiness); 
            return;
        }

        foreach (var kvp in happiness[current_attendee])
        {
            if (happiness.Count == seated.Count)
            {
                if (seated[0] != kvp.Key)
                    continue;

                seat_people(
                    happiness,
                    seated,
                    kvp.Key,
                    current_happiness + kvp.Value + happiness[kvp.Key][current_attendee],
                    ref most_happy
                );
            }
            else if (seated.Contains(kvp.Key))
            { 
                continue;
            }
            else 
            { 
                seated.Add(kvp.Key);
                seat_people(
                    happiness,
                    seated,
                    kvp.Key,
                    current_happiness + kvp.Value + happiness[kvp.Key][current_attendee],
                    ref most_happy
                );
                seated.Remove(kvp.Key);
            }
        }
    }
    private static (string target, string next_to_target, int happiness) extract_instruction(string instruction) 
    {
        var splitted_space = instruction
            .Split(' ');

        int happiness = int.Parse(splitted_space[3]);

        if (splitted_space.Contains("lose"))
            happiness *= -1;

        return (splitted_space[0], splitted_space[^1].Replace(".", ""), happiness);
    }
}