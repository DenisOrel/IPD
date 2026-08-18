// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.CompositionItem
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Элемент состава</summary>
public sealed class CompositionItem : IComparable<CompositionItem>
{
  public CompositionItem(string id, Guid posGuid)
  {
    if (id == null)
      throw new ArgumentNullException(nameof (id));
    if (posGuid == Guid.Empty)
      throw new ArgumentNullException(nameof (posGuid));
    this.ID = id;
    this.PosGuid = posGuid;
    this.AdditionalAttributes = new List<Tuple<StringKey, object>>();
  }

  /// <summary>Внутренний идентификатор изделия</summary>
  public string ID { get; private set; }

  /// <summary>Глобальный идентификатор позиции на схеме</summary>
  public Guid PosGuid { get; private set; }

  /// <summary>Дополнительные атрибуты для связи</summary>
  public List<Tuple<StringKey, object>> AdditionalAttributes { get; set; }

  public int CompareTo(CompositionItem other) => !(this.ID == other.ID) ? -1 : 0;

  public static CompositionItem CreateSimple(string id, Guid posGuid, string posDesignation)
  {
    return new CompositionItem(id, posGuid)
    {
      AdditionalAttributes = {
        new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(new Guid("cad01478-306c-11d8-b4e9-00304f19f545")), (object) posDesignation),
        new Tuple<StringKey, object>((StringKey) IDCache.Default.Count.Text, (object) new MeasuredValue(1.0, IDCache.Default.ItemsMeasure.Id))
      }
    };
  }
}
