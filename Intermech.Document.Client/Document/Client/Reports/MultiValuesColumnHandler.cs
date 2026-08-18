// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Reports.MultiValuesColumnHandler
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Text;

#nullable disable
namespace Intermech.Document.Client.Reports;

internal class MultiValuesColumnHandler : IColumnHandler
{
  public object GetValue(ReportItemInfo itemInfo, object value)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributable dbAttributable = (IDBAttributable) null;
      switch (itemInfo.AttributeSource)
      {
        case AttributeSourceTypes.Object:
          if (itemInfo.ObjectID == 0L)
            return value;
          dbAttributable = (IDBAttributable) sessionKeeper.Session.GetObject(itemInfo.ObjectID, false);
          break;
        case AttributeSourceTypes.Relation:
          if (itemInfo.PrjLinkID == 0L)
            return value;
          dbAttributable = (IDBAttributable) sessionKeeper.Session.GetRelation(itemInfo.PrjLinkID, false);
          break;
      }
      if (dbAttributable != null)
      {
        IDBAttribute attributeById = dbAttributable.GetAttributeByID(itemInfo.AttributeID);
        if (attributeById != null && attributeById.ValuesCount > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 0; index < attributeById.ValuesCount; ++index)
          {
            if (index > 0)
              attributeById.Index = index;
            if (attributeById.ValuesCount > index + 1)
              stringBuilder.AppendLine(attributeById.AsString);
            else
              stringBuilder.Append(attributeById.AsString);
          }
          return (object) stringBuilder.ToString();
        }
      }
      return value;
    }
  }
}
