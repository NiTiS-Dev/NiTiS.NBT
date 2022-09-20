namespace NiTiS.NBT;

public sealed class DoubleTag : Tag
{
	public readonly double Double;
	public const byte ID = 6;
	public override object Value => Double;
	public override byte TagID => ID;
	public DoubleTag(string name, double value) : base(name)
	{
		Double = value;
	}
}

