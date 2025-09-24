using System.Collections;
using System.Linq;

namespace AoC;
public static class ToStringExtensions
{
    public static void Print<T>(this T input, string delimiter = ", ")
    {
        if (input is IEnumerable enumerable && input is not string)
        {
            if (input is null || !enumerable.Cast<object>().Any())
            {
                return;
            }

            Console.WriteLine(string.Join(delimiter, enumerable.Cast<object>()));
        }
        else 
        { 
            Console.WriteLine(input);
        }
    }

    public static void Print<T>(this T[] input, string delimiter = ", ")
    {
        if (input is null || input.Length == 0)
        {
            return;
        }

        Console.WriteLine(string.Join(delimiter, input));
    }

    public static void Print<T>(this T[][] input, string delimiter = ", ")
    {
        if (input is null || input.Length == 0)
        {
            return;
        }

        for (int i = 0; i < input.Length; i++)
        {
            input[i].Print(delimiter);
        }
    }

    public static void Print<T>(this T[,] input, string delimiter = ", ")
    {
        if (input is null || input.Length == 0)
        {
            return;
        }

        int rows = input.GetLength(0);
        int cols = input.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            T[] row = new T[cols];
            for (int j = 0; j < cols; j++)
            {
                row[j] = input[i, j];
            }
            row.Print(delimiter);
        }
    }

    public static void PrintNested<T>(this IEnumerable<IEnumerable<T>> input, string delimiter = ", ")
    {
        if (input is null || !input.Any())
        {
            return;
        }

        foreach (var item in input) 
        { 
            item.Print(delimiter);
        }
        Console.WriteLine();
    }
}
