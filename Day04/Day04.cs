namespace advent_of_code_2024;

public class Day04
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day04\\input.txt")];
		char[][] grid = [.. input.Select(line => line.ToCharArray())];
		int[][] directions = [
			[0, 1],
			[1, 0],
			[1, 1],
			[1, -1],
			[0, -1],
			[-1, 0],
			[-1, -1],
			[-1, 1]
		];

		string word = "XMAS";
		int count = 0;

		for (int x = 0; x < input.Count; x++)
		{
			for (int y = 0; y < input[0].Length; y++)
			{
				count += directions.Count(dir =>
				{
					for (int i = 0; i < word.Length; i++)
					{
						int newX = x + i * dir[0];
						int newY = y + i * dir[1];
						if (newX < 0 || newY < 0)
						{
							return false;
						}
						else if (newX >= grid.Length || newY >= grid[0].Length)
						{
							return false;
						}
						else if (grid[newY][newX] != word[i])
						{
							return false;
						}
					}
					return true;
				});
			}
		}

		Console.WriteLine($"[Part 1] Answer: {count}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day04\\input.txt")];
		char[][] grid = [.. input.Select(line => line.ToCharArray())];

		int count = 0;

		for (int x = 1; x < input.Count - 1; x++)
		{
			for (int y = 1; y < input[0].Length - 1; y++)
			{
				if (grid[x][y] == 'A')
				{
					bool tl2br = grid[x - 1][y - 1] == 'M' && grid[x + 1][y + 1] == 'S';
					bool br2tl = grid[x - 1][y - 1] == 'S' && grid[x + 1][y + 1] == 'M';
					bool bl2tr = grid[x - 1][y + 1] == 'M' && grid[x + 1][y - 1] == 'S';
					bool tr2bl = grid[x - 1][y + 1] == 'S' && grid[x + 1][y - 1] == 'M';

					if ((tl2br || br2tl) && (tr2bl || bl2tr))
					{
						count++;
					}
				}
			}
		}

		Console.WriteLine($"[Part 2] Answer: {count}");
	}
}
