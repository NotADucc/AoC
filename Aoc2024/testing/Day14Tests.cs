using AoC.days;

namespace AoCTesting;

public class Day14Test
{
    private Day14 proj = new();
    private (long res_1, long res_2) res;
    public Day14Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(220971520, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(6355, res.res_2);
    }
}