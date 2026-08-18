// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Edit.AttributeStorage
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Edit;

internal class AttributeStorage
{
  /// <summary>Исходные значения атрибутов объекта / связи</summary>
  public IList<AttributeValues> Values { get; } = (IList<AttributeValues>) new List<AttributeValues>();

  /// <summary>Новые значения атрибутов объекта / связи</summary>
  public IList<AttributeValues> NewValues { get; } = (IList<AttributeValues>) new List<AttributeValues>();

  /// <summary>Только измененные значения атрибутов</summary>
  public IList<AttributeValues> DeltaValues { get; } = (IList<AttributeValues>) new List<AttributeValues>();

  /// <summary>Анализ изменений атрибутов</summary>
  /// <returns></returns>
  public bool ExtractDeltaValues()
  {
    this.DeltaValues.Clear();
    bool flag = !this.Values.Count.Equals(this.NewValues.Count);
    if (!flag)
    {
      for (int index = 0; index < this.Values.Count; ++index)
      {
        if (!this.Values[index].Equals(this.NewValues[index]))
        {
          flag = true;
          break;
        }
      }
    }
    if (!flag)
      return false;
    List<AttributeValues> resultData1;
    GenericListHelper.GetDifference<AttributeValues>(this.NewValues, this.Values, GenericListHelper.SearchMode.smNotExistInB, out resultData1, (IComparer<AttributeValues>) new AttributeValuesHelper.GuidValueComparer());
    List<AttributeValues> resultData2;
    GenericListHelper.GetDifference<AttributeValues>(this.NewValues, this.Values, GenericListHelper.SearchMode.smNotExistInA, out resultData2, (IComparer<AttributeValues>) new AttributeValuesHelper.GuidComparer());
    if ((resultData1 == null || resultData1.Count == 0) && (resultData2 == null || resultData2.Count == 0))
      return false;
    if (resultData1 != null)
      this.DeltaValues.AddRange<AttributeValues>((IEnumerable<AttributeValues>) resultData1);
    if (resultData2 != null && resultData2.Count > 0)
    {
      foreach (AttributeValues attributeValues in resultData2)
      {
        object[] objArray = new object[1]
        {
          (object) DeleteModesEnum.None
        };
        attributeValues.Values = objArray;
      }
      this.DeltaValues.AddRange<AttributeValues>((IEnumerable<AttributeValues>) resultData2);
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbAttributable"></param>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public static void LoadAttributes(
    [NotNull] IDBAttributable dbAttributable,
    [NotNull] IList<AttributeValues> attributes)
  {
    attributes.AddRange<AttributeValues>(((IEnumerable<AttributeValues>) dbAttributable.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption)).Where<AttributeValues>((Func<AttributeValues, bool>) (item => !item.ReadOnly)));
  }
}
