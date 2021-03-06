﻿using System;
using System.Collections.Generic;

namespace SetCoverProblem
{
	public static class ProblemSolver
	{
		public static List<int> GetSolution(int[,] source, double[] costs = null)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));

			if (!HasSolution(source))
				return null;
			var simplificationInfo = new Simplificator(source, costs).GetSimplificationInfo();
			var simplifiedMatrix = simplificationInfo.ApplySimplification(source);
			var greedySolution = new GreedyAlgorithm(simplifiedMatrix, costs).GetSolution();
			var solution = new MainAlgorithm(source, greedySolution, costs).GetSolution();
			return simplificationInfo.GetOriginalSolution(solution);
		}

		private static bool HasSolution(int[,] source)
		{
			for (int y = 0; y < source.GetLength(1); y++)
			{
				bool canBeCovered = false;
				for (int x = 0; x < source.GetLength(0); x++)
					canBeCovered |= source[x, y] != 0;
				if (!canBeCovered)
					return false;
			}
			return true;
		}
	}
}
