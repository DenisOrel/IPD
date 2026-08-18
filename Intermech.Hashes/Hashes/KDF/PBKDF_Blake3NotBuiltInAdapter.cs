// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.PBKDF_Blake3NotBuiltInAdapter
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Crypto;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.KDF;

internal class PBKDF_Blake3NotBuiltInAdapter : 
  KDFNotBuiltIn,
  IPBKDF_Blake3NotBuiltIn,
  IPBKDF_Blake3,
  IKDFNotBuiltIn,
  IKDF
{
  private byte[] SrcKey;
  private IXOF Xof;
  private const int derivationIVLen = 32 /*0x20*/;
  private const uint flagDeriveKeyContext = 32 /*0x20*/;
  private const uint flagDeriveKeyMaterial = 64 /*0x40*/;

  private PBKDF_Blake3NotBuiltInAdapter()
  {
  }

  ~PBKDF_Blake3NotBuiltInAdapter() => this.Clear();

  internal unsafe PBKDF_Blake3NotBuiltInAdapter(byte[] srcKey, byte[] ctx)
  {
    if (srcKey == null)
      throw new ArgumentNullHashLibException(nameof (srcKey));
    if (ctx == null)
      throw new ArgumentNullHashLibException(nameof (ctx));
    this.SrcKey = srcKey.DeepCopy();
    uint[] a_KeyWords = Blake3.IV.DeepCopy();
    fixed (byte* src = new Blake3(32 /*0x20*/, a_KeyWords, 32U /*0x20*/).ComputeBytes(ctx).GetBytes())
      fixed (uint* dest = a_KeyWords)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, 32 /*0x20*/);
    this.Xof = (IXOF) new Blake3XOF(32 /*0x20*/, a_KeyWords, 64U /*0x40*/);
  }

  public override void Clear() => ArrayUtils.ZeroFill(ref this.SrcKey);

  public override string ToString() => this.Name;

  public override IKDFNotBuiltIn Clone()
  {
    return (IKDFNotBuiltIn) new PBKDF_Blake3NotBuiltInAdapter()
    {
      SrcKey = this.SrcKey.DeepCopy(),
      Xof = (IXOF) this.Xof.Clone()
    };
  }

  public override byte[] GetBytes(int byteCount)
  {
    byte[] destination = new byte[byteCount];
    this.Xof.XOFSizeInBits = (ulong) byteCount * 8UL;
    this.Xof.Initialize();
    this.Xof.TransformBytes(this.SrcKey);
    this.Xof.DoOutput(ref destination, 0UL, (ulong) destination.Length);
    this.Xof.Initialize();
    return destination;
  }

  public override string Name => this.GetType().Name;
}
