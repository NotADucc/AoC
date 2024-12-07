namespace AoCTesting
{
    public class Day7Test
    {
        private Day7 proj = new();
        private (long res_1, long res_2) res;
        public Day7Test()
        {
            res = proj.Run();
        }

        [Fact]
        public void Part1()
        {
            Assert.Equal(1153997401072, res.res_1);
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(97902809384118, res.res_2);
        }
    }
}