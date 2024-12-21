namespace advent_of_code_2024;

public class Day21
{
	public static void SolvePart1()
	{
		List<string> input = [.. File.ReadAllLines("Day21\\input.txt")];
		long result = 0;

		string[] numKeypad = ["789", "456", "123", " 0A"];
		string[] dirKeypad = [" ^A", "<v>"];
		string numKeys = "0123456789A";
		string dirKeys = "<^>vA";

		Dictionary<(char, char), string[]> numMoves = GetMoves(numKeys, numKeypad);
		Dictionary<(char, char), string[]> dirMoves = GetMoves(dirKeys, dirKeypad);
		Dictionary<(char, char), string[]> allMoves = numMoves
			.Where((k) => !dirMoves.ContainsKey(k.Key))
			.Union(dirMoves)
			.ToDictionary();

		Dictionary<(int level, string key), long> resCache1 = [];
		Dictionary<(int level, string key), long> resCache2 = [];

		foreach (string line in input)
		{
			long ShortestMoves(string curr, int lvl, int stopAt,
				Dictionary<(int, string), long> resCache)
			{
				if (curr == "")
				{
					return 0;
				}

				if (resCache.TryGetValue((lvl, curr), out long cachedRes))
				{
					return cachedRes;
				}

				if (lvl == stopAt)
				{
					int res = GetSequences(curr, allMoves).Select(a => a.Length).Min();
					resCache.Add((lvl, curr), res);
					return res;
				}

				int firstA = curr.IndexOf('A');
				string part1 = curr[..(firstA + 1)];
				string part2 = curr[(firstA + 1)..];
				long shortest = -1;
				List<string> possibilities = GetSequences(part1, allMoves);

				foreach (string seq in possibilities)
				{
					long count = ShortestMoves(seq, lvl + 1, stopAt, resCache);
					if (shortest > count || shortest == -1)
					{
						shortest = count;
					}
				}

				if (part2 != "")
				{
					shortest += ShortestMoves(part2, lvl, stopAt, resCache);
				}

				resCache.Add((lvl, curr), shortest);
				return shortest;
			}

			int numPart = int.Parse(line[..3]);
			long la = ShortestMoves(line, 0, 2, resCache1);
			result += la * numPart;
		}

		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		List<string> input = [.. File.ReadAllLines("Day21\\input.txt")];
		long result = 0;

		string[] numKeypad = ["789", "456", "123", " 0A"];
		string[] dirKeypad = [" ^A", "<v>"];
		string numKeys = "0123456789A";
		string dirKeys = "<^>vA";

		Dictionary<(char, char), string[]> numMoves = GetMoves(numKeys, numKeypad);
		Dictionary<(char, char), string[]> dirMoves = GetMoves(dirKeys, dirKeypad);
		Dictionary<(char, char), string[]> allMoves = numMoves
			.Where((k) => !dirMoves.ContainsKey(k.Key))
			.Union(dirMoves)
			.ToDictionary();

		Dictionary<(int level, string key), long> resCache1 = [];
		Dictionary<(int level, string key), long> resCache2 = [];

		foreach (string line in input)
		{
			long ShortestMoves(string curr, int lvl, int stopAt,
				Dictionary<(int, string), long> resCache)
			{
				if (curr == "")
				{
					return 0;
				}

				if (resCache.TryGetValue((lvl, curr), out long cachedRes))
				{
					return cachedRes;
				}

				if (lvl == stopAt)
				{
					int res = GetSequences(curr, allMoves).Select(a => a.Length).Min();
					resCache.Add((lvl, curr), res);
					return res;
				}

				int firstA = curr.IndexOf('A');
				string part1 = curr[..(firstA + 1)];
				string part2 = curr[(firstA + 1)..];
				long shortest = -1;
				List<string> possibilities = GetSequences(part1, allMoves);

				foreach (string seq in possibilities)
				{
					long count = ShortestMoves(seq, lvl + 1, stopAt, resCache);
					if (shortest > count || shortest == -1)
					{
						shortest = count;
					}
				}

				if (part2 != "")
				{
					shortest += ShortestMoves(part2, lvl, stopAt, resCache);
				}

				resCache.Add((lvl, curr), shortest);
				return shortest;
			}

			int numPart = int.Parse(line[..3]);
			long la = ShortestMoves(line, 0, 25, resCache2);
			result += numPart * la;
		}

		Console.WriteLine($"[Part 2] Answer: {result}");
	}

	private static (int x, int y) FindKey(char key, string[] pad)
	{
		for (int i = 0; i < pad[0].Length; i++)
		{
			for (int j = 0; j < pad.Length; j++)
			{
				if (pad[j][i] == key)
				{
					return (i, j);
				}
			}
		}

		return (-1, -1);
	}

	private static Dictionary<(char, char), string[]> GetMoves(string keys, string[] keypad)
	{
		Dictionary<(char, char), string[]> moves = [];

		for (int i = 0; i < keys.Length; i++)
		{
			char c1 = keys[i];
			(int x, int y) f1 = FindKey(c1, keypad);

			for (int j = i; j < keys.Length; j++)
			{
				char c2 = keys[j];
				Dictionary<(int, int), (int cost, HashSet<string> opts)> state = [];
				PriorityQueue<(int, int), int> queue = new();
				queue.Enqueue((f1.x, f1.y), 0);
				state.Add(f1, (0, new HashSet<string> { "" }));

				while (queue.Count > 0)
				{
					(int wx, int wy) = queue.Dequeue();
					(int cost, HashSet<string> options) = state[(wx, wy)];

					if (keypad[wy][wx] == c2)
					{
						moves.Add((c1, c2), [.. options]);
						if (c1 != c2)
						{
							HashSet<string> reverseOptions = [];
							foreach (string s1 in options)
							{
								string rev = "";
								foreach (char c in s1.Reverse())
								{
									rev += c switch
									{
										'>' => '<',
										'<' => ">",
										'^' => 'v',
										'v' => '^',
										_ => c,
									};
								}
								reverseOptions.Add(rev);
							}
							moves.Add((c2, c1), [.. reverseOptions]);
						}
						break;
					}

					void DoTask(int x, int y, char dir)
					{
						if (x < 0 || y < 0 ||
							x == keypad[0].Length || y == keypad.Length)
						{
							return;
						}

						if (keypad[wy][wx] == ' ')
						{
							return;
						}

						int newcost = cost + 1;
						bool seenbefore = state.TryGetValue((x, y), out var st1);

						if (!seenbefore)
						{
							state[(x, y)] = st1 = (newcost, []);
						}

						if (newcost == st1.cost)
						{
							foreach (string s in options)
							{
								st1.opts.Add(s + dir);
							}

							if (!seenbefore)
							{
								queue.Enqueue((x, y), newcost);
							}
						}
					}

					DoTask(wx + 1, wy, '>');
					DoTask(wx - 1, wy, '<');
					DoTask(wx, wy + 1, 'v');
					DoTask(wx, wy - 1, '^');
				}
			}
		}

		return moves;
	}

	private static List<string> GetSequences(string code, Dictionary<(char, char), string[]> moves)
	{
		List<string> sequence = [""];
		char prevKey = 'A';

		foreach (char key in code)
		{
			List<string> newSequences = [];
			string[] keypadMoves = moves[(prevKey, key)];

			foreach (string prevStrokes in sequence)
			{
				foreach (string nextStroke in keypadMoves)
				{
					newSequences.Add(prevStrokes + nextStroke + 'A');
				}
			}

			prevKey = key;
			sequence = newSequences;
		}

		return sequence;
	}
}
