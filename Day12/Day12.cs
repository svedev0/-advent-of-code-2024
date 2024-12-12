namespace advent_of_code_2024;

public class Day12
{
	private record Region(long Area, long Perimeter, long Sides);
	private record Position(int X, int Y, int Direction);

	private static string[] input = [];
	private static readonly Queue<(int x, int y)> tasks = new();
	private static List<Region> regions = [];
	private static bool[,] visited = null!;
	private static HashSet<Position> fence = [];

	public static void SolvePart1()
	{
		input = File.ReadAllLines("Day12\\input.txt");
		long result = GetRegions().Select(r => r.Area * r.Perimeter).Sum();
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		input = File.ReadAllLines("Day12\\input.txt");
		long result = GetRegions().Select(r => r.Area * r.Sides).Sum();
		Console.WriteLine($"[Part 2] Answer: {result}");
	}

	private static List<Region> GetRegions()
	{
		visited = new bool[input[0].Length, input.Length];
		regions = [];

		int currRegion = 0;

		for (int y = 0; y < input.Length; y++)
		{
			for (int x = 0; x < input[y].Length; x++)
			{
				if (!visited[x, y])
				{
					tasks.Enqueue((x, y));
					fence = [];
					regions.Add(new(0, 0, 0));

					char plant = input[y][x];
					ExploreRegion(plant, currRegion);

					int sides = CalculateFenceSides();
					long area = regions[currRegion].Area;
					long perimeter = regions[currRegion].Perimeter;

					regions[currRegion] = new Region(area, perimeter, sides);
					currRegion++;
				}
			}
		}

		return regions;
	}

	private static void ExploreRegion(char plant, int currRegion)
	{
		while (tasks.Count > 0)
		{
			(int x, int y) = tasks.Dequeue();
			if (visited[x, y])
			{
				continue;
			}

			visited[x, y] = true;

			int perimeter =
				ExploreFence(plant, new Position(x, y - 1, 1)) +
				ExploreFence(plant, new Position(x, y + 1, 2)) +
				ExploreFence(plant, new Position(x - 1, y, 3)) +
				ExploreFence(plant, new Position(x + 1, y, 4));

			long newArea = regions[currRegion].Area + 1;
			long newPerimeter = regions[currRegion].Perimeter + perimeter;
			regions[currRegion] = new Region(newArea, newPerimeter, 0);
		}
	}

	private static int ExploreFence(char plant, Position pos)
	{
		(int x, int y, _) = pos;
		if (y >= 0 && y < input.Length && x >= 0 && x < input[y].Length)
		{
			if (input[y][x] == plant)
			{
				if (!visited[x, y])
				{
					tasks.Enqueue((x, y));
				}
				return 0;
			}
		}

		fence.Add(pos);
		return 1;
	}

	private static int CalculateFenceSides()
	{
		int sides = 0;

		while (fence.Count > 0)
		{
			sides++;
			Position currPos = fence.First();
			Position nextPos;
			fence.Remove(currPos);

			(int x, int y, int dir) = currPos;
			nextPos = new(x + 1, y, dir);
			while (fence.Contains(nextPos))
			{
				fence.Remove(nextPos);
				x++;
				nextPos = new(x + 1, y, dir);
			}

			(x, y, dir) = currPos;
			nextPos = new(x - 1, y, dir);
			while (fence.Contains(nextPos))
			{
				fence.Remove(nextPos);
				x--;
				nextPos = new(x - 1, y, dir);
			}

			(x, y, dir) = currPos;
			nextPos = new(x, y + 1, dir);
			while (fence.Contains(nextPos))
			{
				fence.Remove(nextPos);
				y++;
				nextPos = new(x, y + 1, dir);
			}

			(x, y, dir) = currPos;
			nextPos = new(x, y - 1, dir);
			while (fence.Contains(nextPos))
			{
				fence.Remove(nextPos);
				y--;
				nextPos = new(x, y - 1, dir);
			}
		}

		return sides;
	}
}
