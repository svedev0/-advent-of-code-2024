namespace advent_of_code_2024;

public class Day13
{
	private record ClawMachine(long Ax, long Ay, long Bx, long By, int Px, int Py);

	public static void SolvePart1()
	{
		string input = File.ReadAllText("Day13\\input.txt");
		string[] machineRecords = input.Split("\r\n\r\n");
		ClawMachine[] machines = [.. machineRecords.Select(ParseMachine)];

		Console.WriteLine($"[Part 1] Answer: {PlayMachines(machines)}");
	}

	public static void SolvePart2()
	{
		string input = File.ReadAllText("Day13\\input.txt");
		string[] machineRecords = input.Split("\r\n\r\n");
		ClawMachine[] machines = [.. machineRecords.Select(ParseMachine)];

		long correction = 10_000_000_000_000L;
		Console.WriteLine($"[Part 2] Answer: {PlayMachines(machines, correction)}");
	}

	private static ClawMachine ParseMachine(string record)
	{
		string[] lines = record.Split("\r\n");

		string[] buttonA = lines[0][10..].Split(", ");
		int ax = int.Parse(buttonA[0][2..]);
		int ay = int.Parse(buttonA[1][2..]);

		string[] buttonB = lines[1][10..].Split(", ");
		int bx = int.Parse(buttonB[0][2..]);
		int by = int.Parse(buttonB[1][2..]);

		string[] prize = lines[2][7..].Split(", ");
		int pX = int.Parse(prize[0][2..]);
		int pY = int.Parse(prize[1][2..]);

		return new(ax, ay, bx, by, pX, pY);
	}

	private static long PlayMachines(ClawMachine[] machines, long correction = 0L)
	{
		long result = 0L;

		foreach (ClawMachine m in machines)
		{
			long d = m.Ax * m.By - m.Ay * m.Bx;
			long dx = (m.Px + correction) * m.By - (m.Py + correction) * m.Bx;
			long dy = m.Ax * (m.Py + correction) - m.Ay * (m.Px + correction);

			if (dx % d != 0 || dy % d != 0)
			{
				continue;
			}

			long a = dx / d;
			long b = dy / d;

			if (correction == 0L && (a > 100 || b > 100))
			{
				continue;
			}

			result += a * 3 + b;
		}

		return result;
	}
}
