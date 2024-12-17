using AoC;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
public class Day14 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc14.txt");
        string output_path = Path.Combine(Helper.GetBaseDir(), "IO", "AOC14");
        long res_1 = 1, res_2 = 6355;
        var grid = new Grid(101, 103);
        foreach (var line in File.ReadAllLines(file_name))
        {
            var coords = line.Split(' ')
                .Select(split => split.Split(',').Select(x => x.Replace("p=", "").Replace("v=", "")))
                .SelectMany(x => x)
                .Select(int.Parse)
                .ToArray();
            grid.AddRobot(coords);
        }

        for (int i = 0; i < 100; i++)
        {
            grid.Update();
            grid.Draw(output_path, i + 1);
        }

        foreach (var score in grid.GetQuadrantScores())
        {
            res_1 *= score;
        }

        for (int i = 100; i < 10_000; i++)
        {
            grid.Update();
            grid.Draw(output_path, i + 1);
        }

        return (res_1, res_2);
    }
    class Grid
    {
        private int Width;
        private int Length;
        private List<Robot> _robots = new List<Robot>();
        public Grid(int width, int length)
        {
            Width = width;
            Length = length;
        }
        public void AddRobot(int[] coords) 
            => AddRobot(coords[0], coords[1], coords[2], coords[3]);
        public void AddRobot(int start_x, int start_y, int velo_x, int velo_y)
        {
            var loc = new Point(start_x, start_y);
            var velo = new Point(velo_x, velo_y);
            _robots.Add(new Robot(loc, velo));
        }
        public void Update()
        {
            foreach (var robot in _robots)
            {
                robot.Location.X = (robot.Location.X + robot.Velocity.X) % Width;
                robot.Location.Y = (robot.Location.Y + robot.Velocity.Y) % Length;
                if (robot.Location.X < 0) robot.Location.X += Width;
                if (robot.Location.Y < 0) robot.Location.Y += Length;
            }
        }
        public IEnumerable<int> GetQuadrantScores()
        {
            var freq = _robots
                .GroupBy(x => (x.Location.X, x.Location.Y))
                .ToDictionary(x => x.Key, x => x.Count());

            int half_width = Width >> 1;
            int half_len = Length >> 1;
            yield return GetQuadrantScore(freq, 0, half_width, 0, half_len);
            yield return GetQuadrantScore(freq, half_width + 1, Width, 0, half_len);
            yield return GetQuadrantScore(freq, 0, half_width, half_len + 1, Length);
            yield return GetQuadrantScore(freq, half_width + 1, Width, half_len + 1, Length);
        }
        private int GetQuadrantScore(Dictionary<(int, int), int> freq, int x_start, int x_end, int y_start, int y_end)
        {
            int count = 0;
            for (int x = x_start; x < x_end; x++)
            {
                for (int y = y_start; y < y_end; y++)
                {
                    if (freq.TryGetValue((x, y), out var cnt))
                        count += cnt;
                }
            }
            return count;
        }
        public void DisplayGrid()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int cnt = _robots
                        .Where(x => x.Location.X == j && x.Location.Y == i)
                        .Count();
                    char ch = cnt == 0 ? '.' : cnt.DigitToChar();
                    Console.Write(ch);
                }
                Console.WriteLine();
            }
        }
        public override string? ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var robot in _robots)
            {
                sb.Append(robot);
                sb.Append("\n");
            }
            return sb.ToString();
        }
        public void Draw(string base_path, int second)
        {
            const int drawingFactor = 1;
            using Bitmap bm = new Bitmap(Width * drawingFactor, Length * drawingFactor);
            using Graphics g = Graphics.FromImage(bm);
            using Pen p = new Pen(Color.White, 1);
            foreach (var robot in _robots)
            {
                g.DrawEllipse(p, robot.Location.X * drawingFactor, robot.Location.Y * drawingFactor, drawingFactor, drawingFactor);
            }
            bm.Save(Path.Combine(base_path, second + "_aoc14.png"), ImageFormat.Png);
        }
    }
    class Robot
    {
        public Point Location;
        public Point Velocity;
        public Robot(Point location, Point velocity)
        {
            Location = location;
            Velocity = velocity;
        }
        public override string? ToString()
        {
            return $"Location: {Location} | Velocity:{Velocity}";
        }
    }
    class Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string? ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
    }
}