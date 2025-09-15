using AoC._2015;

namespace AoCTesting._2015;

public class Day05Test
{
    private Day05 proj = new();
    private (long res_1, long res_2) res;
    public Day05Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(236, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(51, res.res_2);
    }
}