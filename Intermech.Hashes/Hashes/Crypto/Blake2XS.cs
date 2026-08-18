// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2XS
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Crypto.Blake2SConfigurations;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using Intermech.Interfaces.Hashes.IBlake2SConfigurations;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal sealed class Blake2XS : Blake2S, IXOF, IHash
{
  private IBlake2XSConfig Blake2XSConfig;
  private ulong DigestPosition;
  private IBlake2XSConfig RootConfig;
  private IBlake2XSConfig OutputConfig;
  private byte[] RootHashDigest;
  private byte[] Blake2XSBuffer;
  private bool Finalized;
  private ulong _XofSizeInBits;
  private const int Blake2SHashSize = 32 /*0x20*/;
  private const ushort UnknownDigestLengthInBytes = 65535 /*0xFFFF*/;
  private const ulong MaxNumberBlocks = 4294967296 /*0x0100000000*/;
  private const ulong UnknownMaxDigestLengthInBytes = 137438953472 /*0x2000000000*/;

  public ulong XOFSizeInBits
  {
    get => this._XofSizeInBits;
    set => this.SetXOFSizeInBitsInternal(value);
  }

  public override string Name => this.GetType().Name;

  public unsafe void DoOutput(
    ref byte[] a_destination,
    ulong a_destinationOffset,
    ulong a_outputLength)
  {
    if ((ulong) a_destination.Length - a_destinationOffset < a_outputLength)
      throw new ArgumentOutOfRangeHashLibException(Blake2S.OutputBufferTooShort);
    if (this.XOFSizeInBits >> 3 != (ulong) ushort.MaxValue)
    {
      if (this.DigestPosition + a_outputLength > this.XOFSizeInBits >> 3)
        throw new ArgumentOutOfRangeHashLibException(Blake2S.OutputLengthInvalid);
    }
    else if (this.DigestPosition == 137438953472UL /*0x2000000000*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2S.MaximumOutputLengthExceeded);
    if (!this.Finalized)
    {
      this.Finish();
      this.Finalized = true;
    }
    if (this.RootHashDigest.Empty())
    {
      this.RootHashDigest = new byte[32 /*0x20*/];
      fixed (uint* src = this.State)
        fixed (byte* dest = this.RootHashDigest)
          Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, this.RootHashDigest.Length);
    }
    while (a_outputLength > 0UL)
    {
      if (((long) this.DigestPosition & 31L /*0x1F*/) == 0L)
      {
        this.OutputConfig.Blake2SConfig.HashSize = this.ComputeStepLength();
        this.OutputConfig.Blake2STreeConfig.InnerHashSize = (byte) 32 /*0x20*/;
        this.Blake2XSBuffer = new Blake2S(this.OutputConfig.Blake2SConfig, this.OutputConfig.Blake2STreeConfig).ComputeBytes(this.RootHashDigest).GetBytes();
        ++this.OutputConfig.Blake2STreeConfig.NodeOffset;
      }
      ulong indexSrc = this.DigestPosition & 31UL /*0x1F*/;
      ulong val2 = (ulong) this.Blake2XSBuffer.Length - indexSrc;
      ulong n = Math.Min(a_outputLength, val2);
      Intermech.Hashes.Utils.Utils.Memmove(ref a_destination, this.Blake2XSBuffer, (int) n, (int) indexSrc, (int) a_destinationOffset);
      a_outputLength -= n;
      a_destinationOffset += n;
      this.DigestPosition += n;
    }
  }

  public Blake2XS(IBlake2XSConfig a_Blake2XSConfig)
  {
    this.Blake2XSConfig = a_Blake2XSConfig;
    this.RootConfig = (IBlake2XSConfig) new Intermech.Hashes.Crypto.Blake2SConfigurations.Blake2XSConfig();
    this.RootConfig.Blake2SConfig = this.Blake2XSConfig.Blake2SConfig;
    if (this.RootConfig.Blake2SConfig == null)
    {
      this.RootConfig.Blake2SConfig = (IBlake2SConfig) new Blake2SConfig();
    }
    else
    {
      this.RootConfig.Blake2SConfig.Key = this.Blake2XSConfig.Blake2SConfig.Key;
      this.RootConfig.Blake2SConfig.Salt = this.Blake2XSConfig.Blake2SConfig.Salt;
      this.RootConfig.Blake2SConfig.Personalisation = this.Blake2XSConfig.Blake2SConfig.Personalisation;
    }
    this.RootConfig.Blake2STreeConfig = this.Blake2XSConfig.Blake2STreeConfig;
    if (this.RootConfig.Blake2STreeConfig == null)
    {
      this.RootConfig.Blake2STreeConfig = (IBlake2STreeConfig) new Blake2STreeConfig();
      this.RootConfig.Blake2STreeConfig.FanOut = (byte) 1;
      this.RootConfig.Blake2STreeConfig.MaxDepth = (byte) 1;
      this.RootConfig.Blake2STreeConfig.LeafSize = 0U;
      this.RootConfig.Blake2STreeConfig.NodeOffset = 0UL;
      this.RootConfig.Blake2STreeConfig.NodeDepth = (byte) 0;
      this.RootConfig.Blake2STreeConfig.InnerHashSize = (byte) 0;
      this.RootConfig.Blake2STreeConfig.IsLastNode = false;
    }
    this.OutputConfig = (IBlake2XSConfig) new Intermech.Hashes.Crypto.Blake2SConfigurations.Blake2XSConfig();
    this.OutputConfig.Blake2SConfig = (IBlake2SConfig) new Blake2SConfig();
    this.OutputConfig.Blake2SConfig.Salt = this.RootConfig.Blake2SConfig.Salt;
    this.OutputConfig.Blake2SConfig.Personalisation = this.RootConfig.Blake2SConfig.Personalisation;
    this.OutputConfig.Blake2STreeConfig = (IBlake2STreeConfig) new Blake2STreeConfig();
    this.Config = this.RootConfig.Blake2SConfig;
    this.TreeConfig = this.RootConfig.Blake2STreeConfig;
    this.HashSize = this.Config.HashSize;
    this.Blake2XSBuffer = new byte[32 /*0x20*/];
  }

  public override void Initialize()
  {
    ulong a_XOFSizeInBytes = this.XOFSizeInBits >> 3;
    this.RootConfig.Blake2STreeConfig.NodeOffset = this.NodeOffsetWithXOFDigestLength(a_XOFSizeInBytes);
    this.OutputConfig.Blake2STreeConfig.NodeOffset = this.NodeOffsetWithXOFDigestLength(a_XOFSizeInBytes);
    this.RootHashDigest = (byte[]) null;
    this.DigestPosition = 0UL;
    this.Finalized = false;
    ArrayUtils.ZeroFill(ref this.Blake2XSBuffer);
    base.Initialize();
  }

  public override IHash Clone()
  {
    Blake2XS blake2Xs1 = new Blake2XS(this.Blake2XSConfig);
    blake2Xs1.XOFSizeInBits = this.XOFSizeInBits;
    Blake2XS blake2Xs2 = blake2Xs1 as Blake2XS;
    blake2Xs2.Blake2XSConfig = this.Blake2XSConfig.Clone();
    blake2Xs2.DigestPosition = this.DigestPosition;
    blake2Xs2.RootConfig = this.RootConfig.Clone();
    blake2Xs2.OutputConfig = this.OutputConfig.Clone();
    blake2Xs2.Finalized = this.Finalized;
    blake2Xs2.RootHashDigest = this.RootHashDigest.DeepCopy();
    blake2Xs2.Blake2XSBuffer = this.Blake2XSBuffer.DeepCopy();
    blake2Xs2.M = this.M.DeepCopy();
    blake2Xs2.State = this.State.DeepCopy();
    blake2Xs2.Buffer = this.Buffer.DeepCopy();
    blake2Xs2.FilledBufferCount = this.FilledBufferCount;
    blake2Xs2.Counter0 = this.Counter0;
    blake2Xs2.Counter1 = this.Counter1;
    blake2Xs2.FinalizationFlag0 = this.FinalizationFlag0;
    blake2Xs2.FinalizationFlag1 = this.FinalizationFlag1;
    blake2Xs2.BufferSize = this.BufferSize;
    return (IHash) blake2Xs2;
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    if (this.Finalized)
      throw new InvalidOperationHashLibException(string.Format(Blake2S.WritetoXofAfterReadError, (object) this.Name));
    base.TransformBytes(a_data, a_index, a_length);
  }

  public override IHashResult TransformFinal()
  {
    HashResult hashResult = new HashResult(this.GetResult());
    this.Initialize();
    return (IHashResult) hashResult;
  }

  private IXOF SetXOFSizeInBitsInternal(ulong a_XofSizeInBits)
  {
    ulong num = a_XofSizeInBits >> 3;
    this._XofSizeInBits = ((long) a_XofSizeInBits & 7L) == 0L && num >= 1UL && num <= (ulong) ushort.MaxValue ? a_XofSizeInBits : throw new ArgumentInvalidHashLibException(string.Format(Blake2S.InvalidXOFSize, (object) 1, (object) (ulong) ushort.MaxValue));
    return (IXOF) this;
  }

  private ulong NodeOffsetWithXOFDigestLength(ulong a_XOFSizeInBytes)
  {
    return a_XOFSizeInBytes << 32 /*0x20*/;
  }

  private int ComputeStepLength()
  {
    long num = (long) (this.XOFSizeInBits >> 3);
    ulong val2 = (ulong) num - this.DigestPosition;
    return num == (long) ushort.MaxValue ? 32 /*0x20*/ : (int) Math.Min(32UL /*0x20*/, val2);
  }

  private byte[] GetResult()
  {
    ulong a_outputLength = this.XOFSizeInBits >> 3;
    byte[] a_destination = new byte[a_outputLength];
    this.DoOutput(ref a_destination, 0UL, a_outputLength);
    return a_destination;
  }
}
