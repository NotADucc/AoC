using AoC.days;

namespace AoCTesting;

public class Day06Test
{
    private Day06 proj = new();
    private (long res_1, long res_2) res;
    public Day06Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(5564, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1976, res.res_2);
    }
}