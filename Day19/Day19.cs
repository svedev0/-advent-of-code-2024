using System.Collections.Concurrent;

namespace advent_of_code_2024;

public class Day19
{
	private static readonly ConcurrentDictionary<string, long> cache = new();
	private static string[] patterns = [];
	private static string[] designs = [];

	public static void SolvePart1()
	{
		string[] input = File.ReadAllText("Day19\\input.txt").Split("\r\n\r\n");
		patterns = input[0].Split(", ");
		designs = input[1].Split("\r\n");

		int result = designs.Select(PossibleMemoized).Count(r => r > 0);
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		string[] input = File.ReadAllText("Day19\\input.txt").Split("\r\n\r\n");
		patterns = input[0].Split(", ");
		designs = input[1].Split("\r\n");

		long result = designs.Sum(PossibleMemoized);
		Console.WriteLine($"[Part 2] Answer: {result}");
	}

	private static long PossibleMemoized(string design)
	{
		return cache.GetOrAdd(design, Possible);
	}

	private static long Possible(string design)
	{
		return design switch
		{
			string empty when string.IsNullOrEmpty(empty) => 1,
			string other => patterns
				.Where(other.StartsWith)
				.Sum(p => PossibleMemoized(other[p.Length..])),
		};
	}
}
