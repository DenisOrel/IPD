// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ImbaseKeyInfo
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[Serializable]
public struct ImbaseKeyInfo(long recordId)
{
  public long CatalogId = -1;
  public long FolderId = -1;
  public long LinkId = -1;
  public long TableId = -1;
  public long RecordId = recordId;
  public string CatalogName = string.Empty;
  public string TableName = string.Empty;
}
