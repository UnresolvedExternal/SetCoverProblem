using NUnit.Framework;
using SetCoverProblem;

namespace SetCoverTests
{
	[TestFixture]
	class SimplificatorTests
	{
		private static void AssertEqual(SimplificationInfo actual, SimplificationInfo expected)
		{
			Assert.That(actual.ColumnsExcluded, Is.EqualTo(expected.ColumnsExcluded));
			Assert.That(actual.RowsExcluded, Is.EqualTo(expected.RowsExcluded));
			Assert.That(actual.ColumnsInSolution, Is.EqualTo(expected.ColumnsInSolution));
		}

		[Test]
		public void _1_1x1_Test()
		{
			int[,] source =
			{
				{1}
			};
			source = source.Transpose();
			var expected = new SimplificationInfo(new[] { 0 }, new[] { 0 }, new[] { 0 });

			var actual = new Simplificator(source).GetSimplificationInfo();

			AssertEqual(actual, expected);
		}

		[Test]
		public void _0x0_Test()
		{
			int[,] source = new int[0, 0];
			source = source.Transpose();
			var expected = new SimplificationInfo(new int[0], new int[0], new int[0]);

			var actual = new Simplificator(source).GetSimplificationInfo();

			AssertEqual(actual, expected);
		}

		[Test]
		public void _11_1x2_Test()
		{
			int[,] source =
			{
				{1},
				{1}
			};
			source = source.Transpose();
			var expected = new SimplificationInfo(new[] { 0 }, new[] { 0, 1 }, new[] { 0 });

			var actual = new Simplificator(source).GetSimplificationInfo();

			AssertEqual(actual, expected);
		}

		[Test]
		public void _100_3x1_Test()
		{
			int[,] source =
			{
				{1, 0, 0}
			};
			source = source.Transpose();
			var expected = new SimplificationInfo(new[] { 0, 1, 2 }, new[] { 0 }, new[] { 0 });

			var actual = new Simplificator(source).GetSimplificationInfo();

			AssertEqual(actual, expected);
		}

		[Test]
		public void _110_101_3x2_Test()
		{
			int[,] source =
			{
				{1, 1, 0},
				{1, 0, 1}
			};
			source = source.Transpose();
			var expected = new SimplificationInfo(new[] { 0, 1, 2 }, new[] { 0, 1 }, new[] { 0 });

			var actual = new Simplificator(source).GetSimplificationInfo();

			AssertEqual(actual, expected);
		}

		[Test]
		public void CompleteSimplification_5x5_Test()
		{
			int[,] source =
			{
				{1, 1, 1, 1, 1},
				{1, 0, 0, 0, 0},
				{0, 1, 0, 1, 0},
				{0, 0, 1, 0, 0},
				{0, 0, 0, 0, 1}
			};
			source = source.Transpose();
			var expected = new SimplificationInfo(new[] { 0, 1, 2, 3, 4 }, new[] { 0, 1, 2, 3, 4 }, new[] { 0, 1, 2, 4 });

			var actual = new Simplificator(source).GetSimplificationInfo();

			AssertEqual(actual, expected);
		}

		[Test]
		public void SmallSimplification_5x5_Test()
		{
			int[,] source =
			{
				{1, 1, 1, 0, 0},
				{1, 1, 1, 0, 1},
				{1, 0, 0, 1, 0},
				{0, 1, 0, 1, 0},
				{0, 0, 1, 1, 0}
			};
			source = source.Transpose();
			var expected = new SimplificationInfo(new[] { 4 }, new[] { 1 }, new int[0]);

			var actual = new Simplificator(source).GetSimplificationInfo();

			AssertEqual(actual, expected);
		}
	}
}
