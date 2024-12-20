namespace advent_of_code_2024;

public class Day20
{
	public static void SolvePart1()
	{
		string input = File.ReadAllText("Day20\\input.txt");

		int firstNlIdx = input.IndexOf("\r\n", StringComparison.Ordinal);
		int rowLength = firstNlIdx + "\r\n".Length;
		int rows = (input.Length + "\r\n".Length) / rowLength;
		int obstacles = input.Count(c => c == '#');

		int[] directions = [-rowLength, 1, rowLength, -1];

		int start = input.IndexOf('S');
		int end = input.IndexOf('E');

		int capacity = firstNlIdx * rows - obstacles;
		Dictionary<int, int> visited = new(capacity) { { start, 0 } };

		int current = start;
		int time = 0;

		while (current != end)
		{
			foreach (int direction in directions)
			{
				int newIdx = current + direction;
				if (newIdx < 0 ||
					newIdx > input.Length - 1 ||
					newIdx % rowLength > firstNlIdx ||
					input[newIdx] == '#' ||
					!visited.TryAdd(newIdx, time))
				{
					continue;
				}

				current = newIdx;
				time++;
				break;
			}
		}

		int result = 0;
		List<(int x, int y)> jumps = [(-2, 0), (0, 2), (2, 0), (0, -2)];

		foreach (var kv in visited)
		{
			(int startIdx, int startTime) = kv;
			int startX = startIdx % rowLength;
			int startY = startIdx / rowLength;

			foreach ((int x, int y) in jumps)
			{
				int endX = startX + x;
				int endY = startY + y;

				if (endY < 0 || endY > rows - 1 ||
					endX < 0 || endX > firstNlIdx - 1)
				{
					continue;
				}

				int endIdx = endY * rowLength + endX;
				if (!visited.TryGetValue(endIdx, out int endTime))
				{
					continue;
				}

				if (endTime - startTime - 2 >= 100)
				{
					result++;
				}
			}
		}

		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		string input = File.ReadAllText("Day20\\input.txt");

		int firstNlIdx = input.IndexOf("\r\n", StringComparison.Ordinal);
		int rowLength = firstNlIdx + "\r\n".Length;
		int rows = (input.Length + "\r\n".Length) / rowLength;
		int obstacles = input.Count(c => c == '#');

		int[] directions = [-rowLength, 1, rowLength, -1];

		int start = input.IndexOf('S');
		int end = input.IndexOf('E');

		int capacity = firstNlIdx * rows - obstacles;
		Dictionary<int, int> visited = new(capacity) { { start, 0 } };

		int current = start;
		int time = 0;

		while (current != end)
		{
			foreach (int direction in directions)
			{
				int newIdx = current + direction;
				if (newIdx < 0 ||
					newIdx > input.Length - 1 ||
					newIdx % rowLength > firstNlIdx ||
					input[newIdx] == '#' ||
					!visited.TryAdd(newIdx, time))
				{
					continue;
				}

				current = newIdx;
				time++;
				break;
			}
		}

		int result = 0;

		foreach (var kv in visited)
		{
			(int startIdx, int startTime) = kv;
			int startX = startIdx % rowLength;
			int startY = startIdx / rowLength;

			for (int i = -20; i <= 20; i++)
			{
				for (int j = -20 + Math.Abs(i); j <= 20 - Math.Abs(i); j++)
				{
					int endX = startX + i;
					int endY = startY + j;

					if (endY < 0 || endY > rows - 1 ||
						endX < 0 || endX > firstNlIdx - 1)
					{
						continue;
					}

					int endIdx = endY * rowLength + endX;
					if (!visited.TryGetValue(endIdx, out int endTime))
					{
						continue;
					}

					int distance = Math.Abs(i) + Math.Abs(j);
					if (endTime - startTime - distance >= 100)
					{
						result++;
					}
				}
			}
		}

		Console.WriteLine($"[Part 2] Answer: {result}");
	}
}
