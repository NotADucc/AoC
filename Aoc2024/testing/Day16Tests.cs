using AoC.days;

namespace AoCTesting;

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
        Assert.Equal(91464, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(494, res.res_2);
    }
}