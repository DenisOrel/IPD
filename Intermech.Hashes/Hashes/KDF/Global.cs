// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.Global
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

#nullable disable
namespace Intermech.Hashes.KDF;

internal static class Global
{
  public static readonly string InvalidOutputByteCount = "\"(ByteCount)\" Argument Less Than \"{0}\".";
  public static readonly string BlockInstanceNotInitialized = "Block Instance not Initialized";
  public static readonly string InputLengthInvalid = "Input Length \"{0}\" is not Equal to BlockSize \"{1}\"";
  public static readonly string LanesTooSmall = "Lanes Must be Greater Than \"{0}\"";
  public static readonly string LanesTooBig = "Lanes Must be Less Than \"{0}\"";
  public static readonly string MemoryTooSmall = "Memory is Less Than: \"{0}\", Expected \"{1}\"";
  public static readonly string IterationsTooSmall = "Iterations is Less Than: \"{0}\"";
  public static readonly string Argon2ParameterBuilderNotInitialized = "Argon2 Parameter Builder Not Initialized";
}
