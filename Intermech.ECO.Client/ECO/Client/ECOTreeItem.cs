// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOTreeItem
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ECO.Client;

public class ECOTreeItem
{
  private HidingType hidingType = HidingType.CanBeHidden;
  private List<ECOTreeItem> childItems = new List<ECOTreeItem>();
  private ECOTreeItem parentItem;
  private long id;
  private QuickObjectInfo info;
  private string caption;
  private DocumentTreeNode node;

  public ECOTreeItem(long id, string caption, DocumentTreeNode node)
  {
    this.id = id;
    this.caption = caption;
    this.node = node;
    this.Info = new QuickObjectInfo(-1L, "", -1, Guid.Empty, -1L);
  }

  public HidingType HidingType
  {
    get => this.hidingType;
    set => this.hidingType = value;
  }

  public List<ECOTreeItem> ChildItems
  {
    get => this.childItems;
    set => this.childItems = value;
  }

  public ECOTreeItem ParentItem
  {
    get => this.parentItem;
    set => this.parentItem = value;
  }

  public long Id
  {
    get => this.id;
    set => this.id = value;
  }

  public long ObjectId => this.Info.ObjectID;

  public QuickObjectInfo Info
  {
    get => this.info;
    set => this.info = value;
  }

  public string Caption
  {
    get => this.caption;
    set => this.caption = value;
  }

  public DocumentTreeNode Node
  {
    get => this.node;
    set => this.node = value;
  }
}
