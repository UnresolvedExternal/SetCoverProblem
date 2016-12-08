using System.Collections.Generic;
using System.Linq;

namespace SetCoverProblem
{
	public class SimplificationInfo
	{
		public readonly List<int> ColumnsExcluded;
		public readonly List<int> RowsExcluded;
		public readonly List<int> ColumnsInSolution;

		public SimplificationInfo(IEnumerable<int> columnsExcluded, IEnumerable<int> rowsExcluded, 
			IEnumerable<int> columnsInSolution)
		{
			ColumnsExcluded = columnsExcluded.Distinct().OrderBy(x => x).ToList();
			RowsExcluded = rowsExcluded.Distinct().OrderBy(x => x).ToList();
			ColumnsInSolution = columnsInSolution.Distinct().OrderBy(x => x).ToList();
		}

		public int[,] ApplySimplification(int[,] source)
		{
			source = source.RemoveColumns(ColumnsExcluded);
			source = source.RemoveRows(RowsExcluded);
			return source;
		}
	}
}
