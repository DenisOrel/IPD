// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Base.BlockHash
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Base;

internal abstract class BlockHash : Hash, IBlockHash, IHash
{
  protected HashBuffer buffer;
  protected ulong processed_bytes;

  public BlockHash(int a_hash_size, int a_block_size, int a_buffer_size = -1)
    : base(a_hash_size, a_block_size)
  {
    if (a_buffer_size == -1)
      a_buffer_size = a_block_size;
    this.buffer = new HashBuffer(a_buffer_size);
  }

  public override unsafe void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    fixed (byte* a_data1 = a_data)
    {
      if (!this.buffer.IsEmpty && this.buffer.Feed((IntPtr) (void*) a_data1, a_data.Length, ref a_index, ref a_length, ref this.processed_bytes))
        this.TransformBuffer();
      for (; a_length >= this.buffer.Length; a_length -= this.buffer.Length)
      {
        this.processed_bytes += (ulong) this.buffer.Length;
        this.TransformBlock((IntPtr) (void*) a_data1, this.buffer.Length, a_index);
        a_index += this.buffer.Length;
      }
      if (a_length > 0)
        this.buffer.Feed((IntPtr) (void*) a_data1, a_data.Length, ref a_index, ref a_length, ref this.processed_bytes);
    }
  }

  public override void Initialize()
  {
    this.buffer.Initialize();
    this.processed_bytes = 0UL;
  }

  public override IHashResult TransformFinal()
  {
    this.Finish();
    byte[] result = this.GetResult();
    this.Initialize();
    return (IHashResult) new HashResult(result);
  }

  private unsafe void TransformBuffer()
  {
    fixed (byte* a_data = this.buffer.GetBytes())
      this.TransformBlock((IntPtr) (void*) a_data, this.buffer.Length, 0);
  }

  protected abstract void Finish();

  protected abstract void TransformBlock(IntPtr a_data, int a_data_length, int a_index);

  protected abstract byte[] GetResult();
}
