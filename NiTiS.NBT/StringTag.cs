using System;

namespace NiTiS.NBT;

public sealed class StringTag : Tag
{
	public readonly string String;
	public const byte ID = 8;
	public override object Value => String;
	public override byte TagID => ID;
	public StringTag(string name, string value) : base(name)
	{
		String = value ?? throw new ArgumentNullException(nameof(value));
	}
}

