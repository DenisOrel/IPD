// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Filters.ImbaseObjFilterData
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Imbase.Filters;

/// <summary>Фильтр справочников / каталогов Imbase для объектов</summary>
[Serializable]
public class ImbaseObjFilterData
{
  /// <summary>Данные фильтра</summary>
  private readonly ImbaseObjFilterItemList _items;

  /// <summary>Конструктор</summary>
  public ImbaseObjFilterData() => this._items = new ImbaseObjFilterItemList();

  /// <summary>Данные фильтра</summary>
  public ImbaseObjFilterItemList Items
  {
    [DebuggerStepThrough] get => this._items;
  }
}
