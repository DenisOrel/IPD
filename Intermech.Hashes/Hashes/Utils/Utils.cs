// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Utils.Utils
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Hashes.Utils;

public static class Utils
{
  public static void Memcopy(ref byte[] dest, byte[] src, int n, int indexSrc = 0, int indexDest = 0)
  {
    Array.Copy((Array) src, indexSrc, (Array) dest, indexDest, n);
  }

  public static void Memcopy(ref uint[] dest, uint[] src, int n, int indexSrc = 0, int indexDest = 0)
  {
    Array.Copy((Array) src, indexSrc, (Array) dest, indexDest, n);
  }

  public static void Memcopy(ref ulong[] dest, ulong[] src, int n, int indexSrc = 0, int indexDest = 0)
  {
    Array.Copy((Array) src, indexSrc, (Array) dest, indexDest, n);
  }

  public static void Memcopy(IntPtr dest, IntPtr src, int n) => Intermech.Hashes.Utils.Utils.Memmove(dest, src, n);

  public static unsafe void Memmove(IntPtr dest, IntPtr src, int n)
  {
    Unsafe.CopyBlock((void*) dest, (void*) src, (uint) n);
  }

  public static void Memmove(ref byte[] dest, byte[] src, int n, int indexSrc = 0, int indexDest = 0)
  {
    Array.Copy((Array) src, indexSrc, (Array) dest, indexDest, n);
  }

  public static void Memmove(ref uint[] dest, uint[] src, int n, int indexSrc = 0, int indexDest = 0)
  {
    Array.Copy((Array) src, indexSrc, (Array) dest, indexDest, n);
  }

  public static void Memmove(ref ulong[] dest, ulong[] src, int n, int indexSrc = 0, int indexDest = 0)
  {
    Array.Copy((Array) src, indexSrc, (Array) dest, indexDest, n);
  }

  public static unsafe void Memset(IntPtr dest, byte value, int n)
  {
    byte* numPtr = (byte*) (void*) dest;
    for (int index = 0; index < n; ++index)
      numPtr[index] = value;
  }

  public static void Memset(ref byte[] array, byte value, int index = 0)
  {
    if (array.Empty())
      return;
    int val1 = 32 /*0x20*/;
    int srcOffset = index;
    int length1 = array.Length;
    int num = index + val1 < length1 ? index + val1 : length1;
    while (index < num)
      array[index++] = value;
    int length2 = array.Length;
    while (index < length1)
    {
      Buffer.BlockCopy((Array) array, srcOffset, (Array) array, index, Math.Min(val1, length1 - index));
      index += val1;
      val1 *= 2;
    }
  }

  public static unsafe void Memset(ref uint[] array, byte value, int index = 0)
  {
    if (array.Empty())
      return;
    fixed (uint* numPtr = array)
      Unsafe.InitBlock((void*) (numPtr + index), value, (uint) (array.Length * 4));
  }

  public static unsafe void Memset(ref ulong[] array, byte value, int index = 0, int n = -1)
  {
    if (array.Empty())
      return;
    fixed (ulong* numPtr = array)
      Unsafe.InitBlock((void*) (numPtr + index), value, (uint) (array.Length * 8));
  }

  public static byte[] Concat(byte[] x, byte[] y)
  {
    byte[] numArray = new byte[0];
    int index = 0;
    if (x.Empty())
    {
      if (y.Empty())
        return numArray;
      Array.Resize<byte>(ref numArray, y.Length);
      Intermech.Hashes.Utils.Utils.Memcopy(ref numArray, y, y.Length);
      return numArray;
    }
    if (y.Empty())
    {
      Array.Resize<byte>(ref numArray, x.Length);
      Intermech.Hashes.Utils.Utils.Memcopy(ref numArray, x, x.Length);
      return numArray;
    }
    Array.Resize<byte>(ref numArray, x.Length + y.Length);
    if (x.Length == y.Length)
    {
      for (; index < y.Length; numArray[x.Length + index] = y[index++])
        numArray[index] = x[index];
    }
    else if (x.Length > y.Length)
    {
      for (; index < y.Length; numArray[x.Length + index] = y[index++])
        numArray[index] = x[index];
      while (index < x.Length)
        numArray[index] = x[index++];
    }
    else if (y.Length > x.Length)
    {
      for (; index < x.Length; numArray[x.Length + index] = y[index++])
        numArray[index] = x[index];
      while (index < y.Length)
        numArray[x.Length + index] = y[index++];
    }
    return numArray;
  }

  public static uint[] Concat(uint[] x, uint[] y)
  {
    uint[] numArray = new uint[0];
    int index = 0;
    if (x.Empty())
    {
      if (y.Empty())
        return numArray;
      Array.Resize<uint>(ref numArray, y.Length);
      Intermech.Hashes.Utils.Utils.Memcopy(ref numArray, y, y.Length);
      return numArray;
    }
    if (y.Empty())
    {
      Array.Resize<uint>(ref numArray, x.Length);
      Intermech.Hashes.Utils.Utils.Memcopy(ref numArray, x, x.Length);
      return numArray;
    }
    Array.Resize<uint>(ref numArray, x.Length + y.Length);
    if (x.Length == y.Length)
    {
      for (; index < y.Length; numArray[x.Length + index] = y[index++])
        numArray[index] = x[index];
    }
    else if (x.Length > y.Length)
    {
      for (; index < y.Length; numArray[x.Length + index] = y[index++])
        numArray[index] = x[index];
      while (index < x.Length)
        numArray[index] = x[index++];
    }
    else if (y.Length > x.Length)
    {
      for (; index < x.Length; numArray[x.Length + index] = y[index++])
        numArray[index] = x[index];
      while (index < y.Length)
        numArray[x.Length + index] = y[index++];
    }
    return numArray;
  }
}
