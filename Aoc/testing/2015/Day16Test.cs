using AoC._2015;

namespace AoCTesting._2015;

public class Day16Test
{
    private Day16 proj = new();
    private (long res_1, long res_2) res;
    public Day16Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(40, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(241, res.res_2);
    }
}