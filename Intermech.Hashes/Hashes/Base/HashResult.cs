// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Base.HashResult
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.Base;

public sealed class HashResult : IHashResult
{
  private byte[] hash;
  private static readonly string ImpossibleRepresentationInt32 = "Current Data Structure cannot be Represented as an 'Int32' Type.";
  private static readonly string ImpossibleRepresentationUInt8 = "Current Data Structure cannot be Represented as an 'UInt8' Type.";
  private static readonly string ImpossibleRepresentationUInt16 = "Current Data Structure cannot be Represented as an 'UInt16' Type.";
  private static readonly string ImpossibleRepresentationUInt32 = "Current Data Structure cannot be Represented as an 'UInt32' Type.";
  private static readonly string ImpossibleRepresentationUInt64 = "Current Data Structure cannot be Represented as an 'UInt64' Type.";

  public HashResult() => this.hash = new byte[0];

  public HashResult(ulong a_hash)
  {
    this.hash = new byte[8];
    this.hash[0] = (byte) (a_hash >> 56);
    this.hash[1] = (byte) (a_hash >> 48 /*0x30*/);
    this.hash[2] = (byte) (a_hash >> 40);
    this.hash[3] = (byte) (a_hash >> 32 /*0x20*/);
    this.hash[4] = (byte) (a_hash >> 24);
    this.hash[5] = (byte) (a_hash >> 16 /*0x10*/);
    this.hash[6] = (byte) (a_hash >> 8);
    this.hash[7] = (byte) a_hash;
  }

  public HashResult(byte[] a_hash) => this.hash = a_hash.DeepCopy();

  public HashResult(uint a_hash)
  {
    this.hash = new byte[4];
    this.hash[0] = (byte) (a_hash >> 24);
    this.hash[1] = (byte) (a_hash >> 16 /*0x10*/);
    this.hash[2] = (byte) (a_hash >> 8);
    this.hash[3] = (byte) a_hash;
  }

  public HashResult(byte a_hash)
  {
    this.hash = new byte[1];
    this.hash[0] = a_hash;
  }

  public HashResult(ushort a_hash)
  {
    this.hash = new byte[2];
    this.hash[0] = (byte) ((uint) a_hash >> 8);
    this.hash[1] = (byte) a_hash;
  }

  public HashResult(int a_hash)
  {
    this.hash = new byte[4];
    this.hash[0] = (byte) Bits.Asr32(a_hash, 24);
    this.hash[1] = (byte) Bits.Asr32(a_hash, 16 /*0x10*/);
    this.hash[2] = (byte) Bits.Asr32(a_hash, 8);
    this.hash[3] = (byte) a_hash;
  }

  public HashResult(HashResult right) => this.hash = right.hash.DeepCopy();

  public bool CompareTo(IHashResult a_hashResult)
  {
    return HashResult.SlowEquals(a_hashResult.GetBytes(), this.hash);
  }

  public byte[] GetBytes() => this.hash.DeepCopy();

  public override int GetHashCode()
  {
    string base64String = Convert.ToBase64String(this.hash);
    uint a_value = 0;
    int index = 0;
    for (int length = base64String.Length; index < length; ++index)
      a_value = Bits.RotateLeft32(a_value, 5) ^ (uint) base64String[index];
    return (int) a_value;
  }

  public int GetInt32()
  {
    if (this.hash.Length != 4)
      throw new InvalidOperationHashLibException(HashResult.ImpossibleRepresentationInt32);
    return (int) this.hash[0] << 24 | (int) this.hash[1] << 16 /*0x10*/ | (int) this.hash[2] << 8 | (int) this.hash[3];
  }

  public byte GetUInt8()
  {
    return this.hash.Length == 1 ? this.hash[0] : throw new InvalidOperationHashLibException(HashResult.ImpossibleRepresentationUInt8);
  }

  public ushort GetUInt16()
  {
    if (this.hash.Length != 2)
      throw new InvalidOperationHashLibException(HashResult.ImpossibleRepresentationUInt16);
    return (ushort) ((uint) this.hash[0] << 8 | (uint) this.hash[1]);
  }

  public uint GetUInt32()
  {
    if (this.hash.Length != 4)
      throw new InvalidOperationHashLibException(HashResult.ImpossibleRepresentationUInt32);
    return (uint) ((int) this.hash[0] << 24 | (int) this.hash[1] << 16 /*0x10*/ | (int) this.hash[2] << 8) | (uint) this.hash[3];
  }

  public ulong GetUInt64()
  {
    if (this.hash.Length != 8)
      throw new InvalidOperationHashLibException(HashResult.ImpossibleRepresentationUInt64);
    return (ulong) ((long) this.hash[0] << 56 | (long) this.hash[1] << 48 /*0x30*/ | (long) this.hash[2] << 40 | (long) this.hash[3] << 32 /*0x20*/ | (long) this.hash[4] << 24 | (long) this.hash[5] << 16 /*0x10*/ | (long) this.hash[6] << 8) | (ulong) this.hash[7];
  }

  private static bool SlowEquals(byte[] a_ar1, byte[] a_ar2)
  {
    int? nullable1 = a_ar1?.Length;
    int? nullable2 = a_ar2?.Length;
    uint num1 = (uint) (nullable1.HasValue & nullable2.HasValue ? new int?(nullable1.GetValueOrDefault() ^ nullable2.GetValueOrDefault()) : new int?()).Value;
    uint index = 0;
    while (true)
    {
      long num2 = (long) index;
      int? nullable3;
      if (a_ar1 == null)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new int?(a_ar1.Length - 1);
      nullable1 = nullable3;
      long? nullable4 = nullable1.HasValue ? new long?((long) nullable1.GetValueOrDefault()) : new long?();
      long valueOrDefault1 = nullable4.GetValueOrDefault();
      if (num2 <= valueOrDefault1 & nullable4.HasValue)
      {
        long num3 = (long) index;
        int? nullable5;
        if (a_ar2 == null)
        {
          nullable2 = new int?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new int?(a_ar2.Length - 1);
        nullable1 = nullable5;
        nullable4 = nullable1.HasValue ? new long?((long) nullable1.GetValueOrDefault()) : new long?();
        long valueOrDefault2 = nullable4.GetValueOrDefault();
        if (num3 <= valueOrDefault2 & nullable4.HasValue)
        {
          num1 |= (uint) a_ar1[(int) index] ^ (uint) a_ar2[(int) index];
          ++index;
        }
        else
          break;
      }
      else
        break;
    }
    return num1 == 0U;
  }

  public string ToString(bool a_group = false)
  {
    return Converters.ConvertBytesToHexString(this.hash, a_group);
  }
}
