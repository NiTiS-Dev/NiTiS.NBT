using Pseudo.Buffers.Binary;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace NiTiS.NBT;

public class NBTReadStream : IDisposable
{
	private readonly BinaryReader stream;
	private readonly GZipStream? gZipStream;
	public readonly NBTReadStreamOptions Options;
	public NBTReadStream(Stream stream) : this(stream, NBTReadStreamOptions.Default) { }
	public NBTReadStream(Stream stream, NBTReadStreamOptions options)
	{
		Options = options;
		if (options.UseGZipStream)
		{
			gZipStream = new(stream, CompressionMode.Decompress);

			this.stream = new(gZipStream);
		}
		else
		{
			this.stream = new(stream);
		}
	}

	public void Dispose()
	{
		stream.Close();
		stream?.Dispose();
		gZipStream?.Close();
		gZipStream?.Dispose();
	}
	public Tag ReadTag()
		=> ReadTag(0);
	public Tag ReadTag(uint depth)
	{
		byte type = (byte)(stream.ReadByte() & 0xff);

		String name = String.Empty;
		if (type != EndTag.ID)
		{
			ushort nameSize = (ushort)BinaryPrimitives.ReadInt16BigEndian(stream.ReadInt16());

			if (nameSize > 0)
			{
				name = Encoding.UTF8.GetString(stream.ReadBytes(nameSize));
			}
		}

		return ReadTagPayload(type, name, depth);
	}
	private Tag ReadTagPayload(byte type, string name, uint depth)
	{
		switch (type)
		{
			case EndTag.ID: return new EndTag();
			case ByteTag.ID: return new ByteTag(name, stream.ReadSByte());
			case ShortTag.ID: return new ShortTag(name, stream.ReadInt16());
			case IntTag.ID: return new IntTag(name, stream.ReadInt32());
			case LongTag.ID: return new LongTag(name, stream.ReadInt64());
			case FloatTag.ID: return new FloatTag(name, stream.ReadSingle());
			case DoubleTag.ID: return new DoubleTag(name, stream.ReadDouble());
			case ByteArrayTag.ID:
				{
					ushort len = (ushort)BinaryPrimitives.ReadInt32BigEndian(stream.ReadInt32());

					return new ByteArrayTag(name, stream.ReadBytes(len));
				}
			case StringTag.ID:
				{
					ushort len = (ushort)BinaryPrimitives.ReadInt16BigEndian(stream.ReadInt16());
					String value = len > 0
						? Encoding.UTF8.GetString(stream.ReadBytes(len))
						: String.Empty;

					return new StringTag(name, value);
				}
			case ListTag.ID:
				{
					byte listType = stream.ReadByte();
					if (listType > 12)
						throw new IOException("Invalid list type");
					int len = BinaryPrimitives.ReadInt32BigEndian(stream.ReadInt32());

					List<Tag> list = new(len);
					for (int i = 0; i < len; i++)
					{
						Tag tag = ReadTagPayload(listType, String.Empty, depth + 1);
						if (tag is EndTag)
						{
							if (!Options.AllowEndTagInList)
								throw new IOException("TAG_End not permitted in a list");
							else
								continue;
						}
						list.Add(tag);
					}

					return new ListTag(name, listType, list);
				}
			case CompoundTag.ID:
				{
					Dictionary<string, Tag> compound = new(32);

					while (true)
					{
						Tag tag = ReadTag(depth + 1);
						if (tag is EndTag)
							break;
						else
							compound[tag.Name] = tag;
					}
					return new CompoundTag(name, compound);
				}
			default:
				{
					throw new IOException("Invalid tag type: " + type);
				}
		}
	}
}
