namespace advent_of_code_2024;

public class Day11
{
	private static Dictionary<long, long> cache = [];

	public static void SolvePart1()
	{
		cache = [];
		string input = File.ReadAllText("Day11\\input.txt");
		long[] initialStones = [.. input.Split(' ').Select(long.Parse)];
		long result = initialStones.Sum(val => Calculate(val, 25));
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		cache = [];
		string input = File.ReadAllText("Day11\\input.txt");
		long[] initialStones = [.. input.Split(' ').Select(long.Parse)];
		long result = initialStones.Sum(val => Calculate(val, 75));
		Console.WriteLine($"[Part 2] Answer: {result}");
	}

	private static long Calculate(long val, long depth)
	{
		if (depth == 0)
		{
			return 1;
		}

		long cacheKey = (val << 7) | (--depth);
		if (!cache.TryGetValue(cacheKey, out long result))
		{
			if (val == 0)
			{
				result = Calculate(1, depth);
				cache.Add(cacheKey, result);
				return result;
			}

			long power = (long)Math.Log10(val) + 1;
			if ((power & 1) != 0)
			{
				result = Calculate(val * 2024, depth);
				cache.Add(cacheKey, result);
				return result;
			}

			long midDiv = (long)Math.Pow(10, power >> 1);
			result = Calculate(val / midDiv, depth) + Calculate(val % midDiv, depth);
			cache.Add(cacheKey, result);
		}

		return result;
	}
}
