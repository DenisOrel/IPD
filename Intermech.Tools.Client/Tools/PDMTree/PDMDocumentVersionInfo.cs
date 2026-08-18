// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMDocumentVersionInfo
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal class PDMDocumentVersionInfo
{
  public PDMDocumentVersionInfo(long id, DBObjectState dbObjectState, string masterFileName)
  {
    this.Id = id;
    this.DBObjectState = dbObjectState;
    this.MasterFileName = masterFileName;
  }

  public long Id { get; private set; }

  public DBObjectState DBObjectState { get; private set; }

  public string MasterFileName { get; private set; }
}
