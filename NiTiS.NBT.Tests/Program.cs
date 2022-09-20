namespace NiTiS.NBT.Tests;

public static class Program
{
	public static void Main(string[] args)
	{
		using FileStream stream = File.OpenRead("B:/Minecraft/hotbar.nbt");
		using NBTReadStream input = new(stream);

		Tag root = input.ReadTag();


		FileStream fs = new("B:/Minecraft/__.nbt", FileMode.OpenOrCreate);
		using NBTWriteStream output = new(fs);

		output.WriteTag(root);
	}
}