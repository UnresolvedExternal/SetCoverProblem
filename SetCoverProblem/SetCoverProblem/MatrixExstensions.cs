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

		public static int FindInColumn(this int[,] source, int x, int value, int[] isRowInvalid)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isRowInvalid == null) throw new ArgumentNullException(nameof(isRowInvalid));

			for (int y = 0; y < source.GetLength(1); y++)
				if (isRowInvalid[y] == 0 && source[x, y] == value)
					return y;
			return -1;
		}

		public static int FindInRow(this int[,] source, int y, int value, int[] isColumnInvalid)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isColumnInvalid == null) throw new ArgumentNullException(nameof(isColumnInvalid));

			for (int x = 0; x < source.GetLength(0); x++)
				if (isColumnInvalid[x] == 0 && source[x, y] == value)
					return x;
			return -1;
		}

		public static int SumRow(this int[,] source, int y, int[] isColumnInvalid)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isColumnInvalid == null) throw new ArgumentNullException(nameof(isColumnInvalid));

			int sum = 0;
			for (int x = 0; x < source.GetLength(0); x++)
				if (isColumnInvalid[x] == 0)
					sum += source[x, y];
			return sum;
		}

		public static int SumColumn(this int[,] source, int x, int[] isRowInvalid)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isRowInvalid == null) throw new ArgumentNullException(nameof(isRowInvalid));

			int sum = 0;
			for (int y = 0; y < source.GetLength(1); y++)
				if (isRowInvalid[y] == 0)
					sum += source[x, y];
			return sum;
		}

		public static bool IsColumnOneSupersetOfColumnTwo(this int[,] source, int xOne, int xTwo, int[] isRowInvalid)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isRowInvalid == null) throw new ArgumentNullException(nameof(isRowInvalid));

			for (int y = 0; y < source.GetLength(1); y++)
				if (isRowInvalid[y] == 0 && source[xOne, y] == 0 && source[xTwo, y] != 0)
					return false;
			return true;
		}

		public static bool IsRowOneSupersetOfRowTwo(this int[,] source, int yOne, int yTwo, int[] isColumnInvalid)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isColumnInvalid == null) throw new ArgumentNullException(nameof(isColumnInvalid));

			for (int x = 0; x < source.GetLength(0); x++)
				if (isColumnInvalid[x] == 0 && source[x, yOne] == 0 && source[x, yTwo] != 0)
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

		public static int GetMaxSumColumn(this int[,] source, int[] isColumnInvalid, int[] isRowInvalid)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isColumnInvalid == null) throw new ArgumentNullException(nameof(isColumnInvalid));
			if (isRowInvalid == null) throw new ArgumentNullException(nameof(isRowInvalid));

			int max = int.MinValue;
			int maxIndex = -1;
			for (int x = 0; x < isColumnInvalid.Length; x++)
				if (isColumnInvalid[x] == 0)
				{
					int sum = source.SumColumn(x, isRowInvalid);
					if (sum > max)
					{
						max = sum;
						maxIndex = x;
					}
				}
			return maxIndex;
		}

		public static int GetBestColumn(this int[,] source, int[] isColumnInvalid, int[] isRowInvalid,
			double[] costs)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (isColumnInvalid == null) throw new ArgumentNullException(nameof(isColumnInvalid));
			if (isRowInvalid == null) throw new ArgumentNullException(nameof(isRowInvalid));
			if (costs == null) throw new ArgumentNullException(nameof(costs));

			double max = double.MinValue;
			int maxIndex = -1;
			for (int x = 0; x < isColumnInvalid.Length; x++)
				if (isColumnInvalid[x] == 0)
				{
					int sum = source.SumColumn(x, isColumnInvalid);
					double value;
					if (sum == 0)
						value = double.MinValue;
					else if (Math.Abs(costs[x]) < App.Default.Tolerance)
						value = App.Default.Infinite * sum;
					else
						value = sum / costs[x];
					if (value > max)
					{
						max = value;
						maxIndex = x;
					}
				}
			return maxIndex;
		}
	}
}
