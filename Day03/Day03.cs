using System.Collections.Generic;

namespace advent_of_code_2024;

public class Day03
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day03\\input.txt")];
		List<int> products = [];

		foreach (string line in input)
		{
			string[] parts = GetMulOpParts(line);
			products.AddRange(GetMulOpSums(parts));
		}

		Console.WriteLine($"[Part 1] Answer: {products.Sum()}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day03\\input.txt")];
		List<int> products = [];

		string[] doSegments = [.. string.Join(string.Empty, input)
			.Split("do()")
			.Select(s => s.Split("don't()")[0])];

		string filteredInput = string.Join(string.Empty, doSegments);
		string[] parts = GetMulOpParts(filteredInput);
		products.AddRange(GetMulOpSums(parts));

		Console.WriteLine($"[Part 2] Answer: {products.Sum()}");
	}

	private static string[] GetMulOpParts(string line)
	{
		return [.. line.Split("mul")
			.Where(s =>
				s.StartsWith('(') &&
				s.Contains(',') &&
				s.Contains(')') &&
				s.Split(')').Length >= 2)
			.Select(s => s.Split(')')[0].Replace("(", string.Empty))
			.Where(s =>
				s.Split(",").Length == 2 &&
				s.Replace(",", string.Empty).All(char.IsNumber))];
	}

	private static int[] GetMulOpSums(string[] parts)
	{
		return [.. parts.Select(p => p.Split(",")
			.Select(int.Parse)
			.Aggregate((a, b) => a * b))];
	}
}
