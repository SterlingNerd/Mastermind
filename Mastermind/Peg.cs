using System;
using System.Diagnostics;

[DebuggerDisplay("Peg: {DisplayCharacter}")]
public class Peg
{
	public ConsoleColor BackgroundColor { get; }
	public char DisplayCharacter { get; }
	public ConsoleColor ForegroundColor { get; }
	public ConsoleKey Key { get; }

	public Peg(ConsoleKey key, char displayCharacter, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
	{
		Key = key;
		DisplayCharacter = displayCharacter;
		ForegroundColor = foregroundColor;
		BackgroundColor = backgroundColor;
	}

	public override string ToString()
	{
		return DisplayCharacter.ToString();
	}
}
