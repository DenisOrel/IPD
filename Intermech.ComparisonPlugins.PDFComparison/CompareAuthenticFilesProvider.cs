// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.CompareAuthenticFilesProvider
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using Intermech.ComparisonPlugins.PDFComparison.Common;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison;

public class CompareAuthenticFilesProvider : ComparisonProvider
{
  public override FileDescription SelectFirstComparedFile()
  {
    return ClientUtils.FindAuthenticObjectFile(this._firstComparedVersion);
  }

  public override FileDescription SelectSecondComparedFile()
  {
    return ClientUtils.FindAuthenticObjectFile(this._secondComparedVersion);
  }
}
