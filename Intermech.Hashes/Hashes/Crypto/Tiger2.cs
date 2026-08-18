// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Tiger2
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal abstract class Tiger2(int a_hash_size, HashRounds a_rounds) : Tiger(a_hash_size, a_rounds)
{
  public static readonly string InvalidTiger2HashSize = "Tiger2 HashSize Must be Either 128 bit(16 byte), 160 bit(20 byte) or 192 bit(24 byte)";

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
