// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA2_224
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA2_224 : SHA2_256Base
{
  public SHA2_224()
    : base(28)
  {
  }

  public override IHash Clone()
  {
    SHA2_224 shA2224 = new SHA2_224();
    shA2224.buffer = this.buffer.Clone();
    shA2224.processed_bytes = this.processed_bytes;
    shA2224.state = this.state.DeepCopy();
    shA2224.BufferSize = this.BufferSize;
    return (IHash) shA2224;
  }

  public override void Initialize()
  {
    this.state[0] = 3238371032U;
    this.state[1] = 914150663U;
    this.state[2] = 812702999U;
    this.state[3] = 4144912697U;
    this.state[4] = 4290775857U;
    this.state[5] = 1750603025U;
    this.state[6] = 1694076839U;
    this.state[7] = 3204075428U;
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[28];
    fixed (uint* src = this.state)
      fixed (byte* dest = result)
        Converters.be32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }
}
