namespace NiTiS.NBT;

public sealed class IntTag : Tag
{
	public readonly int Int32;
	public const byte ID = 3;
	public override object? Value => Int32;
	public override byte TagID => ID;
	public IntTag(string name, int value) : base(name)
	{
		Int32 = value;
	}
}
