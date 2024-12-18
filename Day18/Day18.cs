using System.Numerics;

namespace advent_of_code_2024;

public class Day18
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day18\\input.txt")];
		List<Vector2> obstacles = [.. input
			.Select(x => x.Split(','))
			.Select(y => new Vector2(float.Parse(y[0]), float.Parse(y[1])))];
		int x = 71;
		int y = 71;

		bool[,] map = PlaceObstacles(obstacles, CreateGrid(x, y), 1024);
		int result = Dijkstra(map, 0, 0, x - 1, y - 1);
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day18\\input.txt")];
		List<Vector2> obstacles = [.. input
			.Select(x => x.Split(','))
			.Select(y => new Vector2(float.Parse(y[0]), float.Parse(y[1])))];
		int x = 71;
		int y = 71;

		bool[,] emptyMap = CreateGrid(x, y);
		Vector2 result = Vector2.Zero;

		for (int i = 1; i <= obstacles.Count; i++)
		{
			bool[,] map = PlaceObstacles(obstacles, emptyMap, i);
			if (Dijkstra(map, 0, 0, x - 1, y - 1) == -1)
			{
				result = obstacles[i - 1];
				break;
			}
		}

		Console.WriteLine($"[Part 2] Answer: {result.X}, {result.Y}");
	}

	private static bool[,] CreateGrid(int maxX, int maxY)
	{
		bool[,] grid = new bool[maxX, maxY];
		for (int y = 0; y < maxY; y++)
		{
			for (int x = 0; x < maxX; x++)
			{
				grid[x, y] = true;
			}
		}
		return grid;
	}

	private static bool[,] PlaceObstacles(List<Vector2> obstacles, bool[,] grid, int count)
	{
		foreach (var obstacle in obstacles.Take(count))
		{
			grid[(int)obstacle.X, (int)obstacle.Y] = false;
		}
		return grid;
	}

	private static int Dijkstra(bool[,] grid, int sX, int sY, int eX, int eY)
	{
		int rows = grid.GetLength(0);
		int cols = grid.GetLength(1);

		(int dx, int dy)[] directions = [(0, 1), (1, 0), (0, -1), (-1, 0)];
		PriorityQueue<(int x, int y, int cost), int> queue = new();
		queue.Enqueue((sX, sY, 0), 0);

		HashSet<(int, int)> visited = [];

		while (queue.Count > 0)
		{
			(int currX, int currY, int currCost) = queue.Dequeue();
			if ((currX, currY) == (eX, eY))
			{
				return currCost;
			}
			if (visited.Contains((currX, currY)))
			{
				continue;
			}

			visited.Add((currX, currY));

			foreach (var (dx, dy) in directions)
			{
				int newX = currX + dx;
				int newY = currY + dy;
				if (newX >= 0 && newX < rows &&
					newY >= 0 && newY < cols &&
					!visited.Contains((newX, newY)) &&
					grid[newX, newY])
				{
					queue.Enqueue((newX, newY, currCost + 1), currCost + 1);
				}
			}
		}

		return -1;
	}
}
