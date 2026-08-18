// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Hash32.Murmur2
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;
using System.IO;

#nullable disable
namespace Intermech.Hashes.Hash32;

internal sealed class Murmur2 : 
  MultipleTransformNonBlock,
  IHash32,
  IHash,
  IHashWithKey,
  IWithKey,
  ITransformBlock
{
  private uint key;
  private uint working_key;
  private uint h;
  private static readonly uint CKEY = 0;
  private static readonly uint M = 1540483477;
  private static readonly int R = 24;
  private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";

  public Murmur2()
    : base(4, 4)
  {
  }

  public override IHash Clone()
  {
    Murmur2 murmur2 = new Murmur2();
    murmur2.key = this.key;
    murmur2.working_key = this.working_key;
    murmur2.h = this.h;
    murmur2.Buffer = new MemoryStream();
    byte[] array = this.Buffer.ToArray();
    murmur2.Buffer.Write(array, 0, array.Length);
    murmur2.Buffer.Position = this.Buffer.Position;
    murmur2.BufferSize = this.BufferSize;
    return (IHash) murmur2;
  }

  public override void Initialize()
  {
    this.working_key = this.key;
    base.Initialize();
  }

  protected override IHashResult ComputeAggregatedBytes(byte[] a_data)
  {
    return (IHashResult) new HashResult(this.InternalComputeBytes(a_data));
  }

  private unsafe int InternalComputeBytes(byte[] a_data)
  {
    if (a_data.Empty())
      return 0;
    int length = a_data.Length;
    this.h = this.working_key ^ (uint) length;
    int a_index = 0;
    fixed (byte* a_in = a_data)
    {
      for (; length >= 4; length -= 4)
      {
        this.TransformUInt32Fast(Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, a_index));
        a_index += 4;
      }
      switch (length)
      {
        case 1:
          this.h ^= (uint) a_data[a_index];
          this.h *= Murmur2.M;
          break;
        case 2:
          this.h ^= (uint) a_data[a_index + 1] << 8;
          this.h ^= (uint) a_data[a_index];
          this.h *= Murmur2.M;
          break;
        case 3:
          this.h ^= (uint) a_data[a_index + 2] << 16 /*0x10*/;
          this.h ^= (uint) a_data[a_index + 1] << 8;
          this.h ^= (uint) a_data[a_index];
          this.h *= Murmur2.M;
          break;
      }
    }
    this.h ^= this.h >> 13;
    this.h *= Murmur2.M;
    this.h ^= this.h >> 15;
    return (int) this.h;
  }

  private void TransformUInt32Fast(uint a_data)
  {
    a_data *= Murmur2.M;
    a_data ^= a_data >> Murmur2.R;
    a_data *= Murmur2.M;
    this.h *= Murmur2.M;
    this.h ^= a_data;
  }

  public int? KeyLength => new int?(4);

  public unsafe byte[] Key
  {
    get => Converters.ReadUInt32AsBytesLE(this.key);
    set
    {
      if (value.Empty())
      {
        this.key = Murmur2.CKEY;
      }
      else
      {
        int length = value.Length;
        int? keyLength = this.KeyLength;
        int valueOrDefault = keyLength.GetValueOrDefault();
        if (!(length == valueOrDefault & keyLength.HasValue))
          throw new ArgumentHashLibException(string.Format(Murmur2.InvalidKeyLength, (object) this.KeyLength));
        fixed (byte* a_in = &value[0])
          this.key = Converters.ReadBytesAsUInt32LE((IntPtr) (void*) a_in, 0);
      }
    }
  }
}
