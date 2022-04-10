using System;

public class Guess : Combo
{
	public bool IsCorrect => NumberCorrectPosition == Length;


	public int NumberCorrectColor
	{
		get
		{
			int count = 0;
			foreach (Peg peg in key.Distinct())
			{
				int keyCount = key.Count(x => x == peg);
				int guessCount = this.Count(x => x == peg);
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

	public Guess(Peg[] guess, Combo key)
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
