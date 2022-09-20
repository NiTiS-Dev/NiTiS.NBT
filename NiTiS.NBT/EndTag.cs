using static System.String;

namespace NiTiS.NBT;

public sealed class EndTag : Tag
{
	public const byte ID = 0;
	public override object? Value => null;
	public override byte TagID => ID;
	public EndTag() : base(Empty) { }

}
