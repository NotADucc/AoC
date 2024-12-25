using AoC.days;

namespace AoCTesting;

public class Day25Test
{
    private Day25 proj = new();
    private (long res_1, long res_2) res;
    public Day25Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(3395, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(0, res.res_2);
    }
}