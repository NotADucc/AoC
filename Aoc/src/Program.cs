using AoC;

public class Program
{
    private static void Main(string[] args)
    {
		try
		{
            var (day, result1, result2) = Helper.RunYear(2016);
            Console.WriteLine(day);
            Console.WriteLine($"Res 1 : {result1}");
            Console.WriteLine($"Res 2 : {result2}");
            Console.WriteLine();
        }
		catch (Exception ex)
		{
            Console.WriteLine(ex);
		}
    }
}
