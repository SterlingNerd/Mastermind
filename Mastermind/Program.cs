using System;

public static class Program
{
	public const int KeyLength = 4;
	public const int MaxGuesses = 10;

	public static readonly Dictionary<ConsoleKey, PegColor> ColorKeys = new()
	{
		{ ConsoleKey.R, PegColor.Red },
		{ ConsoleKey.G, PegColor.Green },
		{ ConsoleKey.B, PegColor.Blue },
		{ ConsoleKey.Y, PegColor.Yellow },
		{ ConsoleKey.W, PegColor.White },
		{ ConsoleKey.K, PegColor.Black }
	};

	public static readonly Random rand = new();

	public static void Main()
	{
		Console.Clear();

		Combo key = Combo.Random();
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

		PegColor[] pegs = new PegColor[KeyLength];
		int currentIndex = 0;

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
			else if (ColorKeys.ContainsKey(input.Key))
			{
				pegs[currentIndex] = ColorKeys[input.Key];
				Console.CursorLeft--;
				WritePeg(pegs[currentIndex]);
				SetDefaultColor();
				Console.Write(' ');
				currentIndex++;
			}
		}

		ClearLine();

		return new Guess(pegs, key);
	}

	private static void ClearLine()
	{
		SetDefaultColor();
		Console.SetCursorPosition(0, Console.CursorTop);
		Console.Write(new string(' ', Console.BufferWidth - 1));
		Console.SetCursorPosition(0, Console.CursorTop);
	}

	private static void SetColor(PegColor? color = null)
	{
		switch (color)
		{
			case null:
				// case PegColor.None:
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.BackgroundColor = ConsoleColor.Black;
				break;
			case PegColor.Red:
				Console.ForegroundColor = ConsoleColor.Red;
				Console.BackgroundColor = ConsoleColor.Black;
				break;
			case PegColor.Green:
				Console.ForegroundColor = ConsoleColor.Green;
				Console.BackgroundColor = ConsoleColor.Black;
				break;
			case PegColor.Blue:
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.BackgroundColor = ConsoleColor.Black;
				break;
			case PegColor.Yellow:
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.BackgroundColor = ConsoleColor.Black;
				break;
			case PegColor.Black:
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.DarkGray;
				break;
			case PegColor.White:
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(color), color, null);
		}
	}

	private static void SetDefaultColor()
	{
		SetColor();
	}

	private static void WriteCombo(Combo combo)
	{
		foreach (PegColor peg in combo)
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

	private static void WritePeg(PegColor peg)
	{
		SetColor(peg);
		switch (peg)
		{
			// case PegColor.None:
			// 	Console.Write("_");
			// 	break;
			case PegColor.Red:
				Console.Write("R");
				break;
			case PegColor.Green:
				Console.Write("G");
				break;
			case PegColor.Blue:
				Console.Write("B");
				break;
			case PegColor.Yellow:
				Console.Write("Y");
				break;
			case PegColor.Black:
				Console.Write("K");
				break;
			case PegColor.White:
				Console.Write("W");
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(peg), peg, null);
		}
	}

	private static void WritePrompt()
	{
		SetDefaultColor();
		Console.Write("Enter your guess: (");
		foreach (PegColor color in Enum.GetValues<PegColor>())
		{
			WritePeg(color);
			SetDefaultColor();
			Console.Write(' ');
		}

		Console.Write("): ");
	}
}
