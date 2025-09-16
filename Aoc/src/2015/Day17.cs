using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Dynamic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace AoC._2015;

public class Day17 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc17.txt");
        string[] instructions = File.ReadAllLines(file_name);
        long res_1 = 0, res_2 = 0;
        const int EGGNOG_AMOUNT = 150;

        var containers = instructions
            .Select(int.Parse)
            .ToList();

        Dictionary<int, int> freq = [];
        calc_containers(
            freq,
            containers,
            [],
            EGGNOG_AMOUNT,
            0,
            ref res_1
        );

        res_2 = freq.MinBy(x => x.Key).Value;

        return (res_1, res_2);
    }

    private void calc_containers(
        Dictionary<int, int> freq,
        List<int> containers,
        List<int> current,
        int eggnog,
        int idx,
        ref long total
    )
    {
        if (eggnog <= 0)
        {
            if (eggnog == 0)
            {
                total++;
                freq.TryAdd(current.Count, 0);
                freq[current.Count]++;
            }
            return;
        }

        for (int i = idx; i < containers.Count; i++)
        {
            int container = containers[i];
            current.Add(container);
            calc_containers(
                freq,
                containers,
                current,
                eggnog - container,
                i + 1,
                ref total
            );
            current.RemoveAt(current.Count - 1);
        }
    }
}