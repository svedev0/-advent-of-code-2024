namespace advent_of_code_2024;

public class Day14
{
	private record Robot(int Px, int Py, int Vx, int Vy);

	private const int width = 101;
	private const int height = 103;

	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day14\\input.txt")];
		List<Robot> robots = ParseInput(input);

		for (int i = 0; i < 100; i++)
		{
			for (int r = 0; r < robots.Count; r++)
			{
				Robot robot = robots[r];
				int newX = (width + (robot.Px + robot.Vx) % width) % width;
				int newY = (height + (robot.Py + robot.Vy) % height) % height;
				robots[r] = new Robot(newX, newY, robot.Vx, robot.Vy);
			}
		}

		int topL = robots.Count(r => r is { Px: < width / 2, Py: < height / 2 });
		int topR = robots.Count(r => r is { Px: > width / 2, Py: < height / 2 });
		int botL = robots.Count(r => r is { Px: < width / 2, Py: > height / 2 });
		int botR = robots.Count(r => r is { Px: > width / 2, Py: > height / 2 });

		int result = topL * topR * botL * botR;
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day14\\input.txt")];
		List<Robot> robots = ParseInput(input);
		int count = 0;

		while (true)
		{
			count++;

			for (int r = 0; r < robots.Count; r++)
			{
				Robot robot = robots[r];
				int newX = (width + (robot.Px + robot.Vx) % width) % width;
				int newY = (height + (robot.Py + robot.Vy) % height) % height;
				robots[r] = new Robot(newX, newY, robot.Vx, robot.Vy);
			}

			int positions = robots.Select(r => (r.Px, r.Py)).Distinct().Count();
			if (positions == robots.Count)
			{
				Console.WriteLine($"[Part 2] Answer: {count}");
				// Uncomment to print the easter egg
				// PrintEasterEgg(robots);
				break;
			}
		}
	}

	private static List<Robot> ParseInput(List<string> input)
	{
		List<Robot> robots = [];
		foreach (string line in input)
		{
			string[] parts = line.Split(' ');
			string[] pos = parts[0].Split(',');
			string[] vel = parts[1].Split(',');

			robots.Add(new(
				int.Parse(pos[0][2..]),
				int.Parse(pos[1]),
				int.Parse(vel[0][2..]),
				int.Parse(vel[1])));
		}
		return robots;
	}

	private static void PrintEasterEgg(List<Robot> robots)
	{
		Console.WriteLine();
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				char c = robots.Any(r => r.Px == x && r.Py == y) ? '#' : '.';
				Console.Write(c);
			}
			Console.WriteLine();
		}
	}
}
