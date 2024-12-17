namespace advent_of_code_2024;

public class Day17
{
	private static List<long> operands = [];
	private static long regA = 0L;

	public static void SolvePart1()
	{
		ParseInput();
		List<long> output = RunProgram(regA);
		Console.WriteLine($"[Part 1] Answer: {string.Join(',', output)}");
	}

	public static void SolvePart2()
	{
		ParseInput();
		List<long> output = DFS(0L, 0);
		Console.WriteLine($"[Part 2] Answer: {output.Min()}");
	}

	private static void ParseInput()
	{
		List<string> input = [.. File.ReadAllLines("Day17\\input.txt")];
		regA = long.Parse(input[0].Split(' ')[^1]);
		operands = [.. input[4].Split(' ')[1].Split(',').Select(long.Parse)];
	}

	private static List<long> RunProgram(long regA)
	{
		long regB = 0;
		long regC = 0;
		int pc = 0;
		List<long> output = [];

		while (pc < operands.Count)
		{
			long combo = (operands[pc + 1]) switch
			{
				0 => 0,
				1 => 1,
				2 => 2,
				3 => 3,
				4 => regA,
				5 => regB,
				6 => regC,
				_ => long.MinValue
			};

			long literal = operands[pc + 1];
			bool jumped = false;

			switch (operands[pc])
			{
				case 0:
					regA = (long)(regA / Math.Pow(2, combo));
					break;
				case 1:
					regB ^= literal;
					break;
				case 2:
					regB = combo % 8;
					break;
				case 3:
					if (regA != 0)
					{
						pc = (int)literal;
						jumped = true;
					}
					break;
				case 4:
					regB ^= regC;
					break;
				case 5:
					output.Add(combo % 8);
					break;
				case 6:
					regB = (long)(regA / Math.Pow(2, combo));
					break;
				case 7:
					regC = (long)(regA / Math.Pow(2, combo));
					break;
				default:
					break;
			}

			if (!jumped)
			{
				pc += 2;
			}

			if (output.Count > operands.Count)
			{
				break;
			}
		}

		return output;
	}

	private static List<long> DFS(long currVal, int depth)
	{
		List<long> result = [];
		if (depth > operands.Count)
		{
			return result;
		}

		long tmp = currVal << 3;
		for (int i = 0; i < 8; i++)
		{
			List<long> tmpResult = RunProgram(tmp + i);
			if (tmpResult.SequenceEqual(operands.TakeLast(depth + 1)))
			{
				if (depth + 1 == operands.Count)
				{
					result.Add(tmp + i);
				}
				result.AddRange(DFS(tmp + i, depth + 1));
			}
		}

		return result;
	}
}
