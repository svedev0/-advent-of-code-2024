namespace advent_of_code_2024;

public class Day02
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day02\\input.txt")];
		List<int[]> reports = [.. input.Select(line =>
			line.Split(" ").Select(int.Parse).ToArray())];

		int safeCount = 0;

		foreach (int[] report in reports)
		{
			if (IsSafe(report))
			{
				safeCount++;
			}
		}

		Console.WriteLine($"[Part 1] Answer: {safeCount}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day02\\input.txt")];
		List<int[]> reports = [.. input.Select(line =>
			line.Split(" ").Select(int.Parse).ToArray())];

		int safeCount = 0;

		foreach (int[] report in reports)
		{
			bool isTolerable = false;
			for (int i = 0; i < report.Length; i++)
			{
				if (IsSafe([.. report[..i], .. report.Skip(i + 1)]))
				{
					isTolerable = true;
					break;
				}
			}

			if (IsSafe(report) || isTolerable)
			{
				safeCount++;
			}
		}

		Console.WriteLine($"[Part 2] Answer: {safeCount}");
	}

	private static bool IsSafe(int[] report)
	{
		List<int> diffs = [.. report.Skip(1)
				.Select((val, i) => val - report[i])];

		bool allIncr = diffs.All(d => d >= 1 && d <= 3);
		bool allDecr = diffs.All(d => d <= -1 && d >= -3);
		return allIncr || allDecr;
	}
}
