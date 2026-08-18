// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash64.Murmur2_64
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;
using System.IO;

#nullable disable
namespace Intermech.Hashes.Hash64;

internal sealed class Murmur2_64 : 
  MultipleTransformNonBlock,
  IHash64,
  IHash,
  IHashWithKey,
  IWithKey,
  ITransformBlock
{
  private ulong key;
  private ulong working_key;
  private static readonly ulong CKEY = 0;
  private static readonly ulong M = 14313749767032793493;
  private static readonly int R = 47;
  private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";

  public Murmur2_64()
    : base(8, 8)
  {
    this.key = Murmur2_64.CKEY;
  }

  public override IHash Clone()
  {
    Murmur2_64 murmur264 = new Murmur2_64();
    murmur264.key = this.key;
    murmur264.working_key = this.working_key;
    murmur264.Buffer = new MemoryStream();
    byte[] array = this.Buffer.ToArray();
    murmur264.Buffer.Write(array, 0, array.Length);
    murmur264.Buffer.Position = this.Buffer.Position;
    murmur264.BufferSize = this.BufferSize;
    return (IHash) murmur264;
  }

  public override void Initialize()
  {
    this.working_key = this.key;
    base.Initialize();
  }

  protected override unsafe IHashResult ComputeAggregatedBytes(byte[] a_data)
  {
    if (a_data.Empty())
      return (IHashResult) new HashResult(0UL);
    int length = a_data.Length;
    ulong a_hash;
    fixed (byte* a_in = a_data)
    {
      ulong num1 = this.working_key ^ (ulong) length * Murmur2_64.M;
      int a_index = 0;
      for (; length >= 8; length -= 8)
      {
        ulong num2 = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, a_index) * Murmur2_64.M;
        ulong num3 = (num2 ^ num2 >> Murmur2_64.R) * Murmur2_64.M;
        num1 = (num1 ^ num3) * Murmur2_64.M;
        a_index += 8;
      }
      switch (length)
      {
        case 1:
          num1 = (num1 ^ (ulong) a_data[a_index]) * Murmur2_64.M;
          break;
        case 2:
          num1 = (num1 ^ (ulong) a_data[a_index + 1] << 8 ^ (ulong) a_data[a_index]) * Murmur2_64.M;
          break;
        case 3:
          num1 = (num1 ^ (ulong) a_data[a_index + 2] << 16 /*0x10*/ ^ (ulong) a_data[a_index + 1] << 8 ^ (ulong) a_data[a_index]) * Murmur2_64.M;
          break;
        case 4:
          num1 = (num1 ^ (ulong) a_data[a_index + 3] << 24 ^ (ulong) a_data[a_index + 2] << 16 /*0x10*/ ^ (ulong) a_data[a_index + 1] << 8 ^ (ulong) a_data[a_index]) * Murmur2_64.M;
          break;
        case 5:
          num1 = (num1 ^ (ulong) a_data[a_index + 4] << 32 /*0x20*/ ^ (ulong) a_data[a_index + 3] << 24 ^ (ulong) a_data[a_index + 2] << 16 /*0x10*/ ^ (ulong) a_data[a_index + 1] << 8 ^ (ulong) a_data[a_index]) * Murmur2_64.M;
          break;
        case 6:
          num1 = (num1 ^ (ulong) a_data[a_index + 5] << 40 ^ (ulong) a_data[a_index + 4] << 32 /*0x20*/ ^ (ulong) a_data[a_index + 3] << 24 ^ (ulong) a_data[a_index + 2] << 16 /*0x10*/ ^ (ulong) a_data[a_index + 1] << 8 ^ (ulong) a_data[a_index]) * Murmur2_64.M;
          break;
        case 7:
          num1 = (num1 ^ (ulong) a_data[a_index + 6] << 48 /*0x30*/ ^ (ulong) a_data[a_index + 5] << 40 ^ (ulong) a_data[a_index + 4] << 32 /*0x20*/ ^ (ulong) a_data[a_index + 3] << 24 ^ (ulong) a_data[a_index + 2] << 16 /*0x10*/ ^ (ulong) a_data[a_index + 1] << 8 ^ (ulong) a_data[a_index]) * Murmur2_64.M;
          break;
      }
      ulong num4 = (num1 ^ num1 >> Murmur2_64.R) * Murmur2_64.M;
      a_hash = num4 ^ num4 >> Murmur2_64.R;
    }
    return (IHashResult) new HashResult(a_hash);
  }

  public int? KeyLength => new int?(8);

  public unsafe byte[] Key
  {
    get => Converters.ReadUInt64AsBytesLE(this.key);
    set
    {
      if (value.Empty())
      {
        this.key = Murmur2_64.CKEY;
      }
      else
      {
        int length = value.Length;
        int? keyLength = this.KeyLength;
        int valueOrDefault = keyLength.GetValueOrDefault();
        if (!(length == valueOrDefault & keyLength.HasValue))
          throw new ArgumentHashLibException(string.Format(Murmur2_64.InvalidKeyLength, (object) this.KeyLength));
        fixed (byte* a_in = &value[0])
          this.key = Converters.ReadBytesAsUInt64LE((IntPtr) (void*) a_in, 0);
      }
    }
  }
}
