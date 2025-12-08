namespace AoC._2025;

public class Day08 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc8.txt");
        long res_1 = 0, res_2 = 0;
        
        var lines = File.ReadAllLines(file_name);
        const int COUNT = 1_000;
        var coords = transform(lines)
            .OrderBy(x => x.distance(0, 0, 0))
            .ToList();

        var graph = new Graph(coords.Count);
        HashSet<(JunctionBox coord1, JunctionBox coord2)> visited = [];
        PriorityQueue<(JunctionBox, JunctionBox), double> heap = new();

        for (int i = 0; i < coords.Count; i++)
        {
            var start = coords[i];
            for (int j = 0; j < coords.Count; j++)
            {
                if (i == j) continue;

                var comp = coords[j];

                if (visited.Contains((start, comp))) continue;

                var distance = start.distance(comp);
                heap.Enqueue((start, comp), distance);
                visited.Add((comp, start));
            }
        }

        (JunctionBox coord1, JunctionBox coord2) last = (null!, null!);

        for (int z = 0; z < COUNT; z++)
        {
            if (heap.Count == 0)
                throw new Exception("Heap empty");

            (JunctionBox coord1, JunctionBox coord2) deq = heap.Dequeue();
            var has_merged = graph.add(deq.coord1, deq.coord2);
            if (has_merged && last.coord1 is null)
            {
                last = deq;
            }
        }

        res_1 = graph
            .take_largest(3)
            .Aggregate((x, y) => x * y);

        while (heap.Count > 0)
        {
            (JunctionBox coord1, JunctionBox coord2) deq = heap.Dequeue();
            var has_merged = graph.add(deq.coord1, deq.coord2);
            if (has_merged && last.coord1 is null)
            {
                last = deq;
            }
        }

        res_2 = (long) last.coord1.X * last.coord2.X;

        return (res_1, res_2);
    }
    private IEnumerable<JunctionBox> transform(string[] lines)
    { 
        foreach (var line in lines)
        {
            var split = line.Split(',');
            yield return new(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }
    }
    private class Graph(int count)
    {
        public List<Circuit> Circuits = [];
        public int Count { get; set; } = count;
        public bool add(JunctionBox c1, JunctionBox c2)
        {
            var junc_coord1_exists = false;
            var junc_coord1_idx = -1;

            var junc_coord2_exists = false;
            var junc_coord2_idx = -1;

            for (int i = 0; i < Circuits.Count; i++)
            {
                var junction = Circuits[i];

                if (junction.contains(c1))
                {
                    junc_coord1_exists = true;
                    junc_coord1_idx = i;
                }

                if (junction.contains(c2))
                {
                    junc_coord2_exists = true;
                    junc_coord2_idx = i;
                }
            }

            if (junc_coord1_exists && junc_coord2_exists)
            {
                if (junc_coord1_idx != junc_coord2_idx)
                {
                    Circuits[junc_coord1_idx].add_range(Circuits[junc_coord2_idx].Boxes);
                    Circuits.RemoveAt(junc_coord2_idx);
                }
            }
            else if (junc_coord1_exists)
            {
                Circuits[junc_coord1_idx].add(c2);
            }
            else if (junc_coord2_exists)
            {
                Circuits[junc_coord2_idx].add(c1);
            }
            else
            {
                Circuits.Add(new Circuit([c1, c2]));
            }

            return Circuits.Count == 1 && Count == count();
        }

        public List<int> take_largest(int take)
        {
            return Circuits
                .Select(x => x.Boxes.Count)
                .OrderByDescending(x => x)
                .Take(take)
                .ToList();
        }

        public int count() => Circuits.Sum(x => x.count());
    }

    private class Circuit(List<JunctionBox> boxes)
    { 
        public List<JunctionBox> Boxes = boxes;
        public void add_range(IEnumerable<JunctionBox> c)
        {
            Boxes.AddRange(c);
        }
        public void add(JunctionBox c) 
        {
            if (Boxes.Contains(c))
                return;

            Boxes.Add(c);
        }
        public bool contains(JunctionBox c)
            => Boxes.Contains(c);

        public int count() => Boxes.Count;
    }

    private class JunctionBox(int x, int y, int z) : IEquatable<JunctionBox>
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public int Z { get; set; } = z;
        public double distance(JunctionBox coord)
        {
            var pow1 = Math.Pow(this.X - coord.X, 2);
            var pow2 = Math.Pow(this.Y - coord.Y, 2);
            var pow3 = Math.Pow(this.Z - coord.Z, 2);

            return Math.Sqrt(pow1 + pow2 + pow3);
        }
        public double distance(int x, int y, int z)
        {
            var pow1 = Math.Pow(this.X - x, 2);
            var pow2 = Math.Pow(this.Y - y, 2);
            var pow3 = Math.Pow(this.Z - z, 2);

            return Math.Sqrt(pow1 + pow2 + pow3);
        }
        public bool Equals(JunctionBox? coord)
        {
            return X == coord!.X &&
                   Y == coord!.Y &&
                   Z == coord!.Z;
        }
        public override bool Equals(object? obj)
        {
            return obj is JunctionBox coord && Equals(coord);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string? ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
    }
}