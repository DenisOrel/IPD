// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.CShake
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;

#nullable disable
namespace Intermech.Hashes.Crypto;

internal abstract class CShake : Shake
{
  protected byte[] FN;
  protected byte[] FS;
  protected byte[] InitBlock;

  protected CShake(int a_hash_size, byte[] N, byte[] S)
    : base(a_hash_size)
  {
    this.FN = N.DeepCopy();
    this.FS = S.DeepCopy();
    this.InitBlock = (byte[]) null;
    if (this.FN.Empty() && this.FS.Empty())
    {
      this.hash_mode = HashMode.Shake;
    }
    else
    {
      this.hash_mode = HashMode.CShake;
      this.InitBlock = Intermech.Hashes.Utils.Utils.Concat(CShake.EncodeString(N), CShake.EncodeString(S));
    }
  }

  private static byte[] LeftEncode(ulong a_input)
  {
    byte num = 1;
    for (ulong index = a_input >> 8; index != 0UL; index >>= 8)
      ++num;
    byte[] numArray = new byte[(int) num + 1];
    numArray[0] = num;
    for (int index = 1; index <= (int) num; ++index)
      numArray[index] = (byte) (a_input >> 8 * ((int) num - index));
    return numArray;
  }

  public override void Initialize()
  {
    base.Initialize();
    if (this.InitBlock == null || this.InitBlock.Length == 0)
      return;
    this.TransformBytes(CShake.BytePad(this.InitBlock, this.BlockSize));
  }

  public static byte[] RightEncode(ulong a_input)
  {
    byte index1 = 1;
    for (ulong index2 = a_input >> 8; index2 != 0UL; index2 >>= 8)
      ++index1;
    byte[] numArray = new byte[(int) index1 + 1];
    numArray[(int) index1] = index1;
    for (int index3 = 1; index3 <= (int) index1; ++index3)
      numArray[index3 - 1] = (byte) (a_input >> 8 * ((int) index1 - index3));
    return numArray;
  }

  public static byte[] BytePad(byte[] a_input, int AW)
  {
    byte[] x = Intermech.Hashes.Utils.Utils.Concat(CShake.LeftEncode((ulong) AW), a_input);
    int length = AW - x.Length % AW;
    return Intermech.Hashes.Utils.Utils.Concat(x, new byte[length]);
  }

  public static byte[] EncodeString(byte[] a_input)
  {
    return a_input.Empty() ? CShake.LeftEncode(0UL) : Intermech.Hashes.Utils.Utils.Concat(CShake.LeftEncode((ulong) a_input.Length * 8UL), a_input);
  }
}
