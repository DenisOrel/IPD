// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Checksum.CRC16
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Checksum;

internal class CRC16 : Hash, IChecksum, IBlockHash, IHash, IHash16, ITransformBlock
{
  private ICRC CRCAlgorithm;

  public CRC16(
    ulong _poly,
    ulong _Init,
    bool _refIn,
    bool _refOut,
    ulong _XorOut,
    ulong _check,
    string[] _Names)
    : base(2, 1)
  {
    this.CRCAlgorithm = (ICRC) new CRC(16 /*0x10*/, _poly, _Init, _refIn, _refOut, _XorOut, _check, _Names);
  }

  public override void Initialize() => this.CRCAlgorithm.Initialize();

  public override IHashResult TransformFinal() => this.CRCAlgorithm.TransformFinal();

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    this.CRCAlgorithm.TransformBytes(a_data, a_index, a_length);
  }
}
