﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public List<int> GetAnswer()
		{
			while (!IsCovered())
			{
				int x = _source.GetMaxSumColumn(_isColumnTaken, _isRowCovered);
				for (int y = 0; y < _isRowCovered.Length; y++)
					if (_source[x, y] == 1)
						_isRowCovered[y] = 1;
			}
			return _isColumnTaken.Select((e, x) => x).Where(x => _isColumnTaken[x] > 0).ToList();
		}

		private bool IsCovered()
		{
			return _isRowCovered.Any(e => e > 0);
		}
	}
}
