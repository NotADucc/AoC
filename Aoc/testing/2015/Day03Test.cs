using AoC._2015;

namespace AoCTesting._2015;

public class Day03Test
{
    private Day03 proj = new();
    private (long res_1, long res_2) res;
    public Day03Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(2565, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(2639, res.res_2);
    }
}