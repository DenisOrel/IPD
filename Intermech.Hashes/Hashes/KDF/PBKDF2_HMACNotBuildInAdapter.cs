// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.PBKDF2_HMACNotBuildInAdapter
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Base;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.KDF;

internal class PBKDF2_HMACNotBuildInAdapter : 
  KDFNotBuiltIn,
  IPBKDF2_HMACNotBuiltIn,
  IPBKDF2_HMAC,
  IKDFNotBuiltIn,
  IKDF
{
  private IHMACNotBuiltIn hmacNotBuiltIn;
  private byte[] Password;
  private byte[] Salt;
  private byte[] buffer;
  private uint IterationCount;
  private uint Block;
  private int BlockSize;
  private int startIndex;
  private int endIndex;
  public static readonly string InvalidArgument = "\"bc (ByteCount)\" Argument must be a value greater than zero.";
  public static readonly string InvalidIndex = "Invalid start or end index in the internal buffer.";
  public static readonly string UninitializedInstance = "\"IHash\" instance is uninitialized.";
  public static readonly string EmptyPassword = "Password can't be empty.";
  public static readonly string EmptySalt = "Salt can't be empty.";
  public static readonly string IterationTooSmall = "Iteration must be greater than zero.";

  private PBKDF2_HMACNotBuildInAdapter()
  {
  }

  internal PBKDF2_HMACNotBuildInAdapter(
    IHash a_underlyingHash,
    byte[] a_password,
    byte[] a_salt,
    uint a_iterations)
  {
    if (a_password == null)
      throw new ArgumentNullHashLibException(PBKDF2_HMACNotBuildInAdapter.EmptyPassword);
    if (a_salt == null)
      throw new ArgumentNullHashLibException(PBKDF2_HMACNotBuildInAdapter.EmptySalt);
    if (a_iterations <= 0U)
      throw new ArgumentOutOfRangeHashLibException(PBKDF2_HMACNotBuildInAdapter.IterationTooSmall);
    this.hmacNotBuiltIn = HMACNotBuildInAdapter.CreateHMAC(a_underlyingHash?.Clone() ?? throw new ArgumentNullHashLibException(PBKDF2_HMACNotBuildInAdapter.UninitializedInstance), a_password);
    this.BlockSize = this.hmacNotBuiltIn.HashSize;
    this.buffer = new byte[this.BlockSize];
    this.Password = a_password.DeepCopy();
    this.Salt = a_salt.DeepCopy();
    this.IterationCount = a_iterations;
    this.Initialize();
  }

  public override void Clear()
  {
    ArrayUtils.ZeroFill(ref this.Password);
    ArrayUtils.ZeroFill(ref this.Salt);
  }

  public override byte[] GetBytes(int bc)
  {
    byte[] dest = bc > 0 ? new byte[bc] : throw new ArgumentOutOfRangeHashLibException(PBKDF2_HMACNotBuildInAdapter.InvalidArgument);
    int indexDest = 0;
    int n1 = this.endIndex - this.startIndex;
    if (n1 > 0)
    {
      if (bc >= n1)
      {
        Intermech.Hashes.Utils.Utils.Memmove(ref dest, this.buffer, n1, this.startIndex);
        this.startIndex = 0;
        this.endIndex = 0;
        indexDest += n1;
      }
      else
      {
        Intermech.Hashes.Utils.Utils.Memmove(ref dest, this.buffer, bc, this.startIndex);
        this.startIndex += bc;
        return dest;
      }
    }
    if (this.startIndex != 0 && this.endIndex != 0)
      throw new ArgumentHashLibException(PBKDF2_HMACNotBuildInAdapter.InvalidIndex);
    for (; indexDest < bc; indexDest += this.BlockSize)
    {
      byte[] src = this.Func();
      int num = bc - indexDest;
      if (num > this.BlockSize)
      {
        Intermech.Hashes.Utils.Utils.Memmove(ref dest, src, this.BlockSize, indexDest: indexDest);
      }
      else
      {
        if (num > 0)
          Intermech.Hashes.Utils.Utils.Memmove(ref dest, src, num, indexDest: indexDest);
        int n2 = this.BlockSize - num;
        if (n2 > 0)
          Intermech.Hashes.Utils.Utils.Memmove(ref this.buffer, src, n2, num, this.startIndex);
        this.endIndex += n2;
        this.Initialize();
        return dest;
      }
    }
    this.Initialize();
    return dest;
  }

  public override string Name => $"{this.GetType().Name}({this.hmacNotBuiltIn.Name})";

  public override string ToString() => this.Name;

  public override IKDFNotBuiltIn Clone()
  {
    return (IKDFNotBuiltIn) new PBKDF2_HMACNotBuildInAdapter()
    {
      hmacNotBuiltIn = (IHMACNotBuiltIn) this.hmacNotBuiltIn.Clone(),
      Password = this.Password.DeepCopy(),
      Salt = this.Salt.DeepCopy(),
      buffer = this.buffer.DeepCopy(),
      IterationCount = this.IterationCount,
      Block = this.Block,
      BlockSize = this.BlockSize,
      startIndex = this.startIndex,
      endIndex = this.endIndex
    };
  }

  private void Initialize()
  {
    ArrayUtils.ZeroFill(ref this.buffer);
    this.Block = 1U;
    this.startIndex = 0;
    this.endIndex = 0;
  }

  private byte[] Func()
  {
    byte[] bigEndianBytes = PBKDF2_HMACNotBuildInAdapter.GetBigEndianBytes(this.Block);
    this.hmacNotBuiltIn.Initialize();
    this.hmacNotBuiltIn.TransformBytes(this.Salt, 0, this.Salt.Length);
    this.hmacNotBuiltIn.TransformBytes(bigEndianBytes, 0, bigEndianBytes.Length);
    byte[] bytes = this.hmacNotBuiltIn.TransformFinal().GetBytes();
    byte[] numArray = bytes.DeepCopy();
    uint num = 2;
    for (; num <= this.IterationCount; ++num)
    {
      bytes = this.hmacNotBuiltIn.ComputeBytes(bytes).GetBytes();
      for (int index = 0; index < this.BlockSize; ++index)
        numArray[index] = (byte) ((uint) numArray[index] ^ (uint) bytes[index]);
    }
    ++this.Block;
    return numArray;
  }

  private static byte[] GetBigEndianBytes(uint i)
  {
    byte[] a_Output = new byte[4];
    Converters.ReadUInt32AsBytesBE(i, ref a_Output, 0);
    return a_Output;
  }
}
