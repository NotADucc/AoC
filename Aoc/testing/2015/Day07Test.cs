using AoC._2024;

namespace AoCTesting._2015;

public class Day07Test
{
    private Day01 proj = new();
    private (long res_1, long res_2) res;
    public Day07Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(46065, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(14134, res.res_2);
    }
}