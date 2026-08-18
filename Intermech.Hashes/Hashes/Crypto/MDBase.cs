// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.MDBase
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal abstract class MDBase : BlockHash, ICryptoNotBuiltIn, ICrypto, IHash
{
  protected static readonly uint C1 = 1352829926;
  protected static readonly uint C2 = 1518500249;
  protected static readonly uint C3 = 1548603684;
  protected static readonly uint C4 = 1859775393;
  protected static readonly uint C5 = 1836072691;
  protected static readonly uint C6 = 2400959708;
  protected static readonly uint C7 = 2053994217;
  protected static readonly uint C8 = 2840853838;
  protected uint[] state;

  public MDBase(int a_state_length, int a_hash_size)
    : base(a_hash_size, 64 /*0x40*/)
  {
    this.state = new uint[a_state_length];
  }

  protected override unsafe byte[] GetResult()
  {
    byte[] result = new byte[this.state.Length * 4];
    fixed (uint* src = this.state)
      fixed (byte* dest = result)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, result.Length);
    return result;
  }

  public override void Initialize()
  {
    this.state[0] = 1732584193U;
    this.state[1] = 4023233417U;
    this.state[2] = 2562383102U;
    this.state[3] = 271733878U;
    base.Initialize();
  }

  protected override void Finish()
  {
    long x = (long) this.processed_bytes * 8L;
    int a_index = this.buffer.Position >= 56 ? 120 - this.buffer.Position : 56 - this.buffer.Position;
    byte[] a_out = new byte[a_index + 8];
    a_out[0] = (byte) 128 /*0x80*/;
    Converters.ReadUInt64AsBytesLE(Converters.le2me_64((ulong) x), ref a_out, a_index);
    int a_length = a_index + 8;
    this.TransformBytes(a_out, 0, a_length);
  }
}
