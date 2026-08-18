// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionImbaseLink
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionImbaseLink
{
  private long _catalogId;
  private long _folderId;
  private long _tableRecId;

  public AutoSelectionImbaseLink(long catalogId, long folderId, long tableRecId)
  {
    this._catalogId = catalogId;
    this._folderId = folderId;
    this._tableRecId = tableRecId;
  }

  public long CatalogID
  {
    get => this._catalogId;
    set => this._catalogId = value;
  }

  public long FolderID
  {
    get => this._folderId;
    set => this._folderId = value;
  }

  public long TableRecID
  {
    get => this._tableRecId;
    set => this._tableRecId = value;
  }
}
