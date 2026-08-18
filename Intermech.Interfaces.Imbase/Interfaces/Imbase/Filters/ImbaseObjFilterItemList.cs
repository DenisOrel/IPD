// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Filters.ImbaseObjFilterItemList
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase.Filters;

/// <summary>Список элементов фильтра</summary>
[Serializable]
public class ImbaseObjFilterItemList : List<ImbaseObjFilterItem>
{
  /// <summary>Конструктор</summary>
  public ImbaseObjFilterItemList()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="collection"></param>
  public ImbaseObjFilterItemList(ImbaseObjFilterItemList collection)
    : base((IEnumerable<ImbaseObjFilterItem>) collection)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="capacity"></param>
  public ImbaseObjFilterItemList(int capacity)
    : base(capacity)
  {
  }
}
