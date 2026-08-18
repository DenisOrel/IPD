// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.RS
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class RS : Hash, IHash32, IHash, ITransformBlock
{
  private uint a;
  private uint hash;
  private static readonly uint b = 378551;

  public RS()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    RS rs = new RS();
    rs.hash = this.hash;
    rs.a = this.a;
    rs.BufferSize = this.BufferSize;
    return (IHash) rs;
  }

  public override void Initialize()
  {
    this.hash = 0U;
    this.a = 63689U;
  }

  public override IHashResult TransformFinal()
  {
    HashResult hashResult = new HashResult(this.hash);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    int index = a_index;
    for (; a_length > 0; --a_length)
    {
      this.hash = this.hash * this.a + (uint) a_data[index];
      this.a *= RS.b;
      ++index;
    }
  }
}
