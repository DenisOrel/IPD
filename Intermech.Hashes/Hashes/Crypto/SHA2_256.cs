// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA2_256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA2_256 : SHA2_256Base
{
  public SHA2_256()
    : base(32 /*0x20*/)
  {
  }

  public override IHash Clone()
  {
    SHA2_256 shA2256 = new SHA2_256();
    shA2256.buffer = this.buffer.Clone();
    shA2256.processed_bytes = this.processed_bytes;
    shA2256.state = this.state.DeepCopy();
    shA2256.BufferSize = this.BufferSize;
    return (IHash) shA2256;
  }

  public override void Initialize()
  {
    this.state[0] = 1779033703U;
    this.state[1] = 3144134277U;
    this.state[2] = 1013904242U;
    this.state[3] = 2773480762U;
    this.state[4] = 1359893119U;
    this.state[5] = 2600822924U;
    this.state[6] = 528734635U;
    this.state[7] = 1541459225U;
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[32 /*0x20*/];
    fixed (uint* src = this.state)
      fixed (byte* dest = result)
        Converters.be32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }
}
