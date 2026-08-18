// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.Adler32
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Checksum;

internal sealed class Adler32 : Hash, IChecksum, IBlockHash, IHash, IHash32, ITransformBlock
{
  private const uint MOD_ADLER = 65521;
  private uint a = 1;
  private uint b;

  public Adler32()
    : base(4, 1)
  {
  }

  public override IHash Clone()
  {
    Adler32 adler32 = new Adler32();
    adler32.a = this.a;
    adler32.b = this.b;
    adler32.BufferSize = this.BufferSize;
    return (IHash) adler32;
  }

  public override void Initialize()
  {
    this.a = 1U;
    this.b = 0U;
  }

  public override IHashResult TransformFinal()
  {
    HashResult hashResult = new HashResult((int) this.b << 16 /*0x10*/ | (int) this.a);
    this.Initialize();
    return (IHashResult) hashResult;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    while (a_length > 0)
    {
      int num = 3800;
      if (num > a_length)
        num = a_length;
      a_length -= num;
      for (; num - 1 >= 0; --num)
      {
        this.a += (uint) a_data[a_index];
        this.b += this.a;
        ++a_index;
      }
      this.a %= 65521U;
      this.b %= 65521U;
    }
  }
}
