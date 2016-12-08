﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SetCoverProblem
{
	enum ColumnInfo
	{
		Unknown = 0,
		Excluded,
		Included
	}

	public class Simplificator
	{
		private readonly int[,] _source;
		private readonly bool[] _rowsCovered;
		private readonly ColumnInfo[] _columnsInfo;

		public Simplificator(int[,] source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));

			_source = source;
			_rowsCovered = new bool[source.GetLength(1)];
			_columnsInfo = new ColumnInfo[source.GetLength(0)];
		}

		public SimplificationInfo GetSimplificationInfo()
		{
			while (ApplySimplificationIteration())
			{
			}
			return CreateInfo();
		}

		private bool ApplySimplificationIteration()
		{
			bool modified = false;
			modified |= ExcludeSupersetRows();
			modified |= ExcludeSubsetColumns();
			modified |= CoverOneUnitRows();
			return modified;
		}

		private bool ExcludeSupersetRows()
		{
			bool modified = false;
			int[] isInvalidColumn = _columnsInfo.Select(i => i == ColumnInfo.Unknown ? 0 : 1).ToArray();
			for (int yOne = 0; yOne < _source.GetLength(1); yOne++)
			{
				if (_rowsCovered[yOne])
					continue;
				for (int yTwo = 0; yTwo < _source.GetLength(1); yTwo++)
				{
					if (_rowsCovered[yTwo])
						continue;
					if (yOne != yTwo && _source.IsRowOneSupersetOfRowTwo(yTwo, yOne, isInvalidColumn))
					{
						modified = true;
						ExcludeRow(yTwo);
					}
				}
			}
			return modified;
		}

		private void ExcludeRow(int y)
		{
			_rowsCovered[y] = true;
		}

		private SimplificationInfo CreateInfo()
		{
			var columnsExcluded = new List<int>();
			var columnsInSolution = new List<int>();
			for (int x = 0; x < _columnsInfo.Length; x++)
			{
				if (_columnsInfo[x] == ColumnInfo.Included)
				{
					columnsExcluded.Add(x);
					columnsInSolution.Add(x);
				} else if (_columnsInfo[x] == ColumnInfo.Excluded)
				{
					columnsExcluded.Add(x);
				}
			}

			var rowsExcluded = new List<int>();
			for (int y = 0; y < _rowsCovered.Length; y++)
				if (_rowsCovered[y])
					rowsExcluded.Add(y);

			return new SimplificationInfo(columnsExcluded, rowsExcluded, columnsInSolution);
		}

		private void ExcludeColumn(int x, bool inSolution)
		{
			_columnsInfo[x] = inSolution ? ColumnInfo.Included : ColumnInfo.Excluded;
			if (inSolution)
			{
				for (int y = 0; y < _source.GetLength(1); y++)
				{
					if (_source[x, y] == 1)
						_rowsCovered[y] = true;
				}
			}
		}

		private bool ExcludeSubsetColumns()
		{
			bool modified = false;
			int[] isInvalidRow = _rowsCovered.Select(covered => covered ? 1 : 0).ToArray();
			for (int xOne = 0; xOne < _source.GetLength(0); xOne++)
			{
				if (_columnsInfo[xOne] != ColumnInfo.Unknown)
					continue;
				for (int xTwo = 0; xTwo < _source.GetLength(0); xTwo++)
				{
					if (_columnsInfo[xTwo] != ColumnInfo.Unknown)
						continue;
					if (xOne != xTwo && _source.IsColumnOneSupersetOfColumnTwo(xOne, xTwo, isInvalidRow))
					{
						modified = true;
						ExcludeColumn(xTwo, false);
					}
				}
			}
			return modified;
		}

		private bool CoverOneUnitRows()
		{
			bool modified = false;
			int[] isInvalidColumn = _columnsInfo.Select(i => i == ColumnInfo.Excluded ? 1 : 0).ToArray();
			for (int y = 0; y < _source.GetLength(1); y++)
				if (!_rowsCovered[y])
				{
					int sum = _source.SumRow(y, isInvalidColumn);
					if (sum == 1)
					{
						int firstOne = _source.FindInRow(y, 1, isInvalidColumn);
						modified = true;
						ExcludeColumn(firstOne, true);
					}
				}
			return modified;
		}
	}
}
