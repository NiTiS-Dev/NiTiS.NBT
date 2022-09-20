// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Pseudo.Buffers.Binary
{
	internal static partial class BinaryPrimitives
	{
		/// <summary>
		/// Reads a <see cref="double" /> from the beginning of a read-only span of bytes, as big endian.
		/// </summary>
		/// <param name="source">The read-only span to read.</param>
		/// <returns>The big endian value.</returns>
		/// <remarks>Reads exactly 8 bytes from the beginning of the span.</remarks>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="source"/> is too small to contain a <see cref="double" />.
		/// </exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double ReadDoubleBigEndian(double source)
		{
			return BitConverter.IsLittleEndian ?
				BitConverter.Int64BitsToDouble(ReverseEndianness((long)source)) :
				source;
		}

		/// <summary>
		/// Reads an Int16 out of a read-only span of bytes as big endian.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerStepThrough]
		public static short ReadInt16BigEndian(short source)
		{
			short result = source;
			if (BitConverter.IsLittleEndian)
			{
				result = ReverseEndianness(result);
			}
			return result;
		}

		/// <summary>
		/// Reads an Int32 out of a read-only span of bytes as big endian.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerStepThrough]
		public static int ReadInt32BigEndian(int source)
		{
			int result = source;
			if (BitConverter.IsLittleEndian)
			{
				result = ReverseEndianness(result);
			}
			return result;
		}

		/// <summary>
		/// Reads an Int64 out of a read-only span of bytes as big endian.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadInt64BigEndian(long source)
		{
			long result = source;
			if (BitConverter.IsLittleEndian)
			{
				result = ReverseEndianness(result);
			}
			return result;
		}

		/// <summary>
		/// Reads a <see cref="float" /> from the beginning of a read-only span of bytes, as big endian.
		/// </summary>
		/// <param name="source">The read-only span to read.</param>
		/// <returns>The big endian value.</returns>
		/// <remarks>Reads exactly 4 bytes from the beginning of the span.</remarks>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="source"/> is too small to contain a <see cref="float" />.
		/// </exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe float ReadSingleBigEndian(float source)
		{
			if (BitConverter.IsLittleEndian)
			{
				int bits = ReverseEndianness((int)source);
				return *((Single*)&bits);
			}
			else
			{
				return source;
			}
		}
	}
}