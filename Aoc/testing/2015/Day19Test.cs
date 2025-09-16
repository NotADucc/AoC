using AoC._2015;

namespace AoCTesting._2015;

public class Day19Test
{
    private Day19 proj = new();
    private (long res_1, long res_2) res;
    public Day19Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(535, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(212, res.res_2);
    }
}