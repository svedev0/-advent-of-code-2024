namespace advent_of_code_2024;

public class Day01
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day01\\input.txt")];

		int[] leftVals = [.. input.Select(x => int.Parse(x.Split(" ")[0]))
			.OrderBy(x => x)];

		int[] rightVals = [.. input.Select(x => int.Parse(x.Split(" ")[^1]))
			.OrderBy(x => x)];

		List<int> distances = [];
		for (int i = 0; i < leftVals.Length; i++)
		{
			int diff = leftVals[i] - rightVals[i];
			if (diff < 0)
			{
				diff = -diff;
			}
			distances.Add(diff);
		}

		Console.WriteLine($"[Part 1] Answer: {distances.Sum()}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day01\\input.txt")];

		int[] leftVals = [.. input.Select(x => int.Parse(x.Split(" ")[0]))];
		int[] rightVals = [.. input.Select(x => int.Parse(x.Split(" ")[^1]))];

		int similarityScore = 0;
		foreach (int val in leftVals)
		{
			int count = rightVals.Count(x => x == val);
			if (count > 0)
			{
				similarityScore += val * count;
			}
		}

		Console.WriteLine($"[Part 2] Answer: {similarityScore}");
	}
}
