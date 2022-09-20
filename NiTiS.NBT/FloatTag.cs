namespace NiTiS.NBT;

public sealed class FloatTag : Tag
{
	public readonly float Single;
	public const byte ID = 5;
	public override object Value => Single;
	public override byte TagID => ID;
	public FloatTag(string name, float value) : base(name)
	{
		Single = value;
	}
}
