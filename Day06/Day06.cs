namespace advent_of_code_2024;

public class Day06
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day06\\input.txt")];
		int rowCount = input.Count;
		int colCount = input[0].Length;
		int startRow = input.IndexOf(input.First(x => x.Contains('^')));
		int startCol = input.First(x => x.Contains('^')).IndexOf('^');

		int positionsCount = 0;
		for (int obsRow = 0; obsRow < rowCount; obsRow++)
		{
			for (int obsCol = 0; obsCol < colCount; obsCol++)
			{
				int currRow = startRow;
				int currCol = startCol;
				int dir = 0; // 0 = up, 1 = right, 2 = down, 3 = left

				HashSet<(int, int, int)> visitedPositions = [];
				HashSet<(int, int)> visitedRowColumn = [];

				while (true)
				{
					if (visitedPositions.Contains((currRow, currCol, dir)))
					{
						break;
					}
					visitedPositions.Add((currRow, currCol, dir));
					visitedRowColumn.Add((currRow, currCol));

					(int deltaRow, int deltaCol) = new (int, int)[]
						{ (-1, 0), (0, 1), (1, 0), (0, -1) }[dir];
					int nextRow = currRow + deltaRow;
					int nextCol = currCol + deltaCol;

					if (!(0 <= nextRow && nextRow < rowCount &&
						0 <= nextCol && nextCol < colCount))
					{
						if (input[obsRow][obsCol] == '#')
						{
							positionsCount = visitedRowColumn.Count;
						}
						break;
					}

					if (input[nextRow][nextCol] == '#' ||
						(nextRow == obsRow && nextCol == obsCol))
					{
						dir = (dir + 1) % 4;
						continue;
					}

					currRow = nextRow;
					currCol = nextCol;
				}
			}
		}

		Console.WriteLine($"[Part 1] Answer: {positionsCount}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day06\\input.txt")];
		int rowCount = input.Count;
		int colCount = input[0].Length;
		int startRow = input.IndexOf(input.First(x => x.Contains('^')));
		int startCol = input.First(x => x.Contains('^')).IndexOf('^');

		int positionsCount = 0;
		for (int obsRow = 0; obsRow < rowCount; obsRow++)
		{
			for (int obsCol = 0; obsCol < colCount; obsCol++)
			{
				int currRow = startRow;
				int currCol = startCol;
				int dir = 0; // 0 = up, 1 = right, 2 = down, 3 = left
				HashSet<(int, int, int)> visitedPositions = [];

				while (true)
				{
					if (visitedPositions.Contains((currRow, currCol, dir)))
					{
						positionsCount++;
						break;
					}
					visitedPositions.Add((currRow, currCol, dir));

					(int deltaRow, int deltaCol) = new (int, int)[]
						{ (-1, 0), (0, 1), (1, 0), (0, -1) }[dir];
					int nextRow = currRow + deltaRow;
					int nextCol = currCol + deltaCol;

					if (!(0 <= nextRow && nextRow < rowCount &&
						0 <= nextCol && nextCol < colCount))
					{
						break;
					}

					if (input[nextRow][nextCol] == '#' ||
						(nextRow == obsRow && nextCol == obsCol))
					{
						dir = (dir + 1) % 4;
						continue;
					}

					currRow = nextRow;
					currCol = nextCol;
				}
			}
		}

		Console.WriteLine($"[Part 2] Answer: {positionsCount}");
	}
}
