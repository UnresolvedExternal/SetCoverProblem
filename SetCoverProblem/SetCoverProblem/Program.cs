using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SetCoverProblem
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var matrix = EnterMatrix();
				var costs = EnterCosts(matrix.GetLength(0));
				var solution = ProblemSolver.GetSolution(matrix, costs);
				PrintSolution(solution);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
			}
		}

		private static double[] EnterCosts(int size)
		{
			string line = Console.ReadLine() ?? "";
			var costs = new double[size];
			int x = 0;
			foreach (var match in Regex.Matches(line, @"[+-]?\d+(\.\d+)?").Cast<Match>())
				costs[x++] = double.Parse(match.Value);
			if (x != size)
				throw new Exception("Invalid input");
			return costs;
		}

		private static void PrintSolution(List<int> solution)
		{
			Console.Write("Best solution is: ");
			Console.WriteLine(solution == null ? "<none>" : string.Join(", ", solution.Select(x => x + 1)));
		}

		private static int[,] EnterMatrix()
		{
			Console.Write("Enter width: ");
			int width = int.Parse(Console.ReadLine() ?? "");
			if (width < 0)
				throw new Exception("Invalid input");

			Console.Write("Enter height: ");
			int height = int.Parse(Console.ReadLine() ?? "");
			if (height < 0)
				throw new Exception("Invalid input");

			Console.WriteLine($"Enter {height}x{width} maxtrix:");
			var matrix = new int[width, height];
			for (int y = 0; y < height; y++)
			{
				string line = Console.ReadLine() ?? "";
				int x = 0;
				foreach (var match in Regex.Matches(line, @"[01]").Cast<Match>())
					matrix[x++, y] = int.Parse(match.Value);
				if (x != width)
					throw new Exception("Invalid input");
			}
			return matrix;
		}
	}
}
