using System.Collections.Generic;
using System.ComponentModel.Design;

namespace AoC._2015;

public class Day07 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc7.txt");

        string[] instructions = File.ReadAllLines(file_name);
        var mem = build_memo(instructions);
        var instructions_dct = build_instructions(instructions);
        int res_1 = get_wire_value(mem, instructions_dct, "a");

        mem = build_memo(instructions);
        mem.Add("b", res_1);
        int res_2 = get_wire_value(mem, instructions_dct, "a");

        return (res_1, res_2);
    }

    private Dictionary<string, string> build_instructions(string[] instructions)
        => instructions
            .Select(x => x.Split(" -> "))
            .ToDictionary(x => x[1], x => x[0]);

    // build memo with every int in instructions
    // so i dont have to worry about ints later
    private Dictionary<string, int> build_memo(string[] instructions)
        => instructions
            .SelectMany(x => x.Split(' '))
            .Where(x => int.TryParse(x, out int p))
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => int.Parse(x.Key));

    private static readonly Dictionary<string, Func<int, int, int>> bitwise_ops = new() 
    {
        {  "OR ",        (l, r) => l | r },
        {  "AND ",       (l, r) => l & r },
        {  "LSHIFT ",    (l, r) => l << r},
        {  "RSHIFT ",    (l, r) => l >> r},
        {  "NOT ",       (l, _) => ~l    },
    };
    private int get_wire_value(Dictionary<string, int>  mem, Dictionary<string, string> instructions, string wire)
    { 
        if (mem.TryGetValue(wire, out int value))
            return value;

        string instruction = instructions[wire];
        bool found = false; int wire_val = 0;
        foreach (var op in bitwise_ops)
        {
            if (instruction.Contains(op.Key))
            {
                var cleaned_instr = instruction.Replace(op.Key, "");
                var wires = cleaned_instr.Split(" ");

                var l = get_wire_value(mem, instructions, wires[0]);
                var r = get_wire_value(mem, instructions, wires[^1]);
                wire_val = op.Value(l, r);
                found = true;
                break;
            }
        }

        if (!found)
        {
            wire_val = get_wire_value(mem, instructions, instruction);
        }

        mem.TryAdd(wire, wire_val);
        return mem[wire];
    }
}