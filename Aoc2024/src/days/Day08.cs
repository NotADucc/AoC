using AoC;

public class Day08 : IRun
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc8.txt");
        int n = 0, m = 0;
        
        var antennas = new Dictionary<char, List<(int, int)>>();
        foreach (var line in File.ReadAllLines(file_name))
        {
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] != '.')
                {
                    antennas.TryAdd(line[j], new List<(int, int)>());
                    antennas[line[j]].Add((n, j));
                }
            }
            n++;
            m = line.Length;
        }

        bool IsInBounds(int r, int c) => !(r < 0 || c < 0 || r >= n || c >= m);
        var anti_1 = new HashSet<(int, int)>();
        var anti_2 = new HashSet<(int, int)>();
        foreach (var kvp in antennas)
        {
            var coord = kvp.Value;
            for (int i = 0; i < coord.Count; i++)
            {
                var antenna1 = coord[i];
                for (int j = i + 1; j < coord.Count; j++)
                {
                    var antenna2 = coord[j];
                    var delta = (antenna2.Item1 - antenna1.Item1, antenna2.Item2 - antenna1.Item2);

                    if (IsInBounds(antenna1.Item1 - delta.Item1, antenna1.Item2 - delta.Item2)) 
                        anti_1.Add((antenna1.Item1 - delta.Item1, antenna1.Item2 - delta.Item2));
                    
                    var temp = antenna1;
                    while (IsInBounds(temp.Item1, temp.Item2))
                    {
                        anti_2.Add((temp.Item1, temp.Item2));
                        temp.Item1 -= delta.Item1;
                        temp.Item2 -= delta.Item2;
                    }


                    if (IsInBounds(antenna2.Item1 + delta.Item1, antenna2.Item2 + delta.Item2)) 
                        anti_1.Add((antenna2.Item1 + delta.Item1, antenna2.Item2 + delta.Item2));

                    temp = antenna2;
                    while (IsInBounds(temp.Item1, temp.Item2))
                    {
                        anti_2.Add((temp.Item1, temp.Item2));
                        temp.Item1 += delta.Item1;
                        temp.Item2 += delta.Item2;
                    }
                }
            }
        }

        return (anti_1.Count, anti_2.Count);
    }
}