using System.Windows;

namespace MySphere.Framework.Effects;

public static class Homography
{
    public static Matrix3x3 CreateInverse(
        Point topLeft,
        Point topRight,
        Point bottomRight,
        Point bottomLeft)
    {
        var target = new[]
        {
            topLeft,
            topRight,
            bottomRight,
            bottomLeft
        };

        var source = new[]
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(1, 1),
            new Point(0, 1)
        };

        /*
         * We need a homography:
         *
         * x' = (a*x + b*y + c) / (g*x + h*y + 1)
         * y' = (d*x + e*y + f) / (g*x + h*y + 1)
         */

        var matrix = new double[8, 9];

        for (var i = 0; i < 4; i++)
        {
            var x = target[i].X;
            var y = target[i].Y;

            var u = source[i].X;
            var v = source[i].Y;

            var row = i * 2;

            matrix[row, 0] = x;
            matrix[row, 1] = y;
            matrix[row, 2] = 1;
            matrix[row, 3] = 0;
            matrix[row, 4] = 0;
            matrix[row, 5] = 0;
            matrix[row, 6] = -u * x;
            matrix[row, 7] = -u * y;
            matrix[row, 8] = u;

            matrix[row + 1, 0] = 0;
            matrix[row + 1, 1] = 0;
            matrix[row + 1, 2] = 0;
            matrix[row + 1, 3] = x;
            matrix[row + 1, 4] = y;
            matrix[row + 1, 5] = 1;
            matrix[row + 1, 6] = -v * x;
            matrix[row + 1, 7] = -v * y;
            matrix[row + 1, 8] = v;
        }

        var result = Solve(matrix);

        return new Matrix3x3(
            result[0],
            result[1],
            result[2],
            result[3],
            result[4],
            result[5],
            result[6],
            result[7],
            1);
    }

    private static double[] Solve(double[,] matrix)
    {
        const int size = 8;

        for (var column = 0; column < size; column++)
        {
            var pivot = column;

            for (var row = column + 1; row < size; row++)
            {
                if (Math.Abs(matrix[row, column]) >
                    Math.Abs(matrix[pivot, column]))
                {
                    pivot = row;
                }
            }

            if (Math.Abs(matrix[pivot, column]) < 1e-10)
                throw new InvalidOperationException(
                    "The perspective quadrilateral is degenerate.");

            if (pivot != column)
            {
                for (var j = column; j <= size; j++)
                {
                    (matrix[column, j], matrix[pivot, j]) =
                        (matrix[pivot, j], matrix[column, j]);
                }
            }

            var divisor = matrix[column, column];

            for (var j = column; j <= size; j++)
                matrix[column, j] /= divisor;

            for (var row = 0; row < size; row++)
            {
                if (row == column)
                    continue;

                var factor = matrix[row, column];

                for (var j = column; j <= size; j++)
                    matrix[row, j] -=
                        factor * matrix[column, j];
            }
        }

        var result = new double[size];

        for (var i = 0; i < size; i++)
            result[i] = matrix[i, size];

        return result;
    }
}