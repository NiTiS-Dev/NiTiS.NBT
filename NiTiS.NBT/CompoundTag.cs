using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NiTiS.NBT;

public sealed class CompoundTag : Tag, IParentTag, IEnumerable<Tag>
{
	public readonly IDictionary<string, Tag> Nodes;
	public const byte ID = 10;
	public override object Value => Nodes;
	public override byte TagID => ID;
	public CompoundTag(string name, IDictionary<string, Tag> value) : base(name)
	{
		Nodes = value;
	}
	public Tag this[string name]
	{
		[DebuggerStepThrough]
		get => Nodes[name];
		[DebuggerStepThrough]
		set => Nodes[name] = value;
	}
	public IEnumerator<Tag> GetEnumerator() => Nodes.Values.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => Nodes.Values.GetEnumerator();
}

