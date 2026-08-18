// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.GOST3411_2012_256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class GOST3411_2012_256 : GOST3411_2012
{
  private static byte[] IV_256 = new byte[64 /*0x40*/]
  {
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1,
    (byte) 1
  };

  public GOST3411_2012_256()
    : base(32 /*0x20*/, GOST3411_2012_256.IV_256)
  {
  }

  public override IHash Clone()
  {
    GOST3411_2012_256 gosT34112012256 = new GOST3411_2012_256();
    gosT34112012256.bOff = this.bOff;
    gosT34112012256.IV = this.IV.DeepCopy();
    gosT34112012256.N = this.N.DeepCopy();
    gosT34112012256.Sigma = this.Sigma.DeepCopy();
    gosT34112012256.Ki = this.Ki.DeepCopy();
    gosT34112012256.m = this.m.DeepCopy();
    gosT34112012256.h = this.h.DeepCopy();
    gosT34112012256.tmp = this.tmp.DeepCopy();
    gosT34112012256.block = this.block.DeepCopy();
    gosT34112012256.BufferSize = this.BufferSize;
    return (IHash) gosT34112012256;
  }

  public override IHashResult TransformFinal()
  {
    byte[] bytes = base.TransformFinal().GetBytes();
    byte[] dest = new byte[this.hash_size];
    Intermech.Hashes.Utils.Utils.Memmove(ref dest, bytes, 32 /*0x20*/, 32 /*0x20*/);
    return (IHashResult) new HashResult(dest);
  }
}
