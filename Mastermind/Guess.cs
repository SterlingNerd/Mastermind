using System;

public class Guess : Combo
{
	public bool IsCorrect => NumberCorrectPosition == Length;

	public int NumberCorrectColor
	{
		get
		{
			int count = 0;
			foreach (PegColor color in Enum.GetValues<PegColor>())
			{
				int keyCount = key.Count(x => x == color);
				int guessCount = this.Count(x => x == color);
				count += Math.Min(keyCount, guessCount);
			}

			return count;
		}
	}

	public int NumberCorrectPosition
	{
		get
		{
			int count = 0;
			for (int i = 0; i < key.Length; i++)
			{
				if (key[i] == this[i])
				{
					count++;
				}
			}

			return count;
		}
	}

	private Combo key { get; }

	public Guess(PegColor[] guess, Combo key)
		: base(guess)
	{
		this.key = key;
	}

	public Guess(Combo guess, Combo key)
		: base(guess)
	{
		this.key = key;
	}
}
