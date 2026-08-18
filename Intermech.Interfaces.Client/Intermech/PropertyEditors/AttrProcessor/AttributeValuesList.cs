// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeValuesList
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>список AttributeValues</summary>
public class AttributeValuesList : List<AttributeValues>, ICloneable
{
  public AttributeValuesList()
  {
  }

  public AttributeValuesList(IEnumerable<AttributeValues> collection)
    : base(collection)
  {
  }

  public AttributeValuesList(int capacity)
    : base(capacity)
  {
  }

  public AttributeValues FindByAttributeID(int attributeID)
  {
    int indexByAttributeId = this.FindIndexByAttributeID(attributeID);
    return indexByAttributeId != -1 ? this[indexByAttributeId] : (AttributeValues) null;
  }

  public int FindIndexByAttributeID(int attributeID)
  {
    int indexByAttributeId = -1;
    for (int index = 0; index < this.Count; ++index)
    {
      if (this[index].AttributeID == attributeID)
      {
        indexByAttributeId = index;
        break;
      }
    }
    return indexByAttributeId;
  }

  /// <summary>
  /// синхронизировать данный список с list:
  /// 		то чего не было, добавить; то, что было - заменить.
  /// значения из list не клонируются, а берутся ссылки
  /// </summary>
  /// <param name="list"></param>
  public void SyncronizeWith(AttributeValuesList list)
  {
    for (int index = 0; index < list.Count; ++index)
    {
      AttributeValues byAttributeId = this.FindByAttributeID(list[index].AttributeID);
      if (byAttributeId != null)
      {
        byAttributeId.Values = list[index].Values;
        byAttributeId.Descriptions = list[index].Descriptions;
      }
      else
        this.Add(list[index]);
    }
  }

  /// <summary>
  /// вернуть разницу списков.
  /// если атрибут изменен, то выводятся все значения атрибута (для многозначных).
  /// для удаленных в Values пишется: new object[] { DeleteModesEnum.None }.
  /// </summary>
  /// <param name="previousList"></param>
  /// <returns>null - изменения отсутствуют</returns>
  public AttributeValuesList ReturnDelta(AttributeValuesList previousList)
  {
    AttributeValuesList collection = (AttributeValuesList) this.Clone();
    AttributeValuesList attributeValuesList1 = (AttributeValuesList) previousList.Clone();
    AttributeValuesList attributeValuesList2 = new AttributeValuesList();
    for (int index = 0; index < attributeValuesList1.Count; ++index)
    {
      AttributeValues other = attributeValuesList1[index];
      AttributeValues byAttributeId = collection.FindByAttributeID(other.AttributeID);
      if (byAttributeId == null)
      {
        other.Values = new object[1]
        {
          (object) DeleteModesEnum.None
        };
        attributeValuesList2.Add(other);
      }
      else
      {
        if (!byAttributeId.Equals(other))
          attributeValuesList2.Add(byAttributeId);
        collection.Remove(byAttributeId);
      }
    }
    attributeValuesList2.AddRange((IEnumerable<AttributeValues>) collection);
    return attributeValuesList2.Count != 0 ? attributeValuesList2 : (AttributeValuesList) null;
  }

  public object Clone()
  {
    AttributeValuesList attributeValuesList = new AttributeValuesList(this.Count);
    for (int index = 0; index < this.Count; ++index)
      attributeValuesList.Add((AttributeValues) this[index].Clone());
    return (object) attributeValuesList;
  }
}
