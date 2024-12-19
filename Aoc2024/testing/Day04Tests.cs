using AoC.days;

namespace AoCTesting;

public class Day04Test
{
    private Day04 proj = new();
    private (long res_1, long res_2) res;
    public Day04Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(2414, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1871, res.res_2);
    }
}