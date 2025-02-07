using AoC._2024;

namespace AoCTesting._2024;

public class Day02Test
{
    private Day02 proj = new();
    private (long res_1, long res_2) res;
    public Day02Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(564, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(604, res.res_2);
    }
}