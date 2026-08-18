// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.QuickSearch.ImbaseQuickSearchItem
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Selection;
using Intermech.Interfaces.Compositions;

#nullable disable
namespace Intermech.Imbase.QuickSearch;

public class ImbaseQuickSearchItem(int objTypeId, long objectId, string caption, long recordId = -1) : 
  ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(objectId, objTypeId, caption), recordId)
{
  public int ObjectTypeId => this.ObjectInfo.ItemTypeID;

  public long ObjectId => this.ObjectInfo.ItemID;

  public string Caption => this.ObjectInfo.Caption;
}
