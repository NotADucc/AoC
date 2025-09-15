using AoC._2015;

namespace AoCTesting._2015;

public class Day11Test
{
    private Day11 proj = new();
    private (string res_1, string res_2) res;
    public Day11Test() 
    { 
        res = proj.Run();
    }

    [Fact]
    public void Part1()
    {
        Assert.Equal("hxbxxyzz", res.res_1);
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal("hxcaabcc", res.res_2);
    }
}