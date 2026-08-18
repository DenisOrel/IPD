// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2BConfigurations.Blake2BIvBuilder
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes.IBlake2BConfigurations;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto.Blake2BConfigurations;

public sealed class Blake2BIvBuilder
{
  public static readonly string InvalidHashSize = "\"HashSize\" Must Be Greater Than 0 And Less Than or Equal To 64";
  public static readonly string InvalidKeyLength = "\"Key\" Length Must Not Be Greater Than 64";
  public static readonly string InvalidPersonalisationLength = "\"Personalisation\" Length Must Be Equal To 16";
  public static readonly string InvalidSaltLength = "\"Salt\" Length Must Be Equal To 16";
  public static readonly string TreeIncorrectInnerHashSize = "Tree Inner Hash Size Must Not Be Greater Than 64";

  public static unsafe ulong[] ConfigB(IBlake2BConfig a_Config, IBlake2BTreeConfig a_TreeConfig)
  {
    byte[] numArray1 = new byte[64 /*0x40*/];
    bool a_IsSequential = a_TreeConfig == null;
    if (a_IsSequential)
      a_TreeConfig = Blake2BTreeConfig.GetSequentialTreeConfig();
    Blake2BIvBuilder.VerifyConfigB(a_Config, a_TreeConfig, a_IsSequential);
    numArray1[0] = (byte) a_Config.HashSize;
    byte[] numArray2 = numArray1;
    byte[] key = a_Config.Key;
    int length = key != null ? (int) (byte) key.Length : 0;
    numArray2[1] = (byte) length;
    if (a_TreeConfig != null)
    {
      numArray1[2] = a_TreeConfig.FanOut;
      numArray1[3] = a_TreeConfig.MaxDepth;
      Converters.ReadUInt32AsBytesLE(a_TreeConfig.LeafSize, ref numArray1, 4);
      Converters.ReadUInt64AsBytesLE(a_TreeConfig.NodeOffset, ref numArray1, 8);
      numArray1[16 /*0x10*/] = a_TreeConfig.NodeDepth;
      numArray1[17] = a_TreeConfig.InnerHashSize;
    }
    if (!a_Config.Salt.Empty())
      Intermech.Hashes.Utils.Utils.Memmove(ref numArray1, a_Config.Salt, 16 /*0x10*/, indexDest: 32 /*0x20*/);
    if (!a_Config.Personalisation.Empty())
      Intermech.Hashes.Utils.Utils.Memmove(ref numArray1, a_Config.Personalisation, 16 /*0x10*/, indexDest: 48 /*0x30*/);
    ulong[] numArray3;
    fixed (ulong* dest = numArray3 = new ulong[8])
      fixed (byte* src = numArray1)
        Converters.le64_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, numArray1.Length);
    return numArray3;
  }

  private static void VerifyConfigB(
    IBlake2BConfig a_Config,
    IBlake2BTreeConfig a_TreeConfig,
    bool a_IsSequential)
  {
    if (a_Config.HashSize <= 0 || a_Config.HashSize > 64 /*0x40*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2BIvBuilder.InvalidHashSize);
    if (!a_Config.Key.Empty() && a_Config.Key.Length > 64 /*0x40*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2BIvBuilder.InvalidKeyLength);
    if (!a_Config.Salt.Empty() && a_Config.Salt.Length != 16 /*0x10*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2BIvBuilder.InvalidSaltLength);
    if (!a_Config.Personalisation.Empty() && a_Config.Personalisation.Length != 16 /*0x10*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2BIvBuilder.InvalidPersonalisationLength);
    if (a_TreeConfig == null)
      return;
    if (a_IsSequential && a_TreeConfig.InnerHashSize != (byte) 0)
      throw new ArgumentOutOfRangeHashLibException("a_TreeConfig.TreeIntermediateHashSize");
    if (a_TreeConfig.InnerHashSize > (byte) 64 /*0x40*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2BIvBuilder.TreeIncorrectInnerHashSize);
  }
}
