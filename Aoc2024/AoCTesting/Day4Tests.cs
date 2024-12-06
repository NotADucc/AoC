namespace AoCTesting
{
    public class Day4Test
    {
        private Day4 proj = new();
        private (long res_1, long res_2) res;
        public Day4Test()
        {
            res = proj.Run();
        }

        [Fact]
        public void Part1()
        {
            Assert.Equal(2414, res.res_1);
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(1871, res.res_2);
        }
    }
}