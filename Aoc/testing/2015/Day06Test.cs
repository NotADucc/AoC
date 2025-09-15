using AoC._2015;

namespace AoCTesting._2015;

public class Day06Test
{
    private Day06 proj = new();
    private (long res_1, long res_2) res;
    public Day06Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(400410, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(15343601, res.res_2);
    }
}