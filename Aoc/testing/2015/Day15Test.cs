using AoC._2015;

namespace AoCTesting._2015;

public class Day15Test
{
    private Day15 proj = new();
    private (long res_1, long res_2) res;
    public Day15Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(21367368, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1766400, res.res_2);
    }
}