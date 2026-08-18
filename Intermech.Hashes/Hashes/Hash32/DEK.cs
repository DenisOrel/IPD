// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.DEK
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System.IO;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class DEK : MultipleTransformNonBlock, IHash32, IHash, ITransformBlock
{
  public DEK()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    DEK dek = new DEK();
    dek.Buffer = new MemoryStream();
    byte[] array = this.Buffer.ToArray();
    dek.Buffer.Write(array, 0, array.Length);
    dek.Buffer.Position = this.Buffer.Position;
    dek.BufferSize = this.BufferSize;
    return (IHash) dek;
  }

  protected override IHashResult ComputeAggregatedBytes(byte[] a_data)
  {
    uint num = 0;
    if (!a_data.Empty())
    {
      num = (uint) a_data.Length;
      for (int index = 0; index < a_data.Length; ++index)
        num = Bits.RotateLeft32(num, 5) ^ (uint) a_data[index];
    }
    return (IHashResult) new HashResult(num);
  }
}
