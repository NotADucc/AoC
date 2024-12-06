namespace AoCTesting
{
    public class Day2Test
    {
        private Day2 proj = new();
        private (long res_1, long res_2) res;
        public Day2Test()
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
}