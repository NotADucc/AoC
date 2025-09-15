using AoC._2015;

namespace AoCTesting._2015;

public class Day12Test
{
    private Day12 proj = new();
    private (long res_1, long res_2) res;
    public Day12Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(191164, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(87842, res.res_2);
    }
}