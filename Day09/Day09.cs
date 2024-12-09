namespace advent_of_code_2024;

public class Day09
{
	public static void SolvePart1()
	{
		string input = File.ReadAllText("Day09\\input.txt");
		int[] data = [.. input.Select(x => int.Parse($"{x}"))];

		int startPos = 0;
		int endPos = data.Length - 1;
		int passedBlocks = 0;
		int frontBlocks = data[startPos];
		int backBlocks = data[endPos];
		long checksum = 0;

		while (startPos < endPos)
		{
			if (startPos % 2 == 0)
			{
				checksum += CalcChecksum(passedBlocks, frontBlocks, startPos);
				passedBlocks += frontBlocks;

				startPos++;
				frontBlocks = data[startPos];
				continue;
			}

			if (backBlocks >= frontBlocks)
			{
				checksum += CalcChecksum(passedBlocks, frontBlocks, endPos);
				passedBlocks += frontBlocks;
				backBlocks -= frontBlocks;

				startPos++;
				frontBlocks = data[startPos];
			}
			else
			{
				checksum += CalcChecksum(passedBlocks, backBlocks, endPos);
				passedBlocks += backBlocks;
				frontBlocks -= backBlocks;

				endPos -= 2;
				backBlocks = data[endPos];
			}
		}

		if (endPos == startPos)
		{
			int remainingBlocks = Math.Min(frontBlocks, backBlocks);
			checksum += CalcChecksum(passedBlocks, remainingBlocks, startPos);
		}

		Console.WriteLine($"[Part 1] Answer: {checksum}");
	}

	public static void SolvePart2()
	{
		string input = File.ReadAllText("Day09\\input.txt");
		int[] data = [.. input.Select(x => int.Parse($"{x}"))];

		int startPos = 0;
		int endPos = data.Length - 1;
		int passedBlocks = 0;
		int frontBlocks = data[startPos];
		int backBlocks = data[endPos];
		long checksum = 0;

		Dictionary<int, (int startBlock, int freeBlocks)> free = [];
		Dictionary<int, int> pos = [];

		for (int i = 1; i < data.Length; i += 2)
		{
			pos[i - 1] = passedBlocks;
			passedBlocks += data[i - 1];
			int freeBlocks = data[i];
			free[i] = (passedBlocks, freeBlocks);
			passedBlocks += freeBlocks;
		}

		for (int i = data.Length - 1; i > 1; i -= 2)
		{
			int numBlocks = data[i];
			bool moved = false;

			foreach (int key in free.Keys)
			{
				(int startBlock, int space) = free[key];
				if (space >= numBlocks)
				{
					checksum += CalcChecksum(startBlock, numBlocks, i);
					space -= numBlocks;

					if (space == 0)
					{
						free.Remove(key);
					}
					else
					{
						free[key] = (startBlock + numBlocks, space);
					}

					moved = true;
					break;
				}
			}

			if (!moved)
			{
				checksum += CalcChecksum(pos[i], numBlocks, i);
			}

			if (free.TryGetValue(i - 1, out _))
			{
				free.Remove(i - 1);
			}
		}

		Console.WriteLine($"[Part 2] Answer: {checksum}");
	}

	private static long CalcChecksum(int startId, int numBlocks, int oldPos)
	{
		return (2L * startId + numBlocks - 1) * numBlocks * oldPos / 4;
	}
}
