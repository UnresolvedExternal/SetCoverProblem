using System.Linq;
using NUnit.Framework;
using SetCoverProblem;

namespace SetCoverTests
{
	[TestFixture]
	public class MatrixExtensionsTests
	{
		[Test]
		public void RemoveColumnsTest1()
		{
			int[,] source =
			{
				{1, 2, 3, 4, 5},
				{5, 6, 7, 8, 9},
				{10, 11, 12, 13, 14},
				{15, 16, 17, 18, 19}
			};
			source = source.Transpose();

			var columnsIndexes = new[] { 0, 2, 4 }.ToList();

			int[,] expected =
			{
				{2,4 },
				{6,8 },
				{11, 13 },
				{16, 18 }
			};
			expected = expected.Transpose();

			int[,] actual = source.RemoveColumns(columnsIndexes);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void RemoveColumnsTest2()
		{
			int[,] source =
			{
				{1, 2, 3, 4, 5},
				{5, 6, 7, 8, 9},
				{10, 11, 12, 13, 14},
				{15, 16, 17, 18, 19}
			};
			source = source.Transpose();

			var columnsIndexes = new[] { 0, 1, 2, 3, 4 }.ToList();

			int[,] expected = new int[0, 4].Transpose();

			int[,] actual = source.RemoveColumns(columnsIndexes).Transpose();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void RemoveRowsTest1()
		{
			int[,] source =
			{
				{1, 2, 3, 4, 5},
				{5, 6, 7, 8, 9},
				{10, 11, 12, 13, 14},
				{15, 16, 17, 18, 19}
			};
			source = source.Transpose();

			var rowsIndexes = new[] { 1, 2 }.ToList();

			int[,] expected =
			{
				{1, 2, 3, 4, 5},
				{15, 16, 17, 18, 19}
			};
			expected = expected.Transpose();

			int[,] actual = source.RemoveRows(rowsIndexes);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void RemoveRowsTest2()
		{
			int[,] source =
			{
				{1, 2, 3, 4, 5},
				{5, 6, 7, 8, 9},
				{10, 11, 12, 13, 14},
				{15, 16, 17, 18, 19}
			};
			source = source.Transpose();

			var rowsIndexes = new[] { 0, 1, 2, 3 }.ToList();

			int[,] expected = new int[0, 5];
			expected = expected.Transpose();

			int[,] actual = source.RemoveRows(rowsIndexes);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void IsColumnOneSupersetOfColumnTwoTest()
		{
			int[,] source =
			{
				{1, 0, 1, 1, 0},
				{1, 0, 0, 0, 0},
				{0, 0, 0, 1, 1},
				{1, 1, 1, 0, 0}
			};
			source = source.Transpose();

			int[] isInvalidRow = new int[source.GetLength(1)];
			Assert.That(source.IsColumnOneSupersetOfColumnTwo(1, 2, isInvalidRow), Is.False);

			isInvalidRow[0] = 10;
			Assert.That(source.IsColumnOneSupersetOfColumnTwo(1, 2, isInvalidRow), Is.True);

			for (int x = 0; x < source.GetLength(0); x++)
				Assert.That(source.IsColumnOneSupersetOfColumnTwo(x, x, isInvalidRow));
		}

		[Test]
		public void IsRowOneSupersetOfRowTwoTest()
		{
			int[,] source =
			{
				{1, 0, 1, 1, 0},
				{1, 0, 0, 0, 0},
				{0, 0, 0, 1, 1},
				{1, 1, 1, 0, 0}
			};
			source = source.Transpose();

			int[] isInvalidColumn = new int[source.GetLength(0)];
			Assert.That(source.IsRowOneSupersetOfRowTwo(0, 1, isInvalidColumn), Is.True);

			isInvalidColumn[0] = isInvalidColumn[2] = 1;
			Assert.That(source.IsRowOneSupersetOfRowTwo(2, 1, isInvalidColumn), Is.True);

			Assert.That(source.IsRowOneSupersetOfRowTwo(0, 2, isInvalidColumn), Is.False);

			for (int y = 0; y < source.GetLength(1); y++)
				Assert.That(source.IsRowOneSupersetOfRowTwo(y, y, isInvalidColumn), Is.True);
		}

		[Test]
		public void FindAndSumTest()
		{
			int[,] source =
{
				{1, 0, 1, 1, 0},
				{1, 0, 0, 0, 0},
				{0, 0, 0, 1, 1},
				{1, 1, 1, 0, 0}
			};
			source = source.Transpose();

			int[] isInvalidColumn = new int[5];
			isInvalidColumn[2] = isInvalidColumn[3] = 1;

			int[] isInvalidRow = new int[4];
			isInvalidRow[1] = isInvalidRow[2] = 1;

			Assert.That(source.SumColumn(0, isInvalidRow) == 2);
			Assert.That(source.SumRow(2, isInvalidColumn) == 1);
			Assert.That(source.FindInColumn(3, 0, isInvalidRow) == 3);
			Assert.That(source.FindInRow(3, 0, isInvalidColumn) == 4);
		}
	}
}
