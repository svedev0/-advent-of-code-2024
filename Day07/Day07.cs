namespace advent_of_code_2024;

public class Day07
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day07\\input.txt")];
		List<long[]> lines = [.. input.Select(line => line
			.Replace(":", string.Empty)
			.Split(' ', StringSplitOptions.RemoveEmptyEntries)
			.Select(long.Parse)
			.ToArray())];

		List<long> filtered = [.. lines
			.Where(line => EvalsTo([.. line.Skip(1)], line[0]))
			.Select(line => line[0])];

		Console.WriteLine($"[Part 1] Answer: {filtered.Sum()}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day07\\input.txt")];
		List<long[]> lines = [.. input.Select(x => x
			.Replace(":", string.Empty)
			.Split(' ', StringSplitOptions.RemoveEmptyEntries)
			.Select(long.Parse)
			.ToArray())];

		List<long> filtered = [.. lines
			.Where(line => ExtendedEvalsTo([.. line.Skip(1)], line[0]))
			.Select(line => line[0])];

		Console.WriteLine($"[Part 2] Answer: {filtered.Sum()}");
	}

	private static bool EvalsTo(List<long> nums, long cur, int i = -1)
	{
		if (i == -1)
		{
			i = nums.Count - 1;
		}
		if (i == 0)
		{
			return cur == nums[0];
		}
		if (cur < 0)
		{
			return false;
		}

		long divResult = cur % nums[i] == 0 ? cur / nums[i] : -1;
		long subResult = cur - nums[i];

		return EvalsTo(nums, divResult, i - 1) ||
			EvalsTo(nums, subResult, i - 1);
	}

	private static bool ExtendedEvalsTo(List<long> nums, long cur, int i = -1)
	{
		if (i == -1)
		{
			i = nums.Count - 1;
		}
		if (i == 0)
		{
			return cur == nums[0];
		}
		if (cur < 0)
		{
			return false;
		}

		long divResult = cur % nums[i] == 0 ? cur / nums[i] : -1;
		long subResult = cur - nums[i];

		long yMag = (int)Math.Pow(10, (int)Math.Floor(Math.Log10(nums[i])) + 1);

		long unconcatResult;
		if (cur - nums[i] > 0 && (cur - nums[i]) % yMag == 0)
		{
			unconcatResult = (cur - nums[i]) / yMag;
		}
		else
		{
			unconcatResult = -1;
		}

		return ExtendedEvalsTo(nums, divResult, i - 1) ||
			ExtendedEvalsTo(nums, subResult, i - 1) ||
			ExtendedEvalsTo(nums, unconcatResult, i - 1);
	}
}
