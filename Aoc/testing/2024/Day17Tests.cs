using AoC._2024;

namespace AoCTesting._2024;

public class Day17Test
{
    private Day17 proj = new();
    private (string res_1, long res_2) res;
    public Day17Test()
    {
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal("2,1,4,7,6,0,3,1,4", res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(266932601404433, res.res_2);
    }
}