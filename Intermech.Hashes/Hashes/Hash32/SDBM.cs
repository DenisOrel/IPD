// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.SDBM
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class SDBM : Hash, IHash32, IHash, ITransformBlock
{
  private uint hash;

  public SDBM()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    SDBM sdbm = new SDBM();
    sdbm.hash = this.hash;
    sdbm.BufferSize = this.BufferSize;
    return (IHash) sdbm;
  }

  public override void Initialize() => this.hash = 0U;

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
      this.hash = (uint) ((ulong) a_data[index] + (ulong) (this.hash << 6) + (ulong) (this.hash << 16 /*0x10*/) - (ulong) this.hash);
      ++index;
    }
  }
}
