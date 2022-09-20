using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiTiS.NBT;

public sealed class NBTReadStreamOptions
{
	public static readonly NBTReadStreamOptions Default = new();
	public bool AllowEndTagInList { get; set; } = false;
	public bool UseGZipStream { get; set; } = false;
}
