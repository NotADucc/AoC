using AoC._2024;

namespace AoCTesting._2024;

public class Day13Test
{
    private Day13 proj = new();
    private (long res_1, long res_2) res;
    public Day13Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal(29522, res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(101214869433312, res.res_2);
    }
}