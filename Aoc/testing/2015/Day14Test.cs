using AoC._2015;

namespace AoCTesting._2015;

public class Day14Test
{
    private Day14 proj = new();
    private (long res_1, long res_2) res;
    public Day14Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(2640, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1102, res.res_2);
    }
}