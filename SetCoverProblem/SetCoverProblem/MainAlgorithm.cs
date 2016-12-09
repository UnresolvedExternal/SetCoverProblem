using System;
using System.Collections.Generic;
using System.Linq;

namespace SetCoverProblem
{
	public class MainAlgorithm
	{
		private readonly int[,] _source;
		private readonly List<int> _bestSolution;
		private readonly int[] _isColumnTaken;
		private readonly int[] _isRowCovered;
		private readonly int[] _index;
		private bool _isSolutionFound;
		private int _currentCost;

		public MainAlgorithm(int[,] source, List<int> bestSolution)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (bestSolution == null) throw new ArgumentNullException(nameof(bestSolution));

			_source = source;
			_bestSolution = bestSolution;
			_isColumnTaken = new int[_source.GetLength(0)];
			_isRowCovered = new int[_source.GetLength(1)];
			_index = new int[_source.GetLength(0)];
		}

		public List<int> GetSolution()
		{
			if (_isSolutionFound)
				return _bestSolution;
			int index = 0;
			do
			{
				index = ProcessTakingColumns(index);
				ProcessBacktracking();
			} while (IndexExists());
			_isSolutionFound = true;
			return _bestSolution;
		}

		private void ProcessBacktracking()
		{
			while (IndexExists())
			{
				int x = GetMaxIndexColumn();
				if (_isColumnTaken[x] != 0)
				{
					TakeColumn(x, -1, _index[x]);
					break;
				}
				_index[x] = 0;
			}
		}

		private int ProcessTakingColumns(int index)
		{
			while (GetMinThreshold() < _bestSolution.Count)
			{
				if (IsCovered())
				{
					UpdateBestSolution();
					break;
				}

				index++;
				int x = _source.GetMaxSumColumn(_index, _isRowCovered);
				TakeColumn(x, 1, index);
			}
			return index;
		}

		private int GetMaxIndexColumn()
		{
			int x = -1;
			int max = 0;
			for (int i = 0; i < _index.Length; i++)
				if (_index[i] > max)
				{
					max = _index[i];
					x = i;
				}
			return x;
		}

		private void UpdateBestSolution()
		{
			_bestSolution.Clear();
			for (int x = 0; x < _isColumnTaken.Length; x++)
				if (_isColumnTaken[x] != 0)
					_bestSolution.Add(x);
		}

		private int GetMinThreshold()
		{
			int freeColumns = _index.Count(x => x == 0);
			int uncoveredRows = _isRowCovered.Count(y => y == 0);
			int threshold = _currentCost;
			var invalidIndexes = new List<int>();
			for (int k = 0; k < freeColumns && uncoveredRows > 0; k++)
			{
				threshold++;
				int x = _source.GetMaxSumColumn(_index, _isRowCovered);
				int sum = _source.SumColumn(x, _isRowCovered);
				uncoveredRows -= sum;
				_index[x] = 1;
				invalidIndexes.Add(x);
			}
			foreach (var i in invalidIndexes)
				_index[i] = 0;
			return threshold;
		}

		private bool IsCovered() => _isRowCovered.All(y => y != 0);

		private bool IndexExists() => _index.Any(x => x != 0);

		private void TakeColumn(int x, int count, int index)
		{
			_isColumnTaken[x] += count;
			_currentCost += count;
			for (int y = 0; y < _isRowCovered.Length; y++)
				if (_source[x, y] == 1)
					_isRowCovered[y] += count;
			_index[x] = index;
		}
	}
}
