using System.IO.Compression;

namespace NiTiS.NBT;
public sealed class NBTWriteStreamOptions
{
	public static readonly NBTWriteStreamOptions Default = new();
	public bool UseGZipStream { get; set; } = false;
	public CompressionLevel GZipCompression { get; set; } = CompressionLevel.Optimal;
}
