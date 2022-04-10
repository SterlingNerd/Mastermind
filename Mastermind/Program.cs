using System;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class Program
{
	public static int KeyLength = 4;
	public static int MaxGuesses = 10;

	public static readonly PegCollection pegs = PegCollection.Default;

	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public static void Main(int? maxGuesses, int? keyLength)
	{
		MaxGuesses = maxGuesses ?? MaxGuesses;
		KeyLength = keyLength ?? KeyLength;

		Console.Clear();

		Combo key = Combo.Random(pegs, KeyLength);

		Guess? guess = null;
		int guesses = 0;
		while (guess != key && guesses < MaxGuesses)
		{
			guess = GetGuess(key);
			WriteGuess(guess, guesses + 1);
			guesses++;
		}

		Console.Write(new string(' ', MaxGuesses.ToString().Length + 8));
		Console.WriteLine("+" + new string('-', KeyLength * 2 + 1) + "+");
		Console.Write("Key:".PadLeft(MaxGuesses.ToString().Length + 7) + "   ");
		WriteCombo(key);
		Console.WriteLine();
		Console.WriteLine($"You {(guess == key ? "WIN!" : "LOSE!")}");
	}

	private static Guess GetGuess(Combo key)
	{
		WritePrompt();

		int currentIndex = 0;

		Peg[] guess = new Peg[KeyLength];

		while (currentIndex < KeyLength)
		{
			ConsoleKeyInfo input = Console.ReadKey();

			if (input.Key == ConsoleKey.Q)
			{
				Environment.Exit(0);
			}
			else if (input.Key == ConsoleKey.Backspace && currentIndex > 0)
			{
				currentIndex--;
				Console.CursorLeft -= 2;
				Console.Write("  ");
				Console.CursorLeft -= 1;
			}
			else if (pegs.ContainsKey(input.Key))
			{
				guess[currentIndex] = pegs.GetPeg(input.Key);
				Console.CursorLeft--;
				WritePeg(guess[currentIndex]);
				SetDefaultColor();
				Console.Write(' ');
				currentIndex++;
			}
			else
			{
				Console.CursorLeft -= 1;
				Console.Write(" ");
				Console.CursorLeft -= 1;
			}
		}

		ClearLine();

		return new Guess(guess, key);
	}

	private static void ClearLine()
	{
		SetDefaultColor();
		Console.SetCursorPosition(0, Console.CursorTop);
		Console.Write(new string(' ', Console.BufferWidth - 1));
		Console.SetCursorPosition(0, Console.CursorTop);
	}

	private static void SetDefaultColor()
	{
		Console.ForegroundColor = ConsoleColor.White;
		Console.BackgroundColor = ConsoleColor.Black;
	}

	private static void WriteCombo(Combo combo)
	{
		foreach (Peg peg in combo)
		{
			WritePeg(peg);
			SetDefaultColor();
			Console.Write(' ');
		}
	}

	private static void WriteGuess(Guess guess, int guessNumber)
	{
		SetDefaultColor();
		int guessPad = MaxGuesses.ToString().Length + 1;
		string guessStr = "#" + guessNumber;
		Console.Write($"Guess {guessStr.PadLeft(guessPad)} | ");

		WriteCombo(guess);

		SetDefaultColor();
		Console.Write("| ");

		for (int i = 0; i < guess.NumberCorrectPosition; i++)
		{
			Console.Write("X ");
		}

		for (int i = 0; i < guess.NumberCorrectColor - guess.NumberCorrectPosition; i++)
		{
			Console.Write("O ");
		}

		Console.WriteLine();
	}

	private static void WritePeg(Peg peg)
	{
		SetColor(peg);
		Console.Write(peg.DisplayCharacter);
	}

	private static void SetColor(Peg peg)
	{
		Console.ForegroundColor = peg.ForegroundColor;
		Console.BackgroundColor = peg.BackgroundColor;
	}

	private static void WritePrompt()
	{
		SetDefaultColor();
		Console.Write("Enter your guess: (");
		foreach (Peg peg in pegs)
		{
			WritePeg(peg);
			SetDefaultColor();
			Console.Write(' ');
		}

		Console.Write("): ");
	}
}
