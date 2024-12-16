namespace advent_of_code_2024;

public class Day16
{
	private record Direction(int Row, int Col)
	{
		public Direction TurnLeft() => new(-Col, Row);
		public Direction TurnRight() => new(Col, -Row);
	}

	private record Position(int Row, int Col)
	{
		public Position Move(Direction dir) => new(Row + dir.Row, Col + dir.Col);
	}

	private record MazePath(Position Pos, Direction Dir, HashSet<Position> Moves);

	public static void SolvePart1()
	{
		string[] input = File.ReadAllLines("Day16\\input.txt");
		var (start, end, maze) = ParseMaze(input);

		Dictionary<(Position, Direction), int> seen = [];
		PriorityQueue<MazePath, int> queue = new();
		MazePath startPath = new(start, new Direction(0, 1), []);
		queue.Enqueue(startPath, 0);
		int shortestLen = 0;

		while (queue.TryDequeue(out MazePath? path, out int length))
		{
			if (path.Pos == end)
			{
				if (shortestLen == 0)
				{
					shortestLen = length;
					break;
				}

				shortestLen = length;
			}

			if (seen.GetValueOrDefault((path.Pos, path.Dir), int.MaxValue) < length)
			{
				continue;
			}

			seen[(path.Pos, path.Dir)] = length;

			Position next = path.Pos.Move(path.Dir);
			if (maze[next.Row, next.Col])
			{
				HashSet<Position> nextMoves = new(path.Moves) { next };
				queue.Enqueue(
					new MazePath(next, path.Dir, nextMoves),
					length + 1);
			}

			Direction left = path.Dir.TurnLeft();
			next = path.Pos.Move(left);
			if (maze[next.Row, next.Col])
			{
				HashSet<Position> nextMoves = new(path.Moves) { next };
				queue.Enqueue(
					new MazePath(next, left, nextMoves),
					length + 1001);
			}

			Direction right = path.Dir.TurnRight();
			next = path.Pos.Move(right);
			if (maze[next.Row, next.Col])
			{
				HashSet<Position> nextMoves = new(path.Moves) { next };
				queue.Enqueue(
					new MazePath(next, right, nextMoves),
					length + 1001);
			}
		}

		Console.WriteLine($"[Part 1] Answer: {shortestLen}");
	}

	public static void SolvePart2()
	{
		string[] input = File.ReadAllLines("Day16\\input.txt");
		var (start, end, maze) = ParseMaze(input);

		Dictionary<(Position, Direction), int> seen = [];
		PriorityQueue<MazePath, int> queue = new();
		MazePath startPath = new(start, new Direction(0, 1), []);
		queue.Enqueue(startPath, 0);
		HashSet<Position> shortestPaths = [start];

		while (queue.TryDequeue(out MazePath? path, out int length))
		{
			if (path.Pos == end)
			{
				shortestPaths.UnionWith(path.Moves);
			}

			if (seen.GetValueOrDefault((path.Pos, path.Dir), int.MaxValue) < length)
			{
				continue;
			}

			seen[(path.Pos, path.Dir)] = length;

			Position next = path.Pos.Move(path.Dir);
			if (maze[next.Row, next.Col])
			{
				HashSet<Position> nextMoves = new(path.Moves) { next };
				queue.Enqueue(
					new MazePath(next, path.Dir, nextMoves),
					length + 1);
			}

			Direction left = path.Dir.TurnLeft();
			next = path.Pos.Move(left);
			if (maze[next.Row, next.Col])
			{
				HashSet<Position> nextMoves = new(path.Moves) { next };
				queue.Enqueue(
					new MazePath(next, left, nextMoves),
					length + 1001);
			}

			Direction right = path.Dir.TurnRight();
			next = path.Pos.Move(right);
			if (maze[next.Row, next.Col])
			{
				HashSet<Position> nextMoves = new(path.Moves) { next };
				queue.Enqueue(
					new MazePath(next, right, nextMoves),
					length + 1001);
			}
		}

		Console.WriteLine($"[Part 2] Answer: {shortestPaths.Count}");
	}

	private static (Position s, Position e, bool[,] m) ParseMaze(string[] input)
	{
		Position start = new(0, 0);
		Position end = new(0, 0);
		bool[,] maze = new bool[input.Length, input[0].Length];

		for (int row = 0; row < input.Length; row++)
		{
			for (int col = 0; col < input[0].Length; col++)
			{
				maze[row, col] = true;
				switch (input[row][col])
				{
					case 'S':
						start = new Position(row, col);
						break;
					case 'E':
						end = new Position(row, col);
						break;
					case '#':
						maze[row, col] = false;
						break;
				}
			}
		}

		return (start, end, maze);
	}
}
