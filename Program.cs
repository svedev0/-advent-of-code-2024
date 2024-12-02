using System.Diagnostics;

namespace advent_of_code_2024;

public class Program
{
	private static readonly int DAY = 2;

	public static void Main(string[] _)
	{
		long startTime = Stopwatch.GetTimestamp();
		Welcome();

		switch (DAY)
		{
			case 1:
				Day01.SolvePart1();
				Day01.SolvePart2();
				break;
			case 2:
				Day02.SolvePart1();
				Day02.SolvePart2();
				break;
			default:
				throw new Exception("Invalid day");
		}

		TimeSpan elapsed = Stopwatch.GetElapsedTime(startTime);
		int elapsedMs = (int)Math.Floor(elapsed.TotalMilliseconds);
		Console.WriteLine($"The program finished in {elapsedMs} ms");
	}

	private static void Welcome()
	{
		string messsage = $"""
			===== Advent of Code 2024 =====

			Day {DAY}:

			""";
		Console.WriteLine(messsage);
	}
}
