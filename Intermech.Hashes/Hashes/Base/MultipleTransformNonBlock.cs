// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Base.MultipleTransformNonBlock
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System.IO;

#nullable disable
namespace Intermech.Hashes.Base;

internal abstract class MultipleTransformNonBlock : Hash, INonBlockHash
{
  protected MemoryStream Buffer;

  public MultipleTransformNonBlock(int a_hash_size, int a_block_size)
    : base(a_hash_size, a_block_size)
  {
    this.Buffer = new MemoryStream();
  }

  ~MultipleTransformNonBlock()
  {
    this.Buffer.Flush();
    this.Buffer.Close();
  }

  public override void Initialize()
  {
    this.Buffer.Flush();
    this.Buffer.SetLength(0L);
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    if (a_data.Empty())
      return;
    this.Buffer.Write(a_data, a_index, a_length);
  }

  public override IHashResult TransformFinal()
  {
    IHashResult aggregatedBytes = this.ComputeAggregatedBytes(this.Aggregate());
    this.Initialize();
    return aggregatedBytes;
  }

  public override IHashResult ComputeBytes(byte[] a_data)
  {
    this.Initialize();
    return this.ComputeAggregatedBytes(a_data);
  }

  protected abstract IHashResult ComputeAggregatedBytes(byte[] a_data);

  private byte[] Aggregate()
  {
    byte[] buffer = new byte[0];
    if (this.Buffer.Length > 0L)
    {
      this.Buffer.Position = 0L;
      buffer = new byte[this.Buffer.Length];
      this.Buffer.Read(buffer, 0, (int) this.Buffer.Length);
    }
    return buffer;
  }
}
