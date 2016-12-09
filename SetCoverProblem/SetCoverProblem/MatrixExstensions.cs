using System;
using System.Collections.Generic;

namespace SetCoverProblem
{
	public static class MatrixExstensions
	{
		public static int[,] RemoveColumns(this int[,] source, List<int> columnsIndexes)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (columnsIndexes == null) throw new ArgumentNullException(nameof(columnsIndexes));

			var matrix = new int[source.GetLength(0) - columnsIndexes.Count, source.GetLength(1)];
			int columnsShift = 0;
			for (int x = 0; x < source.GetLength(0); x++)
			{
				if (columnsIndexes.Contains(x))
				{
					--columnsShift;
				}
				else
				{
					for (int y = 0; y < source.GetLength(1); y++)
						matrix[x + columnsShift, y] = source[x, y];
				}
			}
			return matrix;
		}

		public static int[,] RemoveRows(this int[,] source, List<int> rowsIndexes)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (rowsIndexes == null) throw new ArgumentNullException(nameof(rowsIndexes));

			var matrix = new int[source.GetLength(0), source.GetLength(1) - rowsIndexes.Count];
			int rowsShift = 0;
			for (int y = 0; y < source.GetLength(1); y++)
			{
				if (rowsIndexes.Contains(y))
				{
					--rowsShift;
				}
				else
				{
					for (int x = 0; x < source.GetLength(0); x++)
						matrix[x, y + rowsShift] = source[x, y];
				}
			}
			return matrix;
		}

		public static int[,] Clone(this int[,] source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));

			var matrix = new int[source.GetLength(0), source.GetLength(1)];
			for (int x = 0; x < source.GetLength(0); x++)
				for (int y = 0; y < source.GetLength(1); y++)
					matrix[x, y] = source[x, y];
			return matrix;
		}

		public static int FindInColumn(this int[,] source, int x, int value, int[] isInvalidRow)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isInvalidRow == null) throw new ArgumentNullException(nameof(isInvalidRow));

			for (int y = 0; y < source.GetLength(1); y++)
				if (isInvalidRow[y] == 0 && source[x, y] == value)
					return y;
			return -1;
		}

		public static int FindInRow(this int[,] source, int y, int value, int[] isInvalidColumn)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isInvalidColumn == null) throw new ArgumentNullException(nameof(isInvalidColumn));

			for (int x = 0; x < source.GetLength(0); x++)
				if (isInvalidColumn[x] == 0 && source[x, y] == value)
					return x;
			return -1;
		}

		public static int SumRow(this int[,] source, int y, int[] isInvalidColumn)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isInvalidColumn == null) throw new ArgumentNullException(nameof(isInvalidColumn));

			int sum = 0;
			for (int x = 0; x < source.GetLength(0); x++)
				if (isInvalidColumn[x] == 0)
					sum += source[x, y];
			return sum;
		}

		public static int SumColumn(this int[,] source, int x, int[] isInvalidRow)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isInvalidRow == null) throw new ArgumentNullException(nameof(isInvalidRow));

			int sum = 0;
			for (int y = 0; y < source.GetLength(1); y++)
				if (isInvalidRow[y] == 0)
					sum += source[x, y];
			return sum;
		}

		public static bool IsColumnOneSupersetOfColumnTwo(this int[,] source, int xOne, int xTwo, int[] isInvalidRow)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isInvalidRow == null) throw new ArgumentNullException(nameof(isInvalidRow));

			for (int y = 0; y < source.GetLength(1); y++)
				if (isInvalidRow[y] == 0 && source[xOne, y] == 0 && source[xTwo, y] == 1)
					return false;
			return true;
		}

		public static bool IsRowOneSupersetOfRowTwo(this int[,] source, int yOne, int yTwo, int[] isInvalidColumn)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isInvalidColumn == null) throw new ArgumentNullException(nameof(isInvalidColumn));

			for (int x = 0; x < source.GetLength(0); x++)
				if (isInvalidColumn[x] == 0 && source[x, yOne] == 0 && source[x, yTwo] == 1)
					return false;
			return true;
		}

		public static int[,] Transpose(this int[,] source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));

			var matrix = new int[source.GetLength(1), source.GetLength(0)];
			for (int x = 0; x < source.GetLength(0); ++x)
				for (int y = 0; y < source.GetLength(1); y++)
					matrix[y, x] = source[x, y];
			return matrix;
		}

		public static int GetMaxSumColumn(this int[,] source, int[] isInvalidColumn, int[] isInvalidRow)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isInvalidColumn == null) throw new ArgumentNullException(nameof(isInvalidColumn));
			if (isInvalidRow == null) throw new ArgumentNullException(nameof(isInvalidRow));

			int max = int.MinValue;
			int maxIndex = -1;
			for (int x = 0; x < isInvalidColumn.Length; x++)
				if (isInvalidColumn[x] == 0)
				{
					int sum = source.SumColumn(x, isInvalidRow);
					if (sum > max)
					{
						max = sum;
						maxIndex = x;
					}
				}
			return maxIndex;
		}
	}
}
