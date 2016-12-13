using System.Collections.Generic;
using System.Linq;

namespace SetCoverProblem
{
	public class GreedyAlgorithm
	{
		private readonly int[,] _source;
		private readonly int[] _isColumnTaken;
		private readonly int[] _isRowCovered;
		private readonly double[] _costs;

		public GreedyAlgorithm(int[,] source, double[] costs)
		{
			_source = source;
			_isColumnTaken = new int[source.GetLength(0)];
			_isRowCovered = new int[source.GetLength(1)];
			_costs = costs ?? Enumerable.Repeat(1.0, _isColumnTaken.Length).ToArray();
		}

		public List<int> GetSolution()
		{
			while (!IsCovered())
			{
				int x = _source.GetBestColumn(_isColumnTaken, _isRowCovered, _costs);
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
