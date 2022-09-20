namespace NiTiS.NBT;

public sealed class LongTag : Tag
{
	public readonly long Int64;
	public const byte ID = 4;
	public override object Value => Int64;
	public override byte TagID => ID;
	public LongTag(string name, long value) : base(name)
	{
		Int64 = value;
	}
}
