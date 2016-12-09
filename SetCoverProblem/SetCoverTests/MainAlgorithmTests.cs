using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SetCoverProblem;

namespace SetCoverTests
{
	[TestFixture]
	class MainAlgorithmTests
	{
		[Test, Timeout(1000)]
		public void _0x0_Test()
		{
			int[,] source = new int[0, 0];
			source = source.Transpose();
			var expected = new List<int>();

			var actual = new MainAlgorithm(source, new List<int>()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _0x2_Test()
		{
			int[,] source = new int[0, 2];
			source = source.Transpose();
			var expected = new List<int>();

			var actual = new MainAlgorithm(source, new List<int>()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _1_1x1_Test()
		{
			int[,] source =
			{
				{1}
			};
			source = source.Transpose();
			var expected = new[] {0}.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _11_2x1_Test()
		{
			int[,] source =
			{
				{1, 1}
			};
			source = source.Transpose();
			var expected = new[] { 0 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _10_2x1_Test()
		{
			int[,] source =
			{
				{1, 0}
			};
			source = source.Transpose();
			var expected = new[] { 0 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _01_2x1_Test()
		{
			int[,] source =
			{
				{0, 1}
			};
			source = source.Transpose();
			var expected = new[] { 1 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _00010_5x1_Test()
		{
			int[,] source =
			{
				{0, 0, 0, 1, 0}
			};
			source = source.Transpose();
			var expected = new[] { 3 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _11_1x2_Test()
		{
			int[,] source =
			{
				{1},
				{1 }
			};
			source = source.Transpose();
			var expected = new[] { 0 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _1001_2x2_Test()
		{
			int[,] source =
			{
				{1, 0},
				{0, 1}
			};
			source = source.Transpose();
			var expected = new[] { 0, 1 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _1110_2x2_Test()
		{
			int[,] source =
			{
				{1, 1},
				{1, 0}
			};
			source = source.Transpose();
			var expected = new[] { 0 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _3Cases_3x3_Test()
		{
			int[,] source =
			{
				{1,0,1 },
				{0,1,1 },
				{1,1,0 }
			};
			source = source.Transpose();
			var expected = new[] { 0,1 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _SeveralCases_5x5_Test()
		{
			int[,] source =
			{
				{1,1,1,0,0 },
				{1,1,1,0,1 },
				{1,0,0,1,0 },
				{0,1,0,1,0 },
				{0,0,1,1,0 }
			};
			source = source.Transpose();
			var expected = new[] { 0, 3 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _1Case_4x8_Test()
		{
			int[,] source =
			{
				{1,1,0,0 },
				{1,0,1,0 },
				{1,1,0,0 },
				{1,0,1,0 },
				{0,1,0,0 },
				{0,0,1,1 },
				{0,1,0,1 },
				{0,0,1,1 }
			};
			source = source.Transpose();
			var expected = new[] { 1, 2 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test, Timeout(1000)]
		public void _1Case_5x8_Test()
		{
			int[,] source =
			{
				{1,1,1,0,0 },
				{1,1,1,0,0 },
				{0,0,0,1,0 },
				{0,1,0,0,0 },
				{1,0,0,0,1 },
				{1,1,0,0,0 },
				{1,0,1,0,1 },
				{0,0,0,0,1 }
			};
			source = source.Transpose();
			var expected = new[] { 1, 3, 4 }.ToList();

			var actual = new MainAlgorithm(source, Enumerable.Range(0, source.GetLength(0)).ToList()).GetSolution();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
