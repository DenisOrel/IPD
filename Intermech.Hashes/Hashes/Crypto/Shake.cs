// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Shake
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal abstract class Shake : SHA3, IXOF, IHash
{
  private ulong xofSizeInBits;
  protected ulong BufferPosition;
  protected ulong DigestPosition;
  protected ulong ShakeBufferPosition;
  protected byte[] ShakeBuffer;
  protected bool Finalized;

  protected Shake(int a_hash_size)
    : base(a_hash_size)
  {
    this.ShakeBuffer = new byte[8];
    this.hash_mode = HashMode.Shake;
    this.Finalized = false;
  }

  public override void Initialize()
  {
    base.Initialize();
    this.BufferPosition = 0UL;
    this.DigestPosition = 0UL;
    this.ShakeBufferPosition = 8UL;
    this.Finalized = false;
    ArrayUtils.ZeroFill(ref this.ShakeBuffer);
  }

  public override IHashResult TransformFinal()
  {
    byte[] result = this.GetResult();
    this.Initialize();
    return (IHashResult) new HashResult(result);
  }

  protected override byte[] GetResult()
  {
    ulong a_outputLength = this.XOFSizeInBits >> 3;
    byte[] a_destination = new byte[a_outputLength];
    this.DoOutput(ref a_destination, 0UL, a_outputLength);
    return a_destination;
  }

  private IXOF SetXOFSizeInBitsInternal(ulong a_XOFSizeInBits)
  {
    ulong num = a_XOFSizeInBits >> 3;
    this.xofSizeInBits = ((long) a_XOFSizeInBits & 7L) == 0L && num >= 1UL ? a_XOFSizeInBits : throw new ArgumentInvalidHashLibException(Global.InvalidXOFSize);
    return (IXOF) this;
  }

  public virtual ulong XOFSizeInBits
  {
    get => this.xofSizeInBits;
    set => this.SetXOFSizeInBitsInternal(value);
  }

  public virtual void DoOutput(
    ref byte[] a_destination,
    ulong a_destinationOffset,
    ulong a_outputLength)
  {
    if ((ulong) a_destination.Length - a_destinationOffset < a_outputLength)
      throw new ArgumentOutOfRangeHashLibException(Global.OutputBufferTooShort);
    if (this.DigestPosition + a_outputLength > this.XOFSizeInBits >> 3)
      throw new ArgumentOutOfRangeHashLibException(Global.OutputLengthInvalid);
    if (!this.Finalized)
    {
      this.Finish();
      this.Finalized = true;
    }
    ulong index = a_destinationOffset;
    while (a_outputLength > 0UL)
    {
      if (this.ShakeBufferPosition >= 8UL)
      {
        if (this.BufferPosition * 8UL >= (ulong) this.BlockSize)
        {
          this.KeccakF1600_StatePermute();
          this.BufferPosition = 0UL;
        }
        Converters.ReadUInt64AsBytesLE(this.state[this.BufferPosition], ref this.ShakeBuffer, 0);
        ++this.BufferPosition;
        this.ShakeBufferPosition = 0UL;
      }
      a_destination[index] = this.ShakeBuffer[this.ShakeBufferPosition];
      ++this.ShakeBufferPosition;
      --a_outputLength;
      ++this.DigestPosition;
      ++index;
    }
  }

  public override void TransformBytes(byte[] a_data, int a_index, int a_length)
  {
    if (this.Finalized)
      throw new InvalidOperationHashLibException(string.Format(Global.WritetoXofAfterReadError, (object) this.Name));
    base.TransformBytes(a_data, a_index, a_length);
  }
}
