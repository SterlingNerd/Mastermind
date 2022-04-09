using System;
using System.Collections;

public class Combo : IList<PegColor>
{
	public int Count => pegs.Length;
	public bool IsReadOnly => true;

	public PegColor this[int i]
	{
		get => pegs[i];
		set => pegs[i] = value;
	}

	public int Length => pegs.Length;

	private PegColor[] pegs { get; }

	private Combo() : this(new PegColor[Program.KeyLength])
	{
	}

	public Combo(Combo combo)
		: this(combo.pegs)
	{
	}

	public Combo(PegColor[] pegs)
	{
		this.pegs = pegs;
	}

	public void Add(PegColor item)
	{
		throw new NotSupportedException();
	}

	public void Clear()
	{
		throw new NotSupportedException();
	}

	public bool Contains(PegColor item)
	{
		return pegs.Contains(item);
	}

	public void CopyTo(PegColor[] array, int arrayIndex)
	{
		throw new NotSupportedException();
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj))
		{
			return false;
		}

		if (ReferenceEquals(this, obj))
		{
			return true;
		}

		if (!obj.GetType().IsAssignableFrom(typeof(Combo)))
		{
			return false;
		}

		return Equals((Combo)obj);
	}

	public IEnumerator<PegColor> GetEnumerator()
	{
		return ((IEnumerable<PegColor>)pegs).GetEnumerator();
	}

	public override int GetHashCode()
	{
		return pegs.GetHashCode();
	}

	public int IndexOf(PegColor item)
	{
		return Array.IndexOf(pegs, item);
	}

	public void Insert(int index, PegColor item)
	{
		throw new NotSupportedException();
	}

	public bool Remove(PegColor item)
	{
		throw new NotSupportedException();
	}

	public void RemoveAt(int index)
	{
		throw new NotSupportedException();
	}

	protected bool Equals(Combo? other)
	{
		if (other == null)
		{
			return false;
		}

		if (pegs.Length != other.pegs.Length)
		{
			return false;
		}

		for (int i = 0; i < pegs.Length; i++)
		{
			if (pegs[i] != other.pegs[i])
			{
				return false;
			}
		}

		return true;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public static bool operator ==(Combo? left, Combo? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(Combo? left, Combo? right)
	{
		return !Equals(left, right);
	}

	public static Combo Random()
	{
		Combo combo = new();

		for (int i = 0; i < Program.KeyLength; i++)
		{
			combo[i] = (PegColor)Program.rand.Next(Enum.GetValues<PegColor>().Cast<int>().Min(), Enum.GetValues<PegColor>().Cast<int>().Max());
		}

		return combo;
	}
}
