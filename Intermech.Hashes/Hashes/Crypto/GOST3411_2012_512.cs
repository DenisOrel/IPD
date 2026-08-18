// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.GOST3411_2012_512
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class GOST3411_2012_512 : GOST3411_2012
{
  private static byte[] IV_512 = new byte[64 /*0x40*/];

  public GOST3411_2012_512()
    : base(64 /*0x40*/, GOST3411_2012_512.IV_512)
  {
  }

  public override IHash Clone()
  {
    GOST3411_2012_512 gosT34112012512 = new GOST3411_2012_512();
    gosT34112012512.bOff = this.bOff;
    gosT34112012512.IV = this.IV.DeepCopy();
    gosT34112012512.N = this.N.DeepCopy();
    gosT34112012512.Sigma = this.Sigma.DeepCopy();
    gosT34112012512.Ki = this.Ki.DeepCopy();
    gosT34112012512.m = this.m.DeepCopy();
    gosT34112012512.h = this.h.DeepCopy();
    gosT34112012512.tmp = this.tmp.DeepCopy();
    gosT34112012512.block = this.block.DeepCopy();
    gosT34112012512.BufferSize = this.BufferSize;
    return (IHash) gosT34112012512;
  }
}
