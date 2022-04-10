using System;
using System.Collections.ObjectModel;

public class PegCollection : ReadOnlyCollection<Peg>
{
	public static readonly PegCollection Default = new(
		new Peg[]
		{
			new(ConsoleKey.R, 'R', ConsoleColor.Red, ConsoleColor.Black),
			new(ConsoleKey.G, 'G', ConsoleColor.Green, ConsoleColor.Black),
			new(ConsoleKey.B, 'B', ConsoleColor.Blue, ConsoleColor.Black),
			new(ConsoleKey.Y, 'Y', ConsoleColor.Yellow, ConsoleColor.Black),
			new(ConsoleKey.W, 'W', ConsoleColor.White, ConsoleColor.Black),
			new(ConsoleKey.K, 'K', ConsoleColor.Black, ConsoleColor.Gray)
		}
	);

	public PegCollection(IList<Peg> list) : base(list)
	{
	}

	public bool ContainsKey(ConsoleKey key)
	{
		return this.Any(x => x.Key == key);
	}

	public Peg GetPeg(ConsoleKey inputKey)
	{
		return this.Single(x => x.Key == inputKey);
	}
}
