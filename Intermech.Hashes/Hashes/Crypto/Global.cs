// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.Crypto.Global
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Hashes.Crypto;

internal static class Global
{
  public static readonly string InvalidHashMode = "Only \"[{0}]\" HashModes are Supported";
  public static readonly string InvalidXOFSize = "XOFSize in Bits must be Multiples of 8 & be Greater than Zero Bytes";
  public static readonly string OutputLengthInvalid = "Output Length is above the Digest Length";
  public static readonly string OutputBufferTooShort = "Output Buffer Too Short";
  public static readonly string WritetoXofAfterReadError = "\"{0}\" Write to Xof after Read not Allowed";
}
