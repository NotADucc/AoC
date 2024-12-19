using AoC.days;

namespace AoCTesting;

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
        Assert.Equal(171183089, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(63866497, res.res_2);
    }
}