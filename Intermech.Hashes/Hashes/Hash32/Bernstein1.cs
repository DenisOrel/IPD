// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.Bernstein1
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class Bernstein1 : Hash, IHash32, IHash, ITransformBlock
{
  private uint hash;

  public Bernstein1()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    Bernstein1 bernstein1 = new Bernstein1();
    bernstein1.hash = this.hash;
    bernstein1.BufferSize = this.BufferSize;
    return (IHash) bernstein1;
  }

  public override void Initialize() => this.hash = 5381U;

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
      this.hash = this.hash * 33U ^ (uint) a_data[index];
      ++index;
    }
  }
}
