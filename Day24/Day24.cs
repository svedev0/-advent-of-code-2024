namespace advent_of_code_2024;

public class Day24
{
	private record Gate(Op Type, string Input1, string Input2, string Output);
	private enum Op { AND, OR, XOR }

	public static void SolvePart1()
	{
		(Dictionary<string, bool> initValues, List<Gate> gates) = ParseInput();
		Dictionary<string, bool> wireValues = CalcWireValues(initValues, gates);

		long result = CalcZWiresValue(wireValues);
		Console.WriteLine($"[Part 1] Answer: {result}");
	}

	public static void SolvePart2()
	{
		(Dictionary<string, bool> _, List<Gate> gates) = ParseInput();

		HashSet<Gate> faultyGates = [];
		Gate lastZGate = gates
			.Where(g => g.Output.StartsWith('z'))
			.OrderByDescending(g => g.Output)
			.First();

		foreach (Gate gate in gates)
		{
			bool isFaulty = false;
			string output = gate.Output;

			if (output.StartsWith('z') && output != lastZGate.Output)
			{
				isFaulty = gate.Type != Op.XOR;
			}
			else if (!output.StartsWith('z') &&
				!IsWire(gate.Input1) &&
				!IsWire(gate.Input2))
			{
				isFaulty = gate.Type == Op.XOR;
			}
			else if (IsWire(gate.Input1) &&
				IsWire(gate.Input2) &&
				!AreFirstBit(gate.Input1, gate.Input2))
			{
				Op nextOp = gate.Type == Op.XOR ? Op.XOR : Op.OR;

				bool feedsIntoNextGate = gates.Any(other =>
					other != gate &&
					(other.Input1 == output || other.Input2 == output) &&
					other.Type == nextOp);

				isFaulty = !feedsIntoNextGate;
			}

			if (isFaulty)
			{
				faultyGates.Add(gate);
			}
		}

		string[] result = [.. faultyGates.Select(g => g.Output).OrderBy(w => w)];
		Console.WriteLine($"[Part 2] Answer: {string.Join(",", result)}");
	}

	private static (Dictionary<string, bool>, List<Gate>) ParseInput()
	{
		Dictionary<string, bool> initialValues = [];
		List<Gate> gates = [];
		string[] lines = File.ReadAllLines("Day24\\input.txt");
		bool parsingGates = false;

		foreach (string line in lines)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				parsingGates = true;
				continue;
			}

			if (!parsingGates)
			{
				string[] parts = line.Split(':');
				string wire = parts[0].Trim();
				int value = int.Parse(parts[1].Trim());
				initialValues[wire] = value == 1;
			}
			else
			{
				string[] parts = line.Split("->");
				string outputWire = parts[1].Trim();
				string[] inputs = parts[0].Trim().Split(' ');

				Op type = inputs switch
				{
					_ when inputs.Contains("AND") => Op.AND,
					_ when inputs.Contains("OR") => Op.OR,
					_ => Op.XOR
				};
				gates.Add(new Gate(type, inputs[0], inputs[2], outputWire));
			}
		}

		return (initialValues, gates);
	}

	private static bool EvalGate(Op type, bool input1, bool input2)
	{
		return type switch
		{
			Op.AND => input1 && input2,
			Op.OR => input1 || input2,
			Op.XOR => input1 ^ input2,
			_ => throw new ArgumentException("Invalid gate type")
		};
	}

	private static bool IsWire(string wire)
	{
		return wire.StartsWith('x') || wire.StartsWith('y');
	}

	private static bool AreFirstBit(string input1, string input2)
	{
		return input1.EndsWith("00") && input2.EndsWith("00");
	}

	private static Dictionary<string, bool> CalcWireValues(
		Dictionary<string, bool> initialValues, List<Gate> gates)
	{
		Dictionary<string, bool> wireValues = new(initialValues);
		HashSet<Gate> doneGates = [];

		while (doneGates.Count < gates.Count)
		{
			foreach (var gate in gates)
			{
				if (doneGates.Contains(gate))
				{
					continue;
				}

				if (!wireValues.ContainsKey(gate.Input1) ||
					!wireValues.ContainsKey(gate.Input2))
				{
					continue;
				}

				wireValues[gate.Output] = EvalGate(
					gate.Type,
					wireValues[gate.Input1],
					wireValues[gate.Input2]);
				doneGates.Add(gate);
			}
		}

		return wireValues;
	}

	private static long CalcZWiresValue(Dictionary<string, bool> wireValues)
	{
		List<string> zWires = [.. wireValues.Keys
			.Where(k => k.StartsWith('z'))
			.OrderByDescending(k => int.Parse(k[1..]))];

		long result = 0;
		foreach (string wire in zWires)
		{
			if (wireValues[wire])
			{
				result = (result << 1) | 1;
			}
			else
			{
				result = (result << 1) | 0;
			}
		}

		return result;
	}
}
