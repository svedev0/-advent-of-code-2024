namespace advent_of_code_2024;

public class Day15
{
	private record Direction(int Row, int Col);

	private record Position(int Row, int Col)
	{
		public Position Move(Direction d) => new(Row + d.Row, Col + d.Col);
		public int Coordinates => Row * 100 + Col;
	}

	private record DoubleNode(Position Left, Position Right)
	{
		public bool Contains(Position pos)
		{
			return Left.Row <= pos.Row && pos.Row <= Right.Row &&
				Left.Col <= pos.Col && pos.Col <= Right.Col;
		}
	}

	private static readonly Dictionary<char, Direction> directions = new()
	{
		['^'] = new Direction(-1, 0),
		['v'] = new Direction(1, 0),
		['<'] = new Direction(0, -1),
		['>'] = new Direction(0, 1)
	};

	public static void SolvePart1()
	{
		string input = File.ReadAllText("Day15\\input.txt");

		List<Position> boxes = [];
		List<Position> walls = [];
		Position start = new(-1, -1);

		string[] mapInput = input.Split("\r\n\r\n")[0].Split("\r\n");
		for (int row = 0; row < mapInput.Length; row++)
		{
			for (int col = 0; col < mapInput[row].Length; col++)
			{
				char cell = mapInput[row][col];
				switch (cell)
				{
					case '#':
						walls.Add(new Position(row, col));
						break;
					case 'O':
						boxes.Add(new Position(row, col));
						break;
					case '@':
						start = new Position(row, col);
						break;
				}
			}
		}

		Position robot = start;

		string[] movesInput = input.Split("\r\n\r\n")[1].Split("\r\n");
		List<Direction> moves = [.. movesInput.SelectMany(line =>
			line.Select(move => directions[move]))];
		foreach (Direction direction in moves)
		{
			Position next = robot.Move(direction);
			if (walls.Contains(next))
			{
				continue;
			}

			if (boxes.Contains(next))
			{
				Position nextBox = next.Move(direction);

				while (boxes.Contains(nextBox))
				{
					nextBox = nextBox.Move(direction);
				}

				if (walls.Contains(nextBox))
				{
					continue;
				}

				boxes.Remove(next);
				boxes.Add(nextBox);
			}

			robot = next;
		}

		int result = boxes.Sum(b => b.Coordinates);
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		string input = File.ReadAllText("Day15\\input.txt");

		List<DoubleNode> boxes = [];
		List<DoubleNode> walls = [];
		Position start = new(-1, -1);

		string[] mapInput = input.Split("\r\n\r\n")[0].Split("\r\n");
		for (int row = 0; row < mapInput.Length; row++)
		{
			for (int col = 0; col < mapInput[row].Length; col++)
			{
				char cell = mapInput[row][col];
				switch (cell)
				{
					case '#':
						walls.Add(new DoubleNode(
							new Position(row, col * 2),
							new Position(row, col * 2 + 1)));
						break;
					case 'O':
						DoubleNode box = new(
							new Position(row, col * 2),
							new Position(row, col * 2 + 1));
						if (boxes.All(b => b.Right != box.Left))
						{
							boxes.Add(box);
						}
						break;
					case '@':
						start = new Position(row, col);
						break;
				}
			}
		}

		Position robot = start with { Col = start.Col * 2 };

		string[] movesInput = input.Split("\r\n\r\n")[1].Split("\r\n");
		List<Direction> moves = [.. movesInput.SelectMany(line =>
			line.Select(move => directions[move]))];
		foreach (Direction direction in moves)
		{
			Position next = robot.Move(direction);
			if (walls.Any(w => w.Contains(next)))
			{
				continue;
			}

			DoubleNode? nextBox = boxes.FirstOrDefault(b => b.Contains(next));
			bool canMove = true;

			if (nextBox != null)
			{
				HashSet<DoubleNode> boxesToMove = [nextBox];

				Queue<DoubleNode> queue = new();
				queue.Enqueue(nextBox);

				while (queue.Count > 0)
				{
					DoubleNode box = queue.Dequeue();
					Position nextLeft = box.Left.Move(direction);
					Position nextRight = box.Right.Move(direction);

					if (walls.Any(w => w.Contains(nextLeft)) ||
						walls.Any(w => w.Contains(nextRight)))
					{
						canMove = false;
						break;
					}

					DoubleNode? nextBoxL = boxes.FirstOrDefault(b =>
						b.Contains(nextLeft));
					if (nextBoxL != null && boxesToMove.Add(nextBoxL))
					{
						queue.Enqueue(nextBoxL);
					}

					DoubleNode? nextBoxR = boxes.FirstOrDefault(b =>
						b.Contains(nextRight));
					if (nextBoxR != null && boxesToMove.Add(nextBoxR))
					{
						queue.Enqueue(nextBoxR);
					}
				}

				if (canMove)
				{
					foreach (DoubleNode box in boxesToMove)
					{
						boxes.Remove(box);
						boxes.Add(new DoubleNode(
							box.Left.Move(direction),
							box.Right.Move(direction)));
					}
				}
			}

			if (canMove)
			{
				robot = next;
			}
		}

		int result = boxes.Sum(b => b.Left.Coordinates);
		Console.WriteLine($"[Part 2] Answer: {result}");
	}
}
