using System.Diagnostics;

namespace advent_of_code_2024;

// dotnet run -c Release -- -day=18

public class Program
{
	public static void Main(string[] args)
	{
		int day = ValidateArgs(args);

		Console.WriteLine($"""
			===== Advent of Code 2024 =====

			Day {day:D2}:

			""");

		long startTime = Stopwatch.GetTimestamp();

		switch (day)
		{
			case 1:
				Day01.SolvePart1();
				Day01.SolvePart2();
				break;
			case 2:
				Day02.SolvePart1();
				Day02.SolvePart2();
				break;
			case 3:
				Day03.SolvePart1();
				Day03.SolvePart2();
				break;
			case 4:
				Day04.SolvePart1();
				Day04.SolvePart2();
				break;
			case 5:
				Day05.SolvePart1();
				Day05.SolvePart2();
				break;
			case 6:
				Day06.SolvePart1();
				Day06.SolvePart2();
				break;
			case 7:
				Day07.SolvePart1();
				Day07.SolvePart2();
				break;
			case 8:
				Day08.SolvePart1();
				Day08.SolvePart2();
				break;
			case 9:
				Day09.SolvePart1();
				Day09.SolvePart2();
				break;
			case 10:
				Day10.SolvePart1();
				Day10.SolvePart2();
				break;
			case 11:
				Day11.SolvePart1();
				Day11.SolvePart2();
				break;
			case 12:
				Day12.SolvePart1();
				Day12.SolvePart2();
				break;
			case 13:
				Day13.SolvePart1();
				Day13.SolvePart2();
				break;
			case 14:
				Day14.SolvePart1();
				Day14.SolvePart2();
				break;
			case 15:
				Day15.SolvePart1();
				Day15.SolvePart2();
				break;
			case 16:
				Day16.SolvePart1();
				Day16.SolvePart2();
				break;
			case 17:
				Day17.SolvePart1();
				Day17.SolvePart2();
				break;
			case 18:
				Day18.SolvePart1();
				Day18.SolvePart2();
				break;
			default:
				throw new Exception("Invalid day. Day not found");
		}

		TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);
		int elapsedMs = (int)Math.Floor(elapsed.TotalMilliseconds);
		Console.WriteLine($"The program finished in {elapsedMs} ms");
	}

	private static int ValidateArgs(string[] args)
	{
		if (args.Length != 1 || !args.Any(a => a.Contains("-day=")))
		{
			throw new Exception("Missing argument 'day'.\nExample: -day=4");
		}

		string dayArg = args[0].Replace("-day=", string.Empty);
		if (!int.TryParse(dayArg, out int day))
		{
			throw new Exception("Invalid value in argument 'day'");
		}

		if (day is < 1 or > 25)
		{
			throw new Exception("Invalid value in argument 'day'");
		}

		return day;
	}
}
