namespace advent_of_code_2024;

public class Day23
{
	private record LAN(string A, string B, string C);

	private static Dictionary<string, HashSet<string>> lanMap = [];

	public static void SolvePart1()
	{
		string[] input = File.ReadAllLines("Day23\\input.txt");
		lanMap = GetLANMap(input);
		int result = FindT(Find3Adjacent());
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		string[] input = File.ReadAllLines("Day23\\input.txt");
		lanMap = GetLANMap(input);
		string result = string.Join(",", MaxCliques());
		Console.WriteLine($"[Part 2] Answer: {result}");
	}

	private static Dictionary<string, HashSet<string>> GetLANMap(string[] input)
	{
		Dictionary<string, HashSet<string>> lanMap = [];

		foreach (string line in input)
		{
			string partA = line[..2];
			string partB = line[3..];

			if (!lanMap.ContainsKey(partA))
			{
				lanMap[partA] = [];
			}
			if (!lanMap.ContainsKey(partB))
			{
				lanMap[partB] = [];
			}

			lanMap[partA].Add(partB);
			lanMap[partB].Add(partA);
		}

		return lanMap;
	}

	private static HashSet<LAN> Find3Adjacent()
	{
		HashSet<LAN> result = [];

		foreach (string key in lanMap.Keys)
		{
			HashSet<string> value = lanMap[key];
			if (value.Count <= 1)
			{
				continue;
			}

			foreach (string key2 in value)
			{
				foreach (string key3 in value)
				{
					if (key2 == key3)
					{
						continue;
					}

					if (lanMap[key2].Contains(key3))
					{
						List<string> r = [key, key2, key3];
						r.Sort();
						result.Add(new LAN(r[0], r[1], r[2]));
					}
				}
			}
		}

		return result;
	}

	private static int FindT(HashSet<LAN> input)
	{
		return input.Count(key =>
			key.A.StartsWith('t') ||
			key.B.StartsWith('t') ||
			key.C.StartsWith('t'));
	}

	private static List<List<string>> BronKerbosch(
		List<string> currClique, List<string> todo, List<string> done)
	{
		if (todo.Count == 0 && done.Count == 0)
		{
			return [currClique];
		}

		List<List<string>> cliques = [];

		for (int index = 0; index < todo.Count;)
		{
			string node = todo[index];
			List<string> newTodo = [.. todo.Where(n => lanMap[node].Contains(n))];
			List<string> newDone = [.. done.Where(n => lanMap[node].Contains(n))];

			cliques.AddRange(
				BronKerbosch([.. currClique, node], newTodo, newDone));

			todo.RemoveAt(index);
			done.Add(node);
		}

		return cliques;
	}

	private static string[] MaxCliques()
	{
		List<string> maxClique = [];
		List<string> allComputers = [.. lanMap.Keys];
		List<List<string>> cliques = BronKerbosch([], allComputers, []);

		foreach (List<string> clique in cliques)
		{
			if (clique.Count > maxClique.Count)
			{
				maxClique = clique;
			}
		}

		maxClique.Sort();
		return [.. maxClique];
	}
}
