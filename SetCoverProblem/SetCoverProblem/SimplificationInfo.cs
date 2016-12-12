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

		public List<int> GetOriginalSolution(List<int> solution)
		{
			return GetOriginalColumns(solution)
				.Concat(ColumnsInSolution)
				.OrderBy(x => x)
				.ToList();
		}

		private List<int> GetOriginalColumns(List<int> columns)
		{
			var originalColumns = new List<int>();
			int columnsRecovered = 0;
			foreach (var column in columns.OrderBy(x => x))
			{
				int x = column + columnsRecovered;
				int addition;
				do
				{
					addition = ColumnsExcluded.Skip(columnsRecovered).Count(e => e <= x);
					x += addition;
					columnsRecovered += addition;
				} while (addition != 0);
				originalColumns.Add(x);
			}
			return originalColumns;
		}
	}
}
