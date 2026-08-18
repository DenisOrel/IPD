// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.SHA2_512_256
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class SHA2_512_256 : SHA2_512Base
{
  public SHA2_512_256()
    : base(32 /*0x20*/)
  {
  }

  public override IHash Clone()
  {
    SHA2_512_256 shA2512256 = new SHA2_512_256();
    shA2512256.buffer = this.buffer.Clone();
    shA2512256.processed_bytes = this.processed_bytes;
    shA2512256.state = this.state.DeepCopy();
    shA2512256.BufferSize = this.BufferSize;
    return (IHash) shA2512256;
  }

  public override void Initialize()
  {
    this.state[0] = 2463787394917988140UL;
    this.state[1] = 11481187982095705282UL;
    this.state[2] = 2563595384472711505UL;
    this.state[3] = 10824532655140301501UL;
    this.state[4] = 10819967247969091555UL;
    this.state[5] = 13717434660681038226UL;
    this.state[6] = 3098927326965381290UL;
    this.state[7] = 1060366662362279074UL;
    base.Initialize();
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[32 /*0x20*/];
    fixed (ulong* src = this.state)
      fixed (byte* dest = result)
        Converters.be64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }
}
