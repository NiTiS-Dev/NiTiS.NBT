namespace NiTiS.NBT;

public sealed class ByteTag : Tag
{
	public readonly sbyte SByte;
	public const byte ID = 1;
	public override object Value => SByte;
	public override byte TagID => ID;
	public ByteTag(string name, sbyte value) : base(name)
	{
		SByte = value;
	}
}
