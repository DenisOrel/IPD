// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.SimpleExternalKeyLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Data;

public class SimpleExternalKeyLocatorData : IExternalKeyLocatorData
{
  private readonly long documentId;
  private readonly string articleExternalKey;

  public SimpleExternalKeyLocatorData(long documentId, string articleExternalKey)
  {
    this.documentId = documentId;
    this.articleExternalKey = articleExternalKey;
  }

  public long GetDocumentId() => this.documentId;

  public string GetExternalKey() => this.articleExternalKey;
}
