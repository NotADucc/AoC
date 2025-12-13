using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC;
public static class JaggedExtensions
{
    // https://stackoverflow.com/questions/4670720/extremely-fast-way-to-clone-the-values-of-a-jagged-array-into-a-second-array
    public static T[][] CopyArrayBuiltIn<T>(this T[][] source)
    {
        var n = source.Length;
        var dest = new T[n][];

        for (var x = 0; x < n; x++)
        {
            var inner = source[x];
            var ilen = inner.Length;
            var newer = new T[ilen];
            Array.Copy(inner, newer, ilen);
            dest[x] = newer;
        }

        return dest;
    }
    // https://www.codeproject.com/articles/Transposing-the-rows-and-columns-of-array-CSharp#comments-section
    public static T[][] Transpose<T>(this T[][] arr)
    {
        int rowCount = arr.Length;
        int columnCount = arr[0].Length;
        T[][] transposed = new T[columnCount][];
        if (rowCount == columnCount)
        {
            transposed = (T[][])arr.Clone();
            for (int i = 1; i < rowCount; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    T temp = transposed[i][j];
                    transposed[i][j] = transposed[j][i];
                    transposed[j][i] = temp;
                }
            }
        }
        else
        {
            for (int column = 0; column < columnCount; column++)
            {
                transposed[column] = new T[rowCount];
                for (int row = 0; row < rowCount; row++)
                {
                    transposed[column][row] = arr[row][column];
                }
            }
        }
        return transposed;
    }
    public static T[][] ReverseRows<T>(this T[][] source)
    {
        var res = source.CopyArrayBuiltIn();
        var half = res.Length >> 1;

        for (int i = 0; i < half; i++)
        {
            (res[^(i + 1)], res[i]) = (res[i], res[^(i + 1)]);
        }

        return res;
    }
    public static T[][] ReverseColumns<T>(this T[][] source)
    {
        var res = source.CopyArrayBuiltIn();
        var n = res.Length;

        if (n == 0) return res;
        var m_half = res[0].Length >> 1;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m_half; j++)
            {
                (res[i][^(j + 1)], res[i][j]) = (res[i][j], res[i][^(j + 1)]);
            }
        }

        return res;
    }
    public enum RotateJaggedClockwiseType 
    {
        None,
        Once,
        Twice,
        Thrice,
    }
    public static RotateJaggedClockwiseType NextClockwiseRotation(RotateJaggedClockwiseType curr) =>
        curr switch
        {
            RotateJaggedClockwiseType.None => RotateJaggedClockwiseType.Once,
            RotateJaggedClockwiseType.Once => RotateJaggedClockwiseType.Twice,
            RotateJaggedClockwiseType.Twice => RotateJaggedClockwiseType.Thrice,
            RotateJaggedClockwiseType.Thrice => RotateJaggedClockwiseType.None,
        };
    public static T[][] Rotate<T>(this T[][] source, RotateJaggedClockwiseType rotate)
    {
        var copy = source.CopyArrayBuiltIn();

        if (rotate is RotateJaggedClockwiseType.None) {}
        else if (rotate is RotateJaggedClockwiseType.Once)
        {
            copy = copy.Transpose();
            copy = copy.ReverseColumns();
        }
        else if (rotate is RotateJaggedClockwiseType.Twice)
        {
            copy = copy.ReverseRows();
            copy = copy.ReverseColumns();
            return copy;
        }
        else if (rotate is RotateJaggedClockwiseType.Thrice)
        {
            copy = copy.Transpose();
            copy = copy.ReverseRows();
        }

        return copy;
    }
}
