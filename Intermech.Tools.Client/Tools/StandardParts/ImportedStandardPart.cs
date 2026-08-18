// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.ImportedStandardPart
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.StandardParts;

internal sealed class ImportedStandardPart
{
  private readonly long modelId;
  private readonly IList<long> articleIds;

  public ImportedStandardPart(long modelId, IList<long> articleIds)
  {
    this.modelId = modelId;
    this.articleIds = articleIds;
  }

  public long ModelId
  {
    [DebuggerStepThrough] get => this.modelId;
  }

  public IList<long> ArticleIds
  {
    [DebuggerStepThrough] get => this.articleIds;
  }
}
