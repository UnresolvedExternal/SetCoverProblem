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
			var matrix = EnterMatrix();
			var solution = ProblemSolver.GetSolution(matrix);
			PrintSolution(solution);
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
			Console.Write("Enter height: ");
			int height = int.Parse(Console.ReadLine() ?? "");
			Console.WriteLine($"Enter {height}x{width} maxtrix:");
			var matrix = new int[width, height];
			for (int y = 0; y < height; y++)
			{
				string line = Console.ReadLine() ?? "";
				int x = 0;
				foreach (var match in Regex.Matches(line, @"[01]").Cast<Match>())
					matrix[x++, y] = int.Parse(match.Value);
			}
			return matrix;
		}
	}
}
