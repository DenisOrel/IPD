// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Blake2SConfigurations.Blake2SIvBuilder
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Hashes.Utils;
using Intermech.Interfaces.Hashes.IBlake2SConfigurations;
using System;

#nullable disable
namespace Intermech.Hashes.Crypto.Blake2SConfigurations;

public sealed class Blake2SIvBuilder
{
  public static readonly string InvalidHashSize = "\"HashSize\" Must Be Greater Than 0 And Less Than or Equal To 32";
  public static readonly string InvalidKeyLength = "\"Key\" Length Must Not Be Greater Than 32";
  public static readonly string InvalidPersonalisationLength = "\"Personalisation\" Length Must Be Equal To 8";
  public static readonly string InvalidSaltLength = "\"Salt\" Length Must Be Equal To 8";
  public static readonly string TreeIncorrectInnerHashSize = "Tree Inner Hash Size Must Not Be Greater Than 32";

  public static unsafe uint[] ConfigS(IBlake2SConfig a_Config, IBlake2STreeConfig a_TreeConfig)
  {
    byte[] numArray1 = new byte[32 /*0x20*/];
    bool a_IsSequential = a_TreeConfig == null;
    if (a_IsSequential)
      a_TreeConfig = Blake2STreeConfig.GetSequentialTreeConfig();
    Blake2SIvBuilder.VerifyConfigS(a_Config, a_TreeConfig, a_IsSequential);
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
      numArray1[8] = (byte) a_TreeConfig.NodeOffset;
      numArray1[9] = (byte) (a_TreeConfig.NodeOffset >> 8);
      numArray1[10] = (byte) (a_TreeConfig.NodeOffset >> 16 /*0x10*/);
      numArray1[11] = (byte) (a_TreeConfig.NodeOffset >> 24);
      numArray1[12] = (byte) (a_TreeConfig.NodeOffset >> 32 /*0x20*/);
      numArray1[13] = (byte) (a_TreeConfig.NodeOffset >> 40);
      numArray1[14] = a_TreeConfig.NodeDepth;
      numArray1[15] = a_TreeConfig.InnerHashSize;
    }
    if (!a_Config.Salt.Empty())
      Intermech.Hashes.Utils.Utils.Memmove(ref numArray1, a_Config.Salt, 8, indexDest: 16 /*0x10*/);
    if (!a_Config.Personalisation.Empty())
      Intermech.Hashes.Utils.Utils.Memmove(ref numArray1, a_Config.Personalisation, 8, indexDest: 24);
    uint[] numArray3;
    fixed (uint* dest = numArray3 = new uint[8])
      fixed (byte* src = numArray1)
        Converters.le32_copy((IntPtr) (void*) src, 0, (IntPtr) (void*) dest, 0, numArray1.Length);
    return numArray3;
  }

  private static void VerifyConfigS(
    IBlake2SConfig a_Config,
    IBlake2STreeConfig a_TreeConfig,
    bool a_IsSequential)
  {
    if (a_Config.HashSize <= 0 || a_Config.HashSize > 32 /*0x20*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2SIvBuilder.InvalidHashSize);
    if (!a_Config.Key.Empty() && a_Config.Key.Length > 32 /*0x20*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2SIvBuilder.InvalidKeyLength);
    if (!a_Config.Salt.Empty() && a_Config.Salt.Length != 8)
      throw new ArgumentOutOfRangeHashLibException(Blake2SIvBuilder.InvalidSaltLength);
    if (!a_Config.Personalisation.Empty() && a_Config.Personalisation.Length != 8)
      throw new ArgumentOutOfRangeHashLibException(Blake2SIvBuilder.InvalidPersonalisationLength);
    if (a_TreeConfig == null)
      return;
    if (a_IsSequential && a_TreeConfig.InnerHashSize != (byte) 0)
      throw new ArgumentOutOfRangeHashLibException("a_TreeConfig.TreeIntermediateHashSize");
    if (a_TreeConfig.InnerHashSize > (byte) 32 /*0x20*/)
      throw new ArgumentOutOfRangeHashLibException(Blake2SIvBuilder.TreeIncorrectInnerHashSize);
  }
}
