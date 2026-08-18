// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.PBKDF_ScryptNotBuildInAdapter
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Crypto;
using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes;
using System;

#nullable disable
namespace Intermech.Hashes.KDF;

internal class PBKDF_ScryptNotBuildInAdapter : 
  KDFNotBuiltIn,
  IPBKDF_ScryptNotBuiltIn,
  IPBKDF_Scrypt,
  IKDFNotBuiltIn,
  IKDF
{
  private byte[] PasswordBytes;
  private byte[] SaltBytes;
  private int Cost;
  private int BlockSize;
  private int Parallelism;
  public static readonly string InvalidByteCount = "\"(ByteCount)\" Argument must be a value greater than zero.";
  public static readonly string InvalidCost = "Cost parameter must be > 1 and a power of 2.";
  public static readonly string BlockSizeAndCostIncompatible = "Cost parameter must be > 1 and < 65536.";
  public static readonly string BlockSizeTooSmall = "Block size must be >= 1.";
  public static readonly string InvalidParallelism = "Parallelism parameter must be >= 1 and <= {0} (based on block size of {1})";
  public static readonly string RoundsMustBeEven = "Number of Rounds Must be Even";

  private PBKDF_ScryptNotBuildInAdapter()
  {
  }

  internal PBKDF_ScryptNotBuildInAdapter(
    byte[] a_PasswordBytes,
    byte[] a_SaltBytes,
    int a_Cost,
    int a_BlockSize,
    int a_Parallelism)
  {
    PBKDF_ScryptNotBuildInAdapter.ValidatePBKDF_ScryptInputs(a_Cost, a_BlockSize, a_Parallelism);
    this.PasswordBytes = a_PasswordBytes.DeepCopy();
    this.SaltBytes = a_SaltBytes.DeepCopy();
    this.Cost = a_Cost;
    this.BlockSize = a_BlockSize;
    this.Parallelism = a_Parallelism;
  }

  ~PBKDF_ScryptNotBuildInAdapter() => this.Clear();

  public static void ValidatePBKDF_ScryptInputs(int a_Cost, int a_BlockSize, int a_Parallelism)
  {
    if (a_Cost <= 1 || !PBKDF_ScryptNotBuildInAdapter.IsPowerOf2(a_Cost))
      throw new ArgumentHashLibException(PBKDF_ScryptNotBuildInAdapter.InvalidCost);
    if (a_BlockSize == 1 && a_Cost >= 65536 /*0x010000*/)
      throw new ArgumentHashLibException(PBKDF_ScryptNotBuildInAdapter.BlockSizeAndCostIncompatible);
    if (a_BlockSize < 1)
      throw new ArgumentHashLibException(PBKDF_ScryptNotBuildInAdapter.BlockSizeTooSmall);
    int num = int.MaxValue / (128 /*0x80*/ * a_BlockSize * 8);
    if (a_Parallelism < 1 || a_Parallelism > num)
      throw new ArgumentHashLibException(string.Format(PBKDF_ScryptNotBuildInAdapter.InvalidParallelism, (object) num, (object) a_BlockSize));
  }

  public override void Clear()
  {
    ArrayUtils.ZeroFill(ref this.PasswordBytes);
    ArrayUtils.ZeroFill(ref this.SaltBytes);
  }

  public override string Name => this.GetType().Name;

  public override string ToString() => this.Name;

  public override IKDFNotBuiltIn Clone()
  {
    return (IKDFNotBuiltIn) new PBKDF_ScryptNotBuildInAdapter()
    {
      PasswordBytes = this.PasswordBytes.DeepCopy(),
      SaltBytes = this.SaltBytes.DeepCopy(),
      Cost = this.Cost,
      BlockSize = this.BlockSize,
      Parallelism = this.Parallelism
    };
  }

  public override byte[] GetBytes(int ByteCount)
  {
    if (ByteCount <= 0)
      throw new ArgumentHashLibException(PBKDF_ScryptNotBuildInAdapter.InvalidByteCount);
    return PBKDF_ScryptNotBuildInAdapter.MFCrypt(this.PasswordBytes, this.SaltBytes, this.Cost, this.BlockSize, this.Parallelism, ByteCount);
  }

  private static void ClearArray(ref byte[] a_Input) => ArrayUtils.ZeroFill(ref a_Input);

  private static void ClearArray(ref uint[] a_Input) => ArrayUtils.ZeroFill(ref a_Input);

  private static void ClearAllArrays(ref uint[][] a_Inputs)
  {
    for (int index = 0; index < a_Inputs.Length; ++index)
      PBKDF_ScryptNotBuildInAdapter.ClearArray(ref a_Inputs[index]);
  }

  private static bool IsPowerOf2(int x) => x > 0 && (x & x - 1) == 0;

  private static byte[] SingleIterationPBKDF2(
    byte[] a_PasswordBytes,
    byte[] a_SaltBytes,
    int a_OutputLength)
  {
    return new PBKDF2_HMACNotBuildInAdapter((IHash) new SHA2_256(), a_PasswordBytes, a_SaltBytes, 1U).GetBytes(a_OutputLength);
  }

  private static uint Rotl(uint a_Value, int a_Distance) => Bits.RotateLeft32(a_Value, a_Distance);

  private static void SalsaCore(int a_Rounds, uint[] a_Input, ref uint[] x)
  {
    if (a_Input.Length != 16 /*0x10*/)
      throw new ArgumentHashLibException("");
    if (x.Length != 16 /*0x10*/)
      throw new ArgumentHashLibException("");
    if (a_Rounds % 2 != 0)
      throw new ArgumentHashLibException(PBKDF_ScryptNotBuildInAdapter.RoundsMustBeEven);
    uint num1 = a_Input[0];
    uint num2 = a_Input[1];
    uint num3 = a_Input[2];
    uint num4 = a_Input[3];
    uint num5 = a_Input[4];
    uint num6 = a_Input[5];
    uint num7 = a_Input[6];
    uint num8 = a_Input[7];
    uint num9 = a_Input[8];
    uint num10 = a_Input[9];
    uint num11 = a_Input[10];
    uint num12 = a_Input[11];
    uint num13 = a_Input[12];
    uint num14 = a_Input[13];
    uint num15 = a_Input[14];
    uint num16 = a_Input[15];
    for (int index = a_Rounds; index > 0; index -= 2)
    {
      uint num17 = num5 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num1 + num13, 7);
      uint num18 = num9 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num17 + num1, 9);
      uint num19 = num13 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num18 + num17, 13);
      uint num20 = num1 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num19 + num18, 18);
      uint num21 = num10 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num6 + num2, 7);
      uint num22 = num14 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num21 + num6, 9);
      uint num23 = num2 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num22 + num21, 13);
      uint num24 = num6 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num23 + num22, 18);
      uint num25 = num15 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num11 + num7, 7);
      uint num26 = num3 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num25 + num11, 9);
      uint num27 = num7 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num26 + num25, 13);
      uint num28 = num11 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num27 + num26, 18);
      uint num29 = num4 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num16 + num12, 7);
      uint num30 = num8 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num29 + num16, 9);
      uint num31 = num12 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num30 + num29, 13);
      uint num32 = num16 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num31 + num30, 18);
      num2 = num23 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num20 + num29, 7);
      num3 = num26 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num2 + num20, 9);
      num4 = num29 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num3 + num2, 13);
      num1 = num20 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num4 + num3, 18);
      num7 = num27 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num24 + num17, 7);
      num8 = num30 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num7 + num24, 9);
      num5 = num17 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num8 + num7, 13);
      num6 = num24 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num5 + num8, 18);
      num12 = num31 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num28 + num21, 7);
      num9 = num18 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num12 + num28, 9);
      num10 = num21 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num9 + num12, 13);
      num11 = num28 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num10 + num9, 18);
      num13 = num19 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num32 + num25, 7);
      num14 = num22 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num13 + num32, 9);
      num15 = num25 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num14 + num13, 13);
      num16 = num32 ^ PBKDF_ScryptNotBuildInAdapter.Rotl(num15 + num14, 18);
    }
    x[0] = num1 + a_Input[0];
    x[1] = num2 + a_Input[1];
    x[2] = num3 + a_Input[2];
    x[3] = num4 + a_Input[3];
    x[4] = num5 + a_Input[4];
    x[5] = num6 + a_Input[5];
    x[6] = num7 + a_Input[6];
    x[7] = num8 + a_Input[7];
    x[8] = num9 + a_Input[8];
    x[9] = num10 + a_Input[9];
    x[10] = num11 + a_Input[10];
    x[11] = num12 + a_Input[11];
    x[12] = num13 + a_Input[12];
    x[13] = num14 + a_Input[13];
    x[14] = num15 + a_Input[14];
    x[15] = num16 + a_Input[15];
  }

  private static void Xor(uint[] a, uint[] b, int bOff, ref uint[] a_Output)
  {
    for (int index = a_Output.Length - 1; index >= 0; --index)
      a_Output[index] = a[index] ^ b[bOff + index];
  }

  private static void SMix(ref uint[] b, int bOff, int N, int R)
  {
    int n = R * 32 /*0x20*/;
    uint[] X1 = new uint[16 /*0x10*/];
    uint[] X2 = new uint[16 /*0x10*/];
    uint[] numArray1 = new uint[n];
    uint[] numArray2 = new uint[n];
    uint[] src = new uint[N * n];
    try
    {
      Intermech.Hashes.Utils.Utils.Memmove(ref numArray2, b, n, bOff);
      int indexDest1 = 0;
      for (int index = 0; index < N; index += 2)
      {
        Intermech.Hashes.Utils.Utils.Memmove(ref src, numArray2, n, indexDest: indexDest1);
        int indexDest2 = indexDest1 + n;
        PBKDF_ScryptNotBuildInAdapter.BlockMix(numArray2, ref X1, ref X2, ref numArray1, R);
        Intermech.Hashes.Utils.Utils.Memmove(ref src, numArray1, n, indexDest: indexDest2);
        indexDest1 = indexDest2 + n;
        PBKDF_ScryptNotBuildInAdapter.BlockMix(numArray1, ref X1, ref X2, ref numArray2, R);
      }
      uint num1 = (uint) (N - 1);
      for (int index = 0; index < N; ++index)
      {
        int num2 = (int) numArray2[n - 16 /*0x10*/] & (int) num1;
        Intermech.Hashes.Utils.Utils.Memmove(ref numArray1, src, n, num2 * n);
        PBKDF_ScryptNotBuildInAdapter.Xor(numArray1, numArray2, 0, ref numArray1);
        PBKDF_ScryptNotBuildInAdapter.BlockMix(numArray1, ref X1, ref X2, ref numArray2, R);
      }
      Intermech.Hashes.Utils.Utils.Memmove(ref b, numArray2, n, indexDest: bOff);
    }
    finally
    {
      uint[][] a_Inputs = new uint[4][]
      {
        numArray2,
        X1,
        X2,
        numArray1
      };
      PBKDF_ScryptNotBuildInAdapter.ClearArray(ref src);
      PBKDF_ScryptNotBuildInAdapter.ClearAllArrays(ref a_Inputs);
    }
  }

  private static void BlockMix(uint[] b, ref uint[] X1, ref uint[] X2, ref uint[] y, int R)
  {
    Intermech.Hashes.Utils.Utils.Memmove(ref X1, b, 16 /*0x10*/, b.Length - 16 /*0x10*/);
    int bOff = 0;
    int indexDest = 0;
    int num = b.Length / 2;
    for (int index = 2 * R; index > 0; --index)
    {
      PBKDF_ScryptNotBuildInAdapter.Xor(X1, b, bOff, ref X2);
      PBKDF_ScryptNotBuildInAdapter.SalsaCore(8, X2, ref X1);
      Intermech.Hashes.Utils.Utils.Memmove(ref y, X1, 16 /*0x10*/, indexDest: indexDest);
      indexDest = num + bOff - indexDest;
      bOff += 16 /*0x10*/;
    }
  }

  private static void DoSMix(ref uint[] b, int a_Parallelism, int a_Cost, int a_BlockSize)
  {
    for (int index = 0; index < a_Parallelism; ++index)
      PBKDF_ScryptNotBuildInAdapter.SMix(ref b, index * 32 /*0x20*/ * a_BlockSize, a_Cost, a_BlockSize);
  }

  private static unsafe byte[] MFCrypt(
    byte[] a_PasswordBytes,
    byte[] a_SaltBytes,
    int a_Cost,
    int a_BlockSize,
    int a_Parallelism,
    int a_OutputLength)
  {
    uint[] numArray = new uint[0];
    int num = a_BlockSize * 128 /*0x80*/;
    byte[] a_Input = PBKDF_ScryptNotBuildInAdapter.SingleIterationPBKDF2(a_PasswordBytes, a_SaltBytes, a_Parallelism * num);
    try
    {
      numArray = new uint[a_Input.Length / 4];
      fixed (uint* numPtr1 = numArray)
        fixed (byte* numPtr2 = a_Input)
        {
          Converters.le32_copy((IntPtr) (void*) numPtr2, 0, (IntPtr) (void*) numPtr1, 0, a_Input.Length);
          PBKDF_ScryptNotBuildInAdapter.DoSMix(ref numArray, a_Parallelism, a_Cost, a_BlockSize);
          Converters.le32_copy((IntPtr) (void*) numPtr1, 0, (IntPtr) (void*) numPtr2, 0, numArray.Length * 4);
        }
      return PBKDF_ScryptNotBuildInAdapter.SingleIterationPBKDF2(a_PasswordBytes, a_Input, a_OutputLength);
    }
    finally
    {
      PBKDF_ScryptNotBuildInAdapter.ClearArray(ref numArray);
      PBKDF_ScryptNotBuildInAdapter.ClearArray(ref a_Input);
    }
  }
}
