// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.Versions.TechObjInfoItemVersionsProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders.Versions;

internal class TechObjInfoItemVersionsProvider : 
  ITechCardDataEnumerableProvider<ObjInfoIDItem>,
  ITechCardDataProvider<IEnumerable<ObjInfoIDItem>>
{
  /// <summary>
  /// 
  /// </summary>
  private readonly ObjInfoItem _objInfoItem;

  /// <summary>Конструктор</summary>
  /// <param name="objIntoItem"></param>
  public TechObjInfoItemVersionsProvider([NotNull] ObjInfoItem objIntoItem)
  {
    this._objInfoItem = objIntoItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerable<ObjInfoIDItem> Execute()
  {
    List<ObjInfoIDItem> objInfoIdItemList = new List<ObjInfoIDItem>();
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._objInfoItem))
      return (IEnumerable<ObjInfoIDItem>) objInfoIdItemList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable allObjectVersions = sessionKeeper.Session.GetAllObjectVersions(this._objInfoItem.ObjectID, false, false, false, "F_OBJECT_ID", "F_OBJECT_TYPE", "F_ID");
      if (allObjectVersions == null)
        return (IEnumerable<ObjInfoIDItem>) objInfoIdItemList;
      foreach (DataRow row in (InternalDataCollectionBase) allObjectVersions.Rows)
        objInfoIdItemList.Add(new ObjInfoIDItem(Convert.ToInt64(row["F_OBJECT_ID"]), Convert.ToInt32(row["F_OBJECT_TYPE"]), (long) Convert.ToInt32(row["F_ID"])));
    }
    return (IEnumerable<ObjInfoIDItem>) objInfoIdItemList;
  }
}
