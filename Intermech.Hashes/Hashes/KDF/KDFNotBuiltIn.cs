// Decompiled with JetBrains decompiler
// Type: Intermech.Hashes.KDF.KDFNotBuiltIn
// Assembly: Intermech.Hashes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF8FF682-430C-4EFD-AB43-3888B8E59961
// Assembly location: D:\IPS\Client\Intermech.Hashes.dll

using Intermech.Interfaces.Hashes;

#nullable disable
namespace Intermech.Hashes.KDF;

internal abstract class KDFNotBuiltIn : IKDFNotBuiltIn, IKDF
{
  public abstract string Name { get; }

  public abstract override string ToString();

  public abstract IKDFNotBuiltIn Clone();

  public abstract void Clear();

  public abstract byte[] GetBytes(int bc);
}
