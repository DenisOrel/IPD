// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA2_512
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA2_512 : SHA2_512Base
{
  public SHA2_512()
    : base(64 /*0x40*/)
  {
  }

  public override IHash Clone()
  {
    SHA2_512 shA2512 = new SHA2_512();
    shA2512.buffer = this.buffer.Clone();
    shA2512.processed_bytes = this.processed_bytes;
    shA2512.state = this.state.DeepCopy();
    shA2512.BufferSize = this.BufferSize;
    return (IHash) shA2512;
  }

  public override void Initialize()
  {
    this.state[0] = 7640891576956012808UL;
    this.state[1] = 13503953896175478587UL;
    this.state[2] = 4354685564936845355UL;
    this.state[3] = 11912009170470909681UL;
    this.state[4] = 5840696475078001361UL;
    this.state[5] = 11170449401992604703UL;
    this.state[6] = 2270897969802886507UL;
    this.state[7] = 6620516959819538809UL;
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[64 /*0x40*/];
    fixed (ulong* src = this.state)
      fixed (byte* dest = result)
        Converters.be64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }
}
