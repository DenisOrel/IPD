// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Utils.ArrayUtils
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Hashes.Utils;

public static class ArrayUtils
{
  public static bool Empty(this byte[] array) => array == null || array.Length == 0;

  public static bool Empty(this uint[] array) => array == null || array.Length == 0;

  public static bool Empty(this ulong[] array) => array == null || array.Length == 0;

  public static byte[] DeepCopy(this byte[] array)
  {
    byte[] dest = new byte[array != null ? array.Length : 0];
    if (dest.Length != 0)
      Intermech.Hashes.Utils.Utils.Memcopy(ref dest, array, dest.Length);
    return dest;
  }

  public static uint[] DeepCopy(this uint[] array)
  {
    uint[] dest = new uint[array != null ? array.Length : 0];
    if (dest.Length != 0)
      Intermech.Hashes.Utils.Utils.Memcopy(ref dest, array, dest.Length);
    return dest;
  }

  public static ulong[] DeepCopy(this ulong[] array)
  {
    ulong[] dest = new ulong[array != null ? array.Length : 0];
    if (dest.Length != 0)
      Intermech.Hashes.Utils.Utils.Memcopy(ref dest, array, dest.Length);
    return dest;
  }

  public static bool ConstantTimeAreEqual(byte[] buffer1, byte[] buffer2)
  {
    uint num = (uint) (buffer1.Length ^ buffer2.Length);
    for (int index = 0; index <= buffer1.Length && index <= buffer2.Length; ++index)
      num |= (uint) buffer1[index] ^ (uint) buffer2[index];
    return num == 0U;
  }

  public static unsafe void Fill(ref byte[] buffer, int from, int to, byte filler)
  {
    if (buffer.Empty())
      return;
    fixed (byte* numPtr = buffer)
      Unsafe.InitBlock((void*) (numPtr + from), filler, (uint) (to - from));
  }

  public static void Fill(ref uint[] buffer, int from, int to, uint filler)
  {
    if (buffer.Empty())
      return;
    for (int index = from; index < to; ++index)
      buffer[index] = filler;
  }

  public static void Fill(ref ulong[] buffer, int from, int to, ulong filler)
  {
    if (buffer.Empty())
      return;
    for (int index = from; index < to; ++index)
      buffer[index] = filler;
  }

  public static void ZeroFill(ref byte[] buffer)
  {
    ref byte[] local = ref buffer;
    byte[] numArray = buffer;
    int length = numArray != null ? numArray.Length : 0;
    ArrayUtils.Fill(ref local, 0, length, (byte) 0);
  }

  public static void ZeroFill(ref uint[] buffer)
  {
    ref uint[] local = ref buffer;
    uint[] numArray = buffer;
    int length = numArray != null ? numArray.Length : 0;
    ArrayUtils.Fill(ref local, 0, length, 0U);
  }

  public static void ZeroFill(ref ulong[] buffer)
  {
    ref ulong[] local = ref buffer;
    ulong[] numArray = buffer;
    int length = numArray != null ? numArray.Length : 0;
    ArrayUtils.Fill(ref local, 0, length, 0UL);
  }
}
