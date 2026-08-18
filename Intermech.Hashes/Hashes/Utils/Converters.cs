// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Utils.Converters
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using System;
using System.Text;

#nullable disable
namespace Intermech.Hashes.Utils;

public static class Converters
{
  public static unsafe void swap_copy_str_to_u32(
    IntPtr src,
    int src_index,
    IntPtr dest,
    int dest_index,
    int length)
  {
    if ((((int) (long) (((IntPtr) (void*) dest - IntPtr.Zero) / 1) | (int) (long) (((IntPtr) (void*) src - IntPtr.Zero) / 1) | src_index | dest_index | length) & 3) == 0)
    {
      uint* numPtr1 = (uint*) ((IntPtr) (void*) src + src_index);
      uint* numPtr2 = (uint*) ((IntPtr) (void*) src + src_index + length);
      uint* numPtr3 = (uint*) ((IntPtr) (void*) dest + dest_index);
      for (; numPtr1 < numPtr2; ++numPtr1)
      {
        *numPtr3 = Bits.ReverseBytesUInt32(*numPtr1);
        ++numPtr3;
      }
    }
    else
    {
      byte* numPtr = (byte*) ((IntPtr) (void*) src + src_index);
      for (int index = length + dest_index; dest_index < index; ++dest_index)
      {
        *(sbyte*) ((IntPtr) (void*) dest + (dest_index ^ 3)) = (sbyte) *numPtr;
        ++numPtr;
      }
    }
  }

  public static unsafe void swap_copy_str_to_u64(
    IntPtr src,
    int src_index,
    IntPtr dest,
    int dest_index,
    int length)
  {
    if ((((int) (long) (((IntPtr) (void*) dest - IntPtr.Zero) / 1) | (int) (long) (((IntPtr) (void*) src - IntPtr.Zero) / 1) | src_index | dest_index | length) & 7) == 0)
    {
      ulong* numPtr1 = (ulong*) ((IntPtr) (void*) src + src_index);
      ulong* numPtr2 = (ulong*) ((IntPtr) (void*) src + src_index + length);
      ulong* numPtr3 = (ulong*) ((IntPtr) (void*) dest + dest_index);
      for (; numPtr1 < numPtr2; ++numPtr1)
      {
        *numPtr3 = Bits.ReverseBytesUInt64(*numPtr1);
        ++numPtr3;
      }
    }
    else
    {
      byte* numPtr = (byte*) ((IntPtr) (void*) src + src_index);
      for (int index = length + dest_index; dest_index < index; ++dest_index)
      {
        *(sbyte*) ((IntPtr) (void*) dest + (dest_index ^ 7)) = (sbyte) *numPtr;
        ++numPtr;
      }
    }
  }

  public static uint be2me_32(uint x)
  {
    return BitConverter.IsLittleEndian ? Bits.ReverseBytesUInt32(x) : x;
  }

  public static ulong be2me_64(ulong x)
  {
    return BitConverter.IsLittleEndian ? Bits.ReverseBytesUInt64(x) : x;
  }

  public static unsafe void be32_copy(
    IntPtr src,
    int src_index,
    IntPtr dest,
    int dest_index,
    int length)
  {
    if (BitConverter.IsLittleEndian)
      Converters.swap_copy_str_to_u32(src, src_index, dest, dest_index, length);
    else
      Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) ((IntPtr) (void*) dest + dest_index), (IntPtr) (void*) ((IntPtr) (void*) src + src_index), length);
  }

  public static unsafe void be64_copy(
    IntPtr src,
    int src_index,
    IntPtr dest,
    int dest_index,
    int length)
  {
    if (BitConverter.IsLittleEndian)
      Converters.swap_copy_str_to_u64(src, src_index, dest, dest_index, length);
    else
      Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) ((IntPtr) (void*) dest + dest_index), (IntPtr) (void*) ((IntPtr) (void*) src + src_index), length);
  }

  public static uint le2me_32(int x)
  {
    return !BitConverter.IsLittleEndian ? Bits.ReverseBytesUInt32((uint) x) : (uint) x;
  }

  public static ulong le2me_64(ulong x)
  {
    return !BitConverter.IsLittleEndian ? Bits.ReverseBytesUInt64(x) : x;
  }

  public static unsafe void le32_copy(
    IntPtr src,
    int src_index,
    IntPtr dest,
    int dest_index,
    int length)
  {
    if (BitConverter.IsLittleEndian)
      Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) ((IntPtr) (void*) dest + dest_index), (IntPtr) (void*) ((IntPtr) (void*) src + src_index), length);
    else
      Converters.swap_copy_str_to_u32(src, src_index, dest, dest_index, length);
  }

  public static unsafe void le64_copy(
    IntPtr src,
    int src_index,
    IntPtr dest,
    int dest_index,
    int length)
  {
    if (BitConverter.IsLittleEndian)
      Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) ((IntPtr) (void*) dest + dest_index), (IntPtr) (void*) ((IntPtr) (void*) src + src_index), length);
    else
      Converters.swap_copy_str_to_u64(src, src_index, dest, dest_index, length);
  }

  public static unsafe uint ReadBytesAsUInt32LE(IntPtr a_in, int a_index)
  {
    return Converters.le2me_32((int) *(uint*) ((IntPtr) (void*) a_in + a_index));
  }

  public static unsafe ulong ReadBytesAsUInt64LE(IntPtr a_in, int a_index)
  {
    return Converters.le2me_64((ulong) *(long*) ((IntPtr) (void*) a_in + a_index));
  }

  public static byte[] ReadUInt32AsBytesLE(uint a_in)
  {
    return new byte[4]
    {
      (byte) a_in,
      (byte) (a_in >> 8),
      (byte) (a_in >> 16 /*0x10*/),
      (byte) (a_in >> 24)
    };
  }

  public static void ReadUInt32AsBytesLE(uint a_Input, ref byte[] a_Output, int a_Index)
  {
    a_Output[a_Index] = (byte) a_Input;
    a_Output[a_Index + 1] = (byte) (a_Input >> 8);
    a_Output[a_Index + 2] = (byte) (a_Input >> 16 /*0x10*/);
    a_Output[a_Index + 3] = (byte) (a_Input >> 24);
  }

  public static void ReadUInt32AsBytesBE(uint a_Input, ref byte[] a_Output, int a_Index)
  {
    a_Output[a_Index] = (byte) (a_Input >> 24);
    a_Output[a_Index + 1] = (byte) (a_Input >> 16 /*0x10*/);
    a_Output[a_Index + 2] = (byte) (a_Input >> 8);
    a_Output[a_Index + 3] = (byte) a_Input;
  }

  public static byte[] ReadUInt64AsBytesLE(ulong a_in)
  {
    return new byte[8]
    {
      (byte) a_in,
      (byte) (a_in >> 8),
      (byte) (a_in >> 16 /*0x10*/),
      (byte) (a_in >> 24),
      (byte) (a_in >> 32 /*0x20*/),
      (byte) (a_in >> 40),
      (byte) (a_in >> 48 /*0x30*/),
      (byte) (a_in >> 56)
    };
  }

  public static void ReadUInt64AsBytesLE(ulong a_in, ref byte[] a_out, int a_index)
  {
    a_out[a_index] = (byte) a_in;
    a_out[a_index + 1] = (byte) (a_in >> 8);
    a_out[a_index + 2] = (byte) (a_in >> 16 /*0x10*/);
    a_out[a_index + 3] = (byte) (a_in >> 24);
    a_out[a_index + 4] = (byte) (a_in >> 32 /*0x20*/);
    a_out[a_index + 5] = (byte) (a_in >> 40);
    a_out[a_index + 6] = (byte) (a_in >> 48 /*0x30*/);
    a_out[a_index + 7] = (byte) (a_in >> 56);
  }

  public static void ReadUInt64AsBytesBE(ulong a_in, ref byte[] a_out, int a_index)
  {
    a_out[a_index] = (byte) (a_in >> 56);
    a_out[a_index + 1] = (byte) (a_in >> 48 /*0x30*/);
    a_out[a_index + 2] = (byte) (a_in >> 40);
    a_out[a_index + 3] = (byte) (a_in >> 32 /*0x20*/);
    a_out[a_index + 4] = (byte) (a_in >> 24);
    a_out[a_index + 5] = (byte) (a_in >> 16 /*0x10*/);
    a_out[a_index + 6] = (byte) (a_in >> 8);
    a_out[a_index + 7] = (byte) a_in;
  }

  public static unsafe string ConvertBytesToHexString(byte[] a_in, bool a_group, char delimeter = '-')
  {
    fixed (byte* a_in1 = a_in)
      return a_in == null || a_in.Length == 0 ? "" : Converters.ConvertBytesToHexString((IntPtr) (void*) a_in1, (uint) a_in.Length, a_group, delimeter);
  }

  public static string ConvertBytesToHexString(
    IntPtr a_in,
    uint size,
    bool a_group,
    char delimeter = '-')
  {
    string upper = ExtendedBitConverter.ToString(a_in, 0, (int) size, delimeter).ToUpper();
    return size == 1U || size != 2U && a_group ? upper : upper.Replace(delimeter.ToString(), "");
  }

  public static byte[] ConvertHexStringToBytes(string a_in, char delimeter = '-')
  {
    string str1 = a_in.Replace(delimeter.ToString(), "");
    byte[] bytes = new byte[str1.Length >> 1];
    int startIndex = 0;
    int index = 0;
    while (startIndex < str1.Length)
    {
      string str2 = str1.Substring(startIndex, 2);
      bytes[index] = (byte) Convert.ToChar(Convert.ToUInt32(str2, 16 /*0x10*/));
      startIndex += 2;
      ++index;
    }
    return bytes;
  }

  public static byte[] ConvertStringToBytes(string a_in, Encoding encoding)
  {
    return string.IsNullOrEmpty(a_in) ? new byte[0] : encoding.GetBytes(a_in);
  }
}
