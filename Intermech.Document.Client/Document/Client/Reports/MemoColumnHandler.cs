// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Reports.MemoColumnHandler
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Text;

#nullable disable
namespace Intermech.Document.Client.Reports;

internal class MemoColumnHandler : IColumnHandler
{
  public object GetValue(ReportItemInfo itemInfo, object value)
  {
    if (Convert.ToString(value).Length < Consts.MaxStringSize)
      return value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute dbAttribute = (IDBAttribute) null;
      switch (itemInfo.AttributeSource)
      {
        case AttributeSourceTypes.Object:
          if (itemInfo.ObjectID == 0L)
            return value;
          IDBObject dbObject = sessionKeeper.Session.GetObject(itemInfo.ObjectID, false);
          if (dbObject != null)
          {
            dbAttribute = dbObject.GetAttributeByID(itemInfo.AttributeID);
            break;
          }
          break;
        case AttributeSourceTypes.Relation:
          if (itemInfo.PrjLinkID == 0L)
            return value;
          IDBRelation relation = sessionKeeper.Session.GetRelation(itemInfo.PrjLinkID, false);
          if (relation != null)
          {
            dbAttribute = relation.GetAttributeByID(itemInfo.AttributeID);
            break;
          }
          break;
      }
      if (dbAttribute != null)
      {
        if (dbAttribute is IMemoReader memoReader)
        {
          if (memoReader.OpenMemo(0) > 0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(memoReader.ReadDataBlock());
            return (object) stringBuilder.ToString();
          }
        }
      }
    }
    return value;
  }
}
