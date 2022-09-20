namespace NiTiS.NBT;

public sealed class ShortTag : Tag
{
	public readonly short Int16;
	public const byte ID = 2;
	public override object Value => Int16;
	public override byte TagID => ID;
	public ShortTag(string name, short value) : base(name)
	{
		Int16 = value;
	}
}
