namespace AoCTesting
{
    public class Day17Test
    {
        private Day17 proj = new();
        private (long res_1, long res_2) res;
        public Day17Test()
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
}