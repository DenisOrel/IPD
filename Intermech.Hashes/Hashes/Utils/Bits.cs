// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Utils.Bits
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using System;

#nullable disable
namespace Intermech.Hashes.Utils;

internal static class Bits
{
  public static unsafe void ReverseByteArray(IntPtr Source, IntPtr Dest, long size)
  {
    byte* numPtr1 = (byte*) (void*) Source;
    byte* numPtr2 = (byte*) ((IntPtr) (void*) Dest + (IntPtr) (size - 1L));
    for (; size > 0L; --size)
    {
      *numPtr2 = *numPtr1;
      ++numPtr1;
      --numPtr2;
    }
  }

  public static int ReverseBytesInt32(int value)
  {
    return (value & (int) byte.MaxValue) << 24 | (Bits.Asr32(value, 8) & (int) byte.MaxValue) << 16 /*0x10*/ | (Bits.Asr32(value, 16 /*0x10*/) & (int) byte.MaxValue) << 8 | Bits.Asr32(value, 24) & (int) byte.MaxValue;
  }

  public static byte ReverseBitsUInt8(byte value)
  {
    byte num1 = (byte) ((int) value >> 1 & 85 | (int) value << 1 & 170);
    byte num2 = (byte) ((int) num1 >> 2 & 51 | (int) num1 << 2 & 204);
    return (byte) ((int) num2 >> 4 & 15 | (int) num2 << 4 & 240 /*0xF0*/);
  }

  public static ushort ReverseBytesUInt16(ushort value)
  {
    return (ushort) ((uint) (((int) value & (int) byte.MaxValue) << 8) | ((uint) value & 65280U) >> 8);
  }

  public static uint ReverseBytesUInt32(uint value)
  {
    return (uint) (((int) value & (int) byte.MaxValue) << 24 | ((int) value & 65280) << 8) | (value & 16711680U /*0xFF0000*/) >> 8 | (value & 4278190080U /*0xFF000000*/) >> 24;
  }

  public static ulong ReverseBytesUInt64(ulong value)
  {
    return (ulong) (((long) value & (long) byte.MaxValue) << 56 | ((long) value & 65280L) << 40 | ((long) value & 16711680L /*0xFF0000*/) << 24 | ((long) value & 4278190080L /*0xFF000000*/) << 8) | (value & 1095216660480UL /*0xFF00000000*/) >> 8 | (value & 280375465082880UL /*0xFF0000000000*/) >> 24 | (value & 71776119061217280UL /*0xFF000000000000*/) >> 40 | (value & 18374686479671623680UL /*0xFF00000000000000*/) >> 56;
  }

  public static int Asr32(int value, int ShiftBits)
  {
    return value >>> (ShiftBits & 31 /*0x1F*/) | (-(value >>> 31 /*0x1F*/) & -Convert.ToInt32((ShiftBits & 31 /*0x1F*/) != 0)) << 32 /*0x20*/ - (ShiftBits & 31 /*0x1F*/);
  }

  public static long Asr64(long value, long ShiftBits)
  {
    return value >>> (int) (ShiftBits & 63L /*0x3F*/) | (-(value >>> 63 /*0x3F*/) & (long) -Convert.ToInt32(((ulong) ShiftBits & 63UL /*0x3F*/) > 0UL)) << (int) (64L /*0x40*/ - (ShiftBits & 63L /*0x3F*/));
  }

  public static uint RotateLeft32(uint a_value, int a_n)
  {
    a_n &= 31 /*0x1F*/;
    return a_value << a_n | a_value >> 32 /*0x20*/ - a_n;
  }

  public static ulong RotateLeft64(ulong a_value, int a_n)
  {
    a_n &= 63 /*0x3F*/;
    return a_value << a_n | a_value >> 64 /*0x40*/ - a_n;
  }

  public static uint RotateRight32(uint a_value, int a_n)
  {
    a_n &= 31 /*0x1F*/;
    return a_value >> a_n | a_value << 32 /*0x20*/ - a_n;
  }

  public static ulong RotateRight64(ulong a_value, int a_n)
  {
    a_n &= 63 /*0x3F*/;
    return a_value >> a_n | a_value << 64 /*0x40*/ - a_n;
  }
}
