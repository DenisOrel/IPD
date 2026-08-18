// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FormDesignerControlToolBoxUpdateEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Класс для обновления списка ToolBoxItem.</summary>
public class FormDesignerControlToolBoxUpdateEventArgs
{
  private List<IMToolBoxItem> _items;

  /// <summary>Конструктор.</summary>
  /// <param name="originalList">Исходный список ToolBoxItems</param>
  public FormDesignerControlToolBoxUpdateEventArgs(List<IMToolBoxItem> originalList)
  {
    this._items = originalList;
  }

  /// <summary>Список ToolBoxItems.</summary>
  public List<IMToolBoxItem> Items => this._items;
}
