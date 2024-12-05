namespace advent_of_code_2024;

public class Day05
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day05\\input.txt")];
		Dictionary<int, HashSet<int>> after = [];
		List<int> middleValues = [];

		foreach (string line in input)
		{
			if (line.Contains('|'))
			{
				int[] ruleNums = [.. line
					.Split('|', StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)];

				if (ruleNums.Length >= 2)
				{
					int u = ruleNums[0];
					int v = ruleNums[1];
					if (!after.TryGetValue(u, out HashSet<int>? value))
					{
						value = ([]);
						after[u] = value;
					}
					value.Add(v);
				}

				continue;
			}

			int[] pageNums = [.. line
				.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)];

			if (pageNums.Length % 2 == 0)
			{
				continue;
			}

			if (pageNums.Zip(pageNums.Skip(1), IsSortedWithAfter).All(x => x))
			{
				middleValues.Add(pageNums[pageNums.Length / 2]);
			}
		}

		Console.WriteLine($"[Part 1] Answer: {middleValues.Sum()}");

		bool IsSortedWithAfter(int a, int b)
		{
			return after.ContainsKey(a) && after[a].Contains(b);
		}
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day05\\input.txt")];
		Dictionary<int, HashSet<int>> after = [];
		List<int> middleValues = [];

		foreach (string line in input)
		{
			if (line.Contains('|'))
			{
				int[] ruleNums = [.. line
					.Split('|', StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)];

				if (ruleNums.Length >= 2)
				{
					int u = ruleNums[0];
					int v = ruleNums[1];
					if (!after.TryGetValue(u, out HashSet<int>? value))
					{
						value = ([]);
						after[u] = value;
					}
					value.Add(v);
				}

				continue;
			}

			List<int> pageNums = [.. line
				.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)];

			if (pageNums.Count % 2 == 0)
			{
				continue;
			}

			if (!pageNums.Zip(pageNums.Skip(1), IsSortedWithAfter).All(x => x))
			{
				pageNums.Sort((a, b) => IsSortedWithAfter(a, b) ? -1 : 1);
				middleValues.Add(pageNums[pageNums.Count / 2]);
			}
		}

		Console.WriteLine($"[Part 2] Answer: {middleValues.Sum()}");

		bool IsSortedWithAfter(int a, int b)
		{
			return after.ContainsKey(a) && after[a].Contains(b);
		}
	}
}
