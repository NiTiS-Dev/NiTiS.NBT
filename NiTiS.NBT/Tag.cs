using System;

namespace NiTiS.NBT;

public abstract class Tag
{
	public readonly string Name;
	public abstract object? Value { get; }
	public abstract byte TagID { get; }
	public Tag(string name)
	{
		Name = name;
	}
	public override string ToString()
		=> GetType().Name;
}
