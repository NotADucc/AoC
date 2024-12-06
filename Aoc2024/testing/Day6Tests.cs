namespace AoCTesting
{
    public class Day6Test
    {
        private Day6 proj = new();
        private (long res_1, long res_2) res;
        public Day6Test()
        {
            res = proj.Run();
        }

        [Fact]
        public void Part1()
        {
            Assert.Equal(5564, res.res_1);
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(1976, res.res_2);
        }
    }
}