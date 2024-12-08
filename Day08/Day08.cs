namespace advent_of_code_2024;

public class Day08
{
	private record Position(int Row, int Col)
	{
		public Position Move(int row, int col) => new(Row + row, Col + col);
		public bool OutOfBounds(int size) => OutOfBounds(0, size);
		public bool OutOfBounds(int start, int end) =>
			Row < start || Row >= end || Col < start || Col >= end;
	}

	public static void SolvePart1()
	{
		string[] input = File.ReadAllLines("Day08\\input.txt");
		Dictionary<char, List<Position>> antennas = GetAntennas(input);

		HashSet<Position> antinodes = [];

		foreach (char frequency in antennas.Keys)
		{
			List<Position> locations = antennas[frequency];

			foreach (Position a in locations)
			{
				foreach (Position b in locations)
				{
					if (a == b)
					{
						continue;
					}

					int rowDiff = a.Row - b.Row;
					int colDiff = a.Col - b.Col;

					Position antinodeA = a.Move(rowDiff, colDiff);
					if (!antinodeA.OutOfBounds(input.Length))
					{
						antinodes.Add(antinodeA);
					}

					while (!antinodeA.OutOfBounds(input.Length))
					{
						antinodeA = antinodeA.Move(rowDiff, colDiff);
					}

					Position antinodeB = b.Move(-rowDiff, -colDiff);
					if (!antinodeB.OutOfBounds(input.Length))
					{
						antinodes.Add(antinodeB);
					}

					while (!antinodeB.OutOfBounds(input.Length))
					{
						antinodeB = antinodeB.Move(-rowDiff, -colDiff);
					}
				}
			}
		}

		Console.WriteLine($"[Part 1] Answer: {antinodes.Count}");
	}

	public static void SolvePart2()
	{
		string[] input = File.ReadAllLines("Day08\\input.txt");
		Dictionary<char, List<Position>> antennas = GetAntennas(input);

		HashSet<Position> antinodes = [];

		foreach (char frequency in antennas.Keys)
		{
			List<Position> locations = antennas[frequency];

			foreach (Position a in locations)
			{
				foreach (Position b in locations)
				{
					if (a == b)
					{
						continue;
					}

					int rowDiff = a.Row - b.Row;
					int colDiff = a.Col - b.Col;

					Position antinodeA = a.Move(rowDiff, colDiff);
					while (!antinodeA.OutOfBounds(input.Length))
					{
						antinodes.Add(antinodeA);
						antinodeA = antinodeA.Move(rowDiff, colDiff);
					}

					Position antinodeB = b.Move(-rowDiff, -colDiff);
					while (!antinodeB.OutOfBounds(input.Length))
					{
						antinodes.Add(antinodeB);
						antinodeB = antinodeB.Move(-rowDiff, -colDiff);
					}

					antinodes.Add(a);
					antinodes.Add(b);
				}
			}
		}

		Console.WriteLine($"[Part 2] Answer: {antinodes.Count}");
	}

	private static Dictionary<char, List<Position>> GetAntennas(string[] input)
	{
		Dictionary<char, List<Position>> antennas = [];

		for (int row = 0; row < input.Length; row++)
		{
			for (int col = 0; col < input.Length; col++)
			{
				char cell = input[row][col];
				if (cell == '.')
				{
					continue;
				}
				if (!antennas.TryGetValue(cell, out _))
				{
					antennas[cell] = [];
				}
				antennas[cell].Add(new Position(row, col));
			}
		}

		return antennas;
	}
}
