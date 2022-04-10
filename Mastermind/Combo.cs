using System;
using System.Collections;
using System.Diagnostics;

[DebuggerDisplay("Combo: {ToString()}")]
public class Combo : IList<Peg>
{
	public override string ToString()
	{
		return string.Join(' ', this.Select(x=>x.DisplayCharacter));
	}

	public int Count => pegs.Length;
	public bool IsReadOnly => true;

	public Peg this[int i]
	{
		get => pegs[i];
		set => pegs[i] = value;
	}

	public int Length => pegs.Length;

	private Peg[] pegs { get; }

	public Combo(Combo combo)
		: this(combo.pegs)
	{
	}

	public Combo(Peg[] pegs)
	{
		this.pegs = pegs;
	}

	public void Add(Peg item)
	{
		throw new NotSupportedException();
	}

	public void Clear()
	{
		throw new NotSupportedException();
	}

	public bool Contains(Peg item)
	{
		return pegs.Contains(item);
	}

	public void CopyTo(Peg[] array, int arrayIndex)
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

	public IEnumerator<Peg> GetEnumerator()
	{
		return ((IEnumerable<Peg>)pegs).GetEnumerator();
	}

	public override int GetHashCode()
	{
		return pegs.GetHashCode();
	}

	public int IndexOf(Peg item)
	{
		return Array.IndexOf(pegs, item);
	}

	public void Insert(int index, Peg item)
	{
		throw new NotSupportedException();
	}

	public bool Remove(Peg item)
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

	public static Combo Random(IList<Peg> validPegs, int keyLength)
	{
		Peg[] pegs = new Peg[keyLength];
		for (int i = 0; i < keyLength; i++)
		{
			pegs[i] = (validPegs[new Random().Next(0, validPegs.Count - 1)]);
		}

		return new Combo(pegs);
	}
}
