namespace advent_of_code_2024;

public class Day10
{
	public static void SolvePart1()
	{
		int[][] input = [.. File.ReadAllLines("Day10\\input.txt")
			.Select(line => line.Select(c => int.Parse($"{c}")).ToArray())];

		List<int[]> zeros = [.. input
			.SelectMany((row, i) => row.Select((val, j) => new { i, j, val }))
			.Where(x => x.val == 0)
			.Select(x => new[] { x.i, x.j })];

		int trailheadsCount = 0;

		foreach (int[] zero in zeros)
		{
			List<int[]> next = [zero];
			while (next.Count != 0)
			{
				List<int[]> found = [];
				foreach (int[] pos in next)
				{
					if (input[pos[0]][pos[1]] == 9)
					{
						trailheadsCount++;
						continue;
					}

					found.AddRange(FindNext(input, pos));
				}

				next = [.. found
					.GroupBy(pos => new { i = pos[0], j = pos[1] })
					.Select(group => group.First())];
			}
		}

		Console.WriteLine($"[Part 1] Answer: {trailheadsCount}");
	}

	public static void SolvePart2()
	{
		int[][] input = [.. File.ReadAllLines("Day10\\input.txt")
			.Select(line => line.Select(c => int.Parse($"{c}")).ToArray())];

		List<int[]> zeros = [.. input
			.SelectMany((row, i) => row.Select((val, j) => new { i, j, val }))
			.Where(x => x.val == 0)
			.Select(x => new[] { x.i, x.j })];

		int trailheadsRatings = 0;

		foreach (int[] zero in zeros)
		{
			List<int[]> next = [zero];
			while (next.Count != 0)
			{
				List<int[]> found = [];
				foreach (int[] pos in next)
				{
					if (input[pos[0]][pos[1]] == 9)
					{
						trailheadsRatings++;
						continue;
					}

					found.AddRange(FindNext(input, pos));
				}

				next = [.. found.Distinct()];
			}
		}

		Console.WriteLine($"[Part 2] Answer: {trailheadsRatings}");
	}

	private static List<int[]> FindNext(int[][] map, int[] pos)
	{
		int curr = map[pos[0]][pos[1]];
		List<int[]> next = [];

		if (pos[0] + 1 < map.Length && map[pos[0] + 1][pos[1]] == curr + 1)
		{
			next.Add([pos[0] + 1, pos[1]]);
		}
		if (pos[0] - 1 >= 0 && map[pos[0] - 1][pos[1]] == curr + 1)
		{
			next.Add([pos[0] - 1, pos[1]]);
		}
		if (pos[1] + 1 < map[pos[0]].Length && map[pos[0]][pos[1] + 1] == curr + 1)
		{
			next.Add([pos[0], pos[1] + 1]);
		}
		if (pos[1] - 1 >= 0 && map[pos[0]][pos[1] - 1] == curr + 1)
		{
			next.Add([pos[0], pos[1] - 1]);
		}

		return next;
	}
}
