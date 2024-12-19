using AoC.days;

namespace AoCTesting;

public class Day18Test
{
    private Day18 proj = new();
    private (long res_1, (long, long) res_2) res;
    public Day18Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(232, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal((44, 64), res.res_2);
    }
}