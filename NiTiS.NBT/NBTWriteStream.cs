using Pseudo.Buffers.Binary;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace NiTiS.NBT;

public class NBTWriteStream : IDisposable
{
	private readonly BinaryWriter stream;
	private readonly GZipStream? gZipStream;
	public NBTWriteStream(Stream stream) : this(stream, NBTWriteStreamOptions.Default) { }
	public NBTWriteStream(Stream stream, NBTWriteStreamOptions options)
	{
		if (options.UseGZipStream)
		{
			gZipStream = new(stream, options.GZipCompression);

			this.stream = new(gZipStream);
		}
		else
		{
			this.stream = new(stream);
		}
	}
	public void WriteTag(Tag tag)
	{
		byte type = tag.TagID;

		stream.Write(BinaryPrimitives.ReverseEndianness(type));

		if (type != EndTag.ID)
		{
			ushort lenBE = BinaryPrimitives.ReverseEndianness((ushort)tag.Name.Length);
			stream.Write(lenBE);

			stream.Write(Encoding.UTF8.GetBytes(tag.Name));

			WriteTagPayload(type, tag);
		}
	}
	private void WriteTagPayload(byte type, Tag tag)
	{
		switch (type)
		{
			case ByteTag.ID:
				ByteTag bt = tag as ByteTag;

				stream.Write(bt!.SByte);

				break;
			case ShortTag.ID:
				ShortTag st = tag as ShortTag;

				stream.Write(st!.Int16);

				break;
			case IntTag.ID:
				IntTag it = tag as IntTag;

				stream.Write(it!.Int32);

				break;
			case LongTag.ID:
				LongTag lt = tag as LongTag;

				stream.Write(lt.Int64);

				break;
			case FloatTag.ID:
				FloatTag ft = tag as FloatTag;

				stream.Write(ft.Single);

				break;
			case DoubleTag.ID:
				DoubleTag dt = tag as DoubleTag;

				stream.Write(dt!.Double);

				break;
			case ByteArrayTag.ID:
				ByteArrayTag bat = tag as ByteArrayTag;

				ushort len = BinaryPrimitives.ReverseEndianness((ushort)bat!.Array.Length);

				stream.Write(len);
				stream.Write(bat.Array);

				break;
			case StringTag.ID:
				StringTag strt = tag as StringTag;

				len = BinaryPrimitives.ReverseEndianness((ushort)strt!.String.Length);

				stream.Write(len);
				stream.Write(Encoding.UTF8.GetBytes(strt.String));

				break;
			case ListTag.ID:
				ListTag listt = tag as ListTag;

				stream.Write(listt.ChildID);
				int listLen = BinaryPrimitives.ReverseEndianness(listt.List.Count);

				stream.Write(listLen);

				for (int i = 0; i < listt.List.Count; i++)
				{
					WriteTagPayload(listt.ChildID, listt[i]);
				}

				break;
			case CompoundTag.ID:
				CompoundTag compound = tag as CompoundTag;

				foreach (Tag compoundTag in compound!)
				{
					WriteTag(compoundTag);
				}
				WriteTag(new EndTag());

				break;
			default: throw new ArgumentException("Invalid tag type");
		}
	}
	public void Dispose()
	{
		stream.Close();
		stream?.Dispose();
		gZipStream?.Close();
		gZipStream?.Dispose();
	}
}
