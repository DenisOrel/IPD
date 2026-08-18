// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.AsmComponentLocatorData
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Data;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class AsmComponentLocatorData : IDocumentTypesLocatorData
{
  private readonly long articleId;
  private readonly ICollection<int> documentTypes;

  public AsmComponentLocatorData(long articleId, ICollection<int> documentTypes)
  {
    this.articleId = articleId;
    this.documentTypes = documentTypes;
  }

  public long GetArticleId() => this.articleId;

  public ICollection<int> GetDocumentTypes() => this.documentTypes;
}
