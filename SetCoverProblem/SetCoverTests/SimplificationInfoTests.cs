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
	class SimplificationInfoTests
	{
		[Test, Timeout(1000)]
		public void RecoveryTest1()
		{
			var si = new SimplificationInfo(
				new[] {1, 2, 3, 5},
				new int[] {},
				new int[] {});
			var solution = new List<int>(new[] {0, 2});
			var expected = new List<int>(new[] {0, 6});

			var actual = si.GetOriginalSolution(solution);

			Assert.That(actual, Is.EquivalentTo(expected));
		}

		[Test, Timeout(1000)]
		public void RecoveryTest2()
		{
			var si = new SimplificationInfo(
				new[] { 1, 2, 5 },
				new int[] { },
				new int[] { });
			var solution = new List<int>(new[] { 0, 1, 2, 3, 4, 5 });
			var expected = new List<int>(new[] { 0, 3, 4, 6, 7, 8 });

			var actual = si.GetOriginalSolution(solution);

			Assert.That(actual, Is.EquivalentTo(expected));
		}

		[Test, Timeout(1000)]
		public void RecoveryTestOdd1()
		{
			var si = new SimplificationInfo(
				new[] { 1, 3, 5, 7, 9 },
				new int[] { },
				new int[] { });
			var solution = new List<int>(new[] { 0, 1, 2, 3, 4, 5});
			var expected = new List<int>(new[] { 0, 2, 4, 6, 8, 10});

			var actual = si.GetOriginalSolution(solution);

			Assert.That(actual, Is.EquivalentTo(expected));
		}
	}
}
