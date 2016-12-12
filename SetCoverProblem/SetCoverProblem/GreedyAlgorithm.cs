using System.Collections.Generic;
using System.Linq;

namespace SetCoverProblem
{
	public class GreedyAlgorithm
	{
		private readonly int[,] _source;
		private readonly int[] _isColumnTaken;
		private readonly int[] _isRowCovered;

		public GreedyAlgorithm(int[,] source)
		{
			_source = source;
			_isColumnTaken = new int[source.GetLength(0)];
			_isRowCovered = new int[source.GetLength(1)];
		}

		public List<int> GetSolution()
		{
			while (!IsCovered())
			{
				int x = _source.GetMaxSumColumn(_isColumnTaken, _isRowCovered);
				_isColumnTaken[x] = 1;
				for (int y = 0; y < _isRowCovered.Length; y++)
					if (_source[x, y] != 0)
						_isRowCovered[y] = 1;
			}
			return _isColumnTaken.Select((e, x) => x).Where(x => _isColumnTaken[x] > 0).ToList();
		}

		private bool IsCovered()
		{
			return _isRowCovered.All(e => e > 0);
		}
	}
}
