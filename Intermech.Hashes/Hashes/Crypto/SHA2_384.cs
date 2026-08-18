// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA2_384
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA2_384 : SHA2_512Base
{
  public SHA2_384()
    : base(48 /*0x30*/)
  {
  }

  public override IHash Clone()
  {
    SHA2_384 shA2384 = new SHA2_384();
    shA2384.buffer = this.buffer.Clone();
    shA2384.processed_bytes = this.processed_bytes;
    shA2384.state = this.state.DeepCopy();
    shA2384.BufferSize = this.BufferSize;
    return (IHash) shA2384;
  }

  public override void Initialize()
  {
    this.state[0] = 14680500436340154072UL;
    this.state[1] = 7105036623409894663UL;
    this.state[2] = 10473403895298186519UL;
    this.state[3] = 1526699215303891257UL;
    this.state[4] = 7436329637833083697UL;
    this.state[5] = 10282925794625328401UL;
    this.state[6] = 15784041429090275239UL;
    this.state[7] = 5167115440072839076UL;
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[48 /*0x30*/];
    fixed (ulong* src = this.state)
      fixed (byte* dest = result)
        Converters.be64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }
}
