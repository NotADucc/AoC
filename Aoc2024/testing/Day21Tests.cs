using AoC.days;

namespace AoCTesting;

public class Day21Test
{
    private Day21 proj = new();
    private (long res_1, long res_2) res;
    public Day21Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(222670, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(271397390297138, res.res_2);
    }
}