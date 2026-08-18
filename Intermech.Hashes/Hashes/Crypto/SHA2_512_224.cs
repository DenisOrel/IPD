// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA2_512_224
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA2_512_224 : SHA2_512Base
{
  public SHA2_512_224()
    : base(28)
  {
  }

  public override IHash Clone()
  {
    SHA2_512_224 shA2512224 = new SHA2_512_224();
    shA2512224.buffer = this.buffer.Clone();
    shA2512224.processed_bytes = this.processed_bytes;
    shA2512224.state = this.state.DeepCopy();
    shA2512224.BufferSize = this.BufferSize;
    return (IHash) shA2512224;
  }

  public override void Initialize()
  {
    this.state[0] = 10105294471447203234UL;
    this.state[1] = 8350123849800275158UL;
    this.state[2] = 2160240930085379202UL;
    this.state[3] = 7466358040605728719UL;
    this.state[4] = 1111592415079452072UL;
    this.state[5] = 8638871050018654530UL;
    this.state[6] = 4583966954114332360UL;
    this.state[7] = 1230299281376055969UL;
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] array = new byte[32 /*0x20*/];
    fixed (ulong* src = this.state)
      fixed (byte* dest = array)
        Converters.be64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, array.Length);
    Array.Resize<byte>(ref array, this.HashSize);
    return array;
  }
}
