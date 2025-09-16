using AoC._2015;

namespace AoCTesting._2015;

public class Day18Test
{
    private Day18 proj = new();
    private (long res_1, long res_2) res;
    public Day18Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(814, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(924, res.res_2);
    }
}