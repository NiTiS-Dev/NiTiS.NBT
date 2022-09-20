using System;
using System.Collections;
using System.Collections.Generic;

namespace NiTiS.NBT;

public sealed class ListTag : Tag, IParentTag
{
	public readonly IList<Tag> List;
	public readonly byte ChildID;
	public const byte ID = 9;
	public override object Value => List;
	public override byte TagID => ID;
	public ListTag(string name, byte childType, IList<Tag> value) : base(name)
	{
		ChildID = childType;
		List = value;
	}
	public Tag this[int index]
	{
		get => List[index];
		set => List[index] = value;
	}
	public IEnumerator<Tag> GetEnumerator() => List.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();
	public void Add(Tag item) => List.Add(item); 
	public void Clear() => List.Clear();
	public bool Contains(Tag item) => List.Contains(item);
	public int IndexOf(Tag item) => IndexOf(item);
	public void Insert(int index, Tag item) => List.Insert(index, item);
	public void Remove(Tag item) => List.Remove(item);
	public void RemoveAt(int index) => List.RemoveAt(index);
	public void CopyTo(Tag[] array, int index) => List.CopyTo(array, index);
}

