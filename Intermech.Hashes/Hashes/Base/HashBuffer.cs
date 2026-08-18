// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Base.HashBuffer
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using System;

#nullable disable
namespace Intermech.Hashes.Base;

internal sealed class HashBuffer
{
  private byte[] data;
  private int pos;

  public HashBuffer()
  {
  }

  public HashBuffer(int a_length)
  {
    this.data = new byte[a_length];
    this.Initialize();
  }

  public HashBuffer Clone()
  {
    return new HashBuffer()
    {
      pos = this.pos,
      data = this.data.DeepCopy()
    };
  }

  public unsafe bool Feed(IntPtr a_data, int a_length_a_data, int a_length)
  {
    if (a_length_a_data == 0 || a_length == 0)
      return false;
    int n = this.data.Length - this.pos;
    if (n > a_length)
      n = a_length;
    fixed (byte* dest = &this.data[0])
      Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) dest, a_data, n);
    this.pos += n;
    return this.IsFull;
  }

  public unsafe bool Feed(
    IntPtr a_data,
    int a_length_a_data,
    ref int a_start_index,
    ref int a_length,
    ref ulong a_processed_bytes)
  {
    if (a_length_a_data == 0 || a_length == 0)
      return false;
    int n = this.data.Length - this.pos;
    if (n > a_length)
      n = a_length;
    fixed (byte* dest = &this.data[this.pos])
      Intermech.Hashes.Utils.Utils.Memmove((IntPtr) (void*) dest, (IntPtr) (void*) ((IntPtr) (void*) a_data + a_start_index), n);
    this.pos += n;
    a_start_index += n;
    a_length -= n;
    a_processed_bytes += (ulong) n;
    return this.IsFull;
  }

  public byte[] GetBytes()
  {
    this.pos = 0;
    return this.data.DeepCopy();
  }

  public byte[] GetBytesZeroPadded()
  {
    Intermech.Hashes.Utils.Utils.Memset(ref this.data, (byte) 0, this.pos);
    this.pos = 0;
    return this.data.DeepCopy();
  }

  public bool IsEmpty => this.pos == 0;

  public bool IsFull => this.pos == this.data.Length;

  public int Length => this.data.Length;

  public int Position => this.pos;

  public void Initialize()
  {
    this.pos = 0;
    ArrayUtils.ZeroFill(ref this.data);
  }

  public override string ToString()
  {
    return $"HashBuffer, Length: {this.Length}, Pos: {this.Position}, IsEmpty: {this.IsEmpty}";
  }
}
