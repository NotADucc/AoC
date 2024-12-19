using AoC.days;

namespace AoCTesting;

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
        Assert.Equal(342, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(891192814474630, res.res_2);
    }
}