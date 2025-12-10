using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AoC._2025;

public class Day09 : IRun<long, long>
{
    private const char RED = 'R';
    private const char GREEN = 'G';
    private const char EMPTY = '.';
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc9.txt");
        long res_1 = 0, res_2 = 0;
        
        // width, height
        var lines = File.ReadAllLines(file_name);
        var coords = transform(lines)
            .ToList();

        var n = coords.Count;

        var grid_min_w = coords.Min(x => x.W);
        var grid_min_h = coords.Min(x => x.H);

        foreach (var c in coords)
        {
            c.H -= grid_min_h;
            c.W -= grid_min_w;
        }

        PriorityQueue<Rect, long> heap = new();

        for (int i = 0; i < n; i++)
        {
            var coord1 = coords[i];
            for (int j = i + 1; j < n; j++)
            {
                var coord2 = coords[j];
                var rec = new Rect(coord1, coord2);
                var area = rec.area();

                heap.Enqueue(rec, -area);
                res_1 = Math.Max(res_1, area);
            }
        }

        //var grid_max_w = coords.Max(x => x.W) + 1;
        //var grid_max_h = coords.Max(x => x.H) + 1;

        //char[][] grid = new char[grid_max_h][];
        //for (int i = 0; i < grid_max_h; i++)
        //{
        //    grid[i] = new char[grid_max_w + 1];
        //    for (int j = 0; j < grid_max_w; j++)
        //    {
        //        grid[i][j] = EMPTY;
        //    }
        //}


        //foreach (var coord in coords)
        //{
        //    grid[coord.H][coord.W] = RED;
        //}

        //List<(int h, int w)> idxs = new();

        //for (int i = grid.Length - 1; i >= 0; i--)
        //{
        //    var line = grid[i];
        //    int l = Array.IndexOf(line, RED), r = Array.LastIndexOf(line, RED);
        //    if (l != -1 && l != r)
        //    {
        //        for (int idx = l + 1; idx < r; idx++)
        //        {
        //            if (line[idx] == RED) continue;
        //            line[idx] = GREEN;
        //        }
        //        var lst = paint_below(grid, i + 1, l, grid.Length);
        //        idxs.AddRange(lst);
        //        paint_below(grid, i + 1, r, grid.Length);
        //    }
        //}

        //Console.WriteLine("start fill");
        //foreach (var kvp in idxs)
        //{
        //    fill(grid, kvp.h, kvp.w);
        //}
        //Console.WriteLine("finished fill");
        //// eh oh
        ////grid.Print();

        //while (heap.Count > 0)
        //{
        //    var rec = heap.Dequeue();

        //    bool is_in_assumption = grid[rec.Min.H][rec.Min.W] != EMPTY
        //        && grid[rec.Max.H][rec.Min.W] != EMPTY
        //        && grid[rec.Max.H][rec.Max.W] != EMPTY
        //        && grid[rec.Min.H][rec.Max.W] != EMPTY;

        //    if (is_in_assumption)
        //    {
        //        if (!is_contained(grid, rec.Min.H, rec.Min.W, rec.Max.H, rec.Max.W))
        //            continue;

        //        res_2 = rec.area();
        //        break;
        //    }
        //}

        // 2979980610
        // 129351348 not right p2
        // 16291440 not right

        return (res_1, res_2);
    }

    private bool can_paint_below(char[][] grid, int h, int w, int max_h)
    {
        for (int i = h; i < max_h; i++)
        {
            if (grid[i][w] == RED)
                return true;
        }

        return false;
    }

    private List<(int, int)> paint_below(char[][] grid, int h, int w, int max_h) 
    {
        if (!can_paint_below(grid, h, w, max_h))
            return [];

        List<(int, int)> lst = new();
        for (int i = h; i < max_h; i++)
        {
            if (grid[i][w] != EMPTY)
                break;

            lst.Add((i, w));
            
            grid[i][w] = GREEN;
        }

        return lst;
    }

    private void fill(char[][] grid, int h, int w)
    {
        Queue<(int h, int w)> q = [];
        q.Enqueue((h, w));

        grid[h][w] = GREEN;

        while (q.Count > 0)
        {
            var deq = q.Dequeue();

            int lh = Math.Max(deq.h - 1, h);
            int mh = Math.Min(deq.h + 2, grid.Length);

            int lw = Math.Max(deq.w - 1, w);
            int mw = Math.Min(deq.w + 2, grid[0].Length);

            for (int i = lh; i < mh; i++)
            {
                for (int j = lw; j < mw; j++)
                {
                    if(grid[i][j] == EMPTY)
                    {
                        grid[i][j] = GREEN;
                        q.Enqueue((i, j));
                    }
                }
            }
        }
    }

    private bool is_contained(char[][] grid, int min_h, int min_w, int max_h, int max_w)
    {
        for (int i = min_h; i <= max_h; i++)
        {
            for (int j = min_w; j <= max_w; j++)
            {
                if (grid[i][j] == EMPTY)
                    return false;
            }
        }
        return true;
    }
    private IEnumerable<Coord> transform(string[] lines)
    {
        foreach (var line in lines)
        {
            var split = line.Split(',');
            yield return new Coord(int.Parse(split[0]), int.Parse(split[1]));
        }
    }
    private class Rect
    {
        public Rect(Coord coord1, Coord coord2)
        {
            (var min_w, var max_w) = coord1.W < coord2.W
                ? (coord1.W, coord2.W)
                : (coord2.W, coord1.W);

            (var min_h, var max_h) = coord1.H < coord2.H
                ? (coord1.H, coord2.H)
                : (coord2.H, coord1.H);

            Min = new Coord(min_w, min_h);
            Max = new Coord(max_w, max_h);
        }

        public Coord Min { get; set; }
        public Coord Max { get; set; }

        public long area()
        {
            var w = Max.W - Min.W + 1;
            var h = Max.H - Min.H + 1;
            return (long)w * (long)h;
        }
    }
    private class Coord(int W, int H)
    {
        public int H { get; set; } = H;
        public int W { get; set; } = W;
    }
}