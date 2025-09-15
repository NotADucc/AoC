using AoC._2015;

namespace AoCTesting._2015;

public class Day09Test
{
    private Day09 proj = new();
    private (long res_1, long res_2) res;
    public Day09Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(141, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(736, res.res_2);
    }
}