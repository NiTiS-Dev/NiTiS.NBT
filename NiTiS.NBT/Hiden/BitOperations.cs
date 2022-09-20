using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pseudo.Numerics;

internal static class BitOperations
{
	/// <summary>
	/// Rotates the specified value left by the specified number of bits.
	/// Similar in behavior to the x86 instruction ROL.
	/// </summary>
	/// <param name="value">The value to rotate.</param>
	/// <param name="offset">The number of bits to rotate by.
	/// Any value outside the range [0..31] is treated as congruent mod 32.</param>
	/// <returns>The rotated value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[CLSCompliant(false)]
	[DebuggerStepThrough]
	public static uint RotateLeft(uint value, int offset)
		=> (value << offset) | (value >> (32 - offset));

	/// <summary>
	/// Rotates the specified value right by the specified number of bits.
	/// Similar in behavior to the x86 instruction ROR.
	/// </summary>
	/// <param name="value">The value to rotate.</param>
	/// <param name="offset">The number of bits to rotate by.
	/// Any value outside the range [0..31] is treated as congruent mod 32.</param>
	/// <returns>The rotated value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[CLSCompliant(false)]
	[DebuggerStepThrough]
	public static uint RotateRight(uint value, int offset)
		=> (value >> offset) | (value << (32 - offset));
}
