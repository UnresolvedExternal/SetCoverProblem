using System.Collections.Generic;

namespace SetCoverProblem
{
	class SimplificationInfo
	{
		public readonly List<int> ColumnsExcluded;
		public readonly List<int> RowsExcluded;
		public readonly List<int> ColumnsInSolution;

		public SimplificationInfo(List<int> columnsExcluded, List<int> rowsExcluded, List<int> columnsInSolution)
		{
			ColumnsExcluded = columnsExcluded;
			RowsExcluded = rowsExcluded;
			ColumnsInSolution = columnsInSolution;
		}
	}
}
