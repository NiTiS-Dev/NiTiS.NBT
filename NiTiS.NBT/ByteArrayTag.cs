namespace NiTiS.NBT;

public sealed class ByteArrayTag : Tag
{
	public readonly byte[] Array;
	public const byte ID = 7;
	public override object Value => Array;
	public override byte TagID => ID;
	public ByteArrayTag(string name, byte[] value) : base(name)
	{
		Array = value;
	}
}

