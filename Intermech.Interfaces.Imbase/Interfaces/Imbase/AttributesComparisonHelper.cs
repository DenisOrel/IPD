// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.AttributesComparisonHelper
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Набор методов для работы с сопоставлениями</summary>
public static class AttributesComparisonHelper
{
  /// <summary>Сформировать список сопоставлений для таблицы</summary>
  /// <param name="attribute">Атрибут таблицы "Сопоставление атрибутов"</param>
  /// <returns>Список сопоставлений. Если сопоставлений нет, то null.</returns>
  public static List<AttributesComparison> ReadFromAttribute(IDBAttribute attribute)
  {
    if (attribute == null || attribute.ValuesCount == 0)
      return (List<AttributesComparison>) null;
    List<AttributesComparison> attributesComparisonList = new List<AttributesComparison>(attribute.ValuesCount);
    for (int index = 0; index < attribute.ValuesCount; ++index)
    {
      attribute.Index = index;
      string asString = attribute.AsString;
      if (!string.IsNullOrEmpty(asString))
        attributesComparisonList.Add(new AttributesComparison(asString));
    }
    return attributesComparisonList.Count <= 0 ? (List<AttributesComparison>) null : attributesComparisonList;
  }

  public static void SaveToAttribute(IDBObject table, List<AttributesComparison> values)
  {
    IDBAttribute attributeByGuid = table.GetAttributeByGuid(PortalConsts.attributeComparisonAttributes, false);
    List<string> stringList = new List<string>();
    for (int index = 0; index < values.Count; ++index)
    {
      AttributesComparison attributesComparison = values[index];
      stringList.Add(attributesComparison.ToBase());
    }
    if (attributeByGuid == null)
      table.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeComparisonAttributes), false, (object[]) stringList.ToArray());
    else
      attributeByGuid.Values = (object[]) stringList.ToArray();
  }
}
