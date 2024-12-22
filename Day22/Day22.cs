using System.Collections;

namespace advent_of_code_2024;

public class Day22
{
	// Big prime number used as fixed range and for key hashing in BitArray.
	// It also allows for array access & updates in O(1) time.
	private const int Magic = 130321;
	// Mask to keep only lower 24 bits
	private const long Mask24Bits = 0xFFFFFF;
	private static int[] sums = new int[Magic];
	private static BitArray seen = new(Magic);

	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day22\\input.txt")];
		long[] nums = [.. input.Select(long.Parse)];

		long result = nums.Sum(x =>
			Enumerable.Range(0, 2000).Aggregate(x, MoveNext));
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day22\\input.txt")];
		long[] nums = [.. input.Select(long.Parse)];
		sums = new int[Magic];
		seen = new(Magic);

		long result = nums.Aggregate(0, Max);
		Console.WriteLine($"[Part 2] Answer: {result}");
	}

	private static long MoveNext(long secret, int _ = 0)
	{
		secret ^= secret << 6;
		secret &= Mask24Bits;
		secret ^= secret >> 5;
		secret ^= secret << 11;
		secret &= Mask24Bits;
		return secret;
	}

	private static int Max(int max, long secret)
	{
		seen.SetAll(false);
		int key = 0;
		int prev = 0;
		int curr = 0;

		for (int i = 0; i <= 2000; secret = MoveNext(secret), i++)
		{
			prev = curr;
			curr = (int)(secret % 10);
			key = (key * 19 + (curr - prev) + 9) % Magic;
			if (i >= 4 && !seen[key] && (seen[key] = true))
			{
				max = Math.Max(max, sums[key] += curr);
			}
		}

		return max;
	}
}
