using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC._2015;

public class Day19 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc19.txt");
        long res_1 = 0, res_2 = long.MaxValue;

        bool seen_newline = false;
        Dictionary<string, List<string>> replacements = new();
        string molecules = string.Empty;

        foreach (string line in File.ReadAllLines(file_name))
        {
            if (string.IsNullOrEmpty(line))
            {
                seen_newline = true;
                continue;
            }
            if (!seen_newline)
            {
                var split = line.Split(" => ");
                replacements.TryAdd(split[0], []);
                replacements[split[0]].Add(split[1]);
            }
            else
            {
                molecules = line;
            }
        }

        HashSet<string> unique_molecoles = new HashSet<string>();
        for (int i = 0; i < molecules.Length; i++)
        {
            string mol = molecules[i].ToString();
            if (replacements.TryGetValue(mol, out var lst))
            {
                string temp_prefix = molecules.Substring(0, i);
                string temp_suffix = molecules.Substring(i + 1);
                foreach (var replacement in lst)
                {
                    string replaced = $"{temp_prefix}{replacement}{temp_suffix}";
                    unique_molecoles.Add(replaced);
                }
            }

            if (i < molecules.Length - 1)
            {
                mol = molecules.Substring(i, 2);
                if (replacements.TryGetValue(mol, out lst))
                {
                    string temp_prefix = molecules.Substring(0, i);
                    string temp_suffix = molecules.Substring(i + 2);
                    foreach (var replacement in lst)
                    {
                        string replaced = $"{temp_prefix}{replacement}{temp_suffix}";
                        unique_molecoles.Add(replaced);
                    }
                }
            }
        }

        res_1 = unique_molecoles.Count;

        // surely working reverse is faster???
        Dictionary<string, string> reverse_replacements = new();
        foreach (var kvp in replacements)
        {
            foreach (var molecu in kvp.Value)
            {
                reverse_replacements.Add(molecu, kvp.Key);
            }
        }

        // gets the first result that equals to "e" and not lowest step count but it seems to work for my dataset
        res_2 = transform_molecule(
            reverse_replacements,
            molecules,
            "e",
            0
        );

        return (res_1, res_2);
    }
    private long transform_molecule(
        Dictionary<string, string> replacements,
        string current_molecule,
        string end_molecule,
        int step
    )
    {
        if (current_molecule.Equals(end_molecule))
        {
            return step;
        }

        foreach (var replacement in replacements)
        {
            var idx = current_molecule.IndexOf(replacement.Key);

            if (idx < 0)
                continue;

            string temp_prefix = current_molecule.Substring(0, idx);
            string temp_suffix = current_molecule.Substring(idx + replacement.Key.Length);
            string replaced = $"{temp_prefix}{replacement.Value}{temp_suffix}";
            return transform_molecule(
                replacements,
                replaced,
                end_molecule,
                step + 1
            );
        }

        return -1;
    }
}