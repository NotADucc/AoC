using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static AoC.JaggedExtensions;

namespace AoC._2025;

public class Day12 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc12.txt");
        long res_1 = 0, res_2 = 0;

        var lines = File.ReadAllLines(file_name);

        // shapes
        // region size
        var shapes = lines
            .Take(30)
            .Where(x => !x.Contains(':'))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Chunk(3)
            .Select((x, idx) => new Shape(idx, x))
            .ToList();

        var sections = lines
            .Skip(30)
            .Select(x => new Section(x, shapes))
            .ToList();

        // no clue how i would actually place the shapes into the grid
        res_1 = rec(sections, shapes);

        return (res_1, res_2);
    }

    private long rec(List<Section> sections, List<Shape> shapes)
    {
        // dp with memo based on {shape}:{howmany presents}:{dimensionx}:{dimensiony}?
        // also add {shape}:{howmany presents}:{dimensiony}:{dimensionx}?
        // since it doesnt matter how the present is positioned 

        // ah nvm u need to see if it all fits and not just 1 shape at a time
        // or expand shape to {shapes...}

        // key => {dx}:{dy}:{addedshapes}

        Dictionary<string, bool> memo = [];
        Dictionary<(Shape, RotateJaggedClockwiseType), int> visited = shapes
            .Select(x => Enum.GetValues<RotateJaggedClockwiseType>().Select(y => (x, y)))
            .SelectMany(x => x)
            .ToDictionary(x => x, _ => 0);

        bool inner(
            Section section, 
            char[][] curr_grid, 
            int shape_idx,
            RotateJaggedClockwiseType rotation,
            Dictionary<(Shape, RotateJaggedClockwiseType), int> visited
        )
        {
            var visited_keys = visited.Select(x => String.Format("{0}={1}", x.Key, x.Value));
            var key = $"{section.Height}:{section.Width}:{shape_idx}:{rotation}:{string.Join(',', visited_keys)}";

            if (shape_idx >= section.Shapes.Count)
            {
                memo[key] = true;
                return true;
            }

            if (memo.TryGetValue(key, out bool value))
                return value;

            return false;
        }

        return sections
            .Where(x => x.Width * x.Height >= x.Shapes.Sum(x => x.Key.get_filled_in_count() * x.Value))
            .Select(x => inner(x, x.create_grid(), 0, RotateJaggedClockwiseType.None, visited))
            .Sum(x => x ? 1 : 0);
    }

    private class Shape 
    {
        private const char FILLED = '#';
        public int Id { get; set; }
        public char[][] Grid;
        public int Count { get; set; }
        public Shape(int id, IEnumerable<string> lines) 
        {
            Id = id;
            Grid = lines
                .Select(x => x.ToCharArray())
                .ToArray();
            Count = Grid.SelectMany(x => x)
                .Sum(x => x.Equals(FILLED) ? 1 : 0);
        }
        public int get_filled_in_count() 
        {
            return Count;
        }
        public char[][] give_rotated_shape(RotateJaggedClockwiseType rotate)
        {
            return Grid.Rotate(rotate);
        }

        public override bool Equals(object? obj)
        {
            return obj is Shape shape &&
                   Id == shape.Id;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string? ToString()
        {
            return Id.ToString();
        }
    }

    private class Section
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public Dictionary<Shape, int> Shapes;
        public Section(string line, IReadOnlyList<Shape> shapes)
        {
            var split = line.Split(':');
            var dim = split[0]
                .Split('x', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();

            Height = dim[0]; 
            Width = dim[1];

            Shapes = split[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select((x, idx) => (shapes[idx], int.Parse(x)))
                .ToDictionary();
        }
        public char[][] create_grid()
        {
            char[][] grid = new char[Height][];
            for (int i = 0; i < Height; i++)
                grid[i] = new char[Width];

            return grid;
        }
    }
}