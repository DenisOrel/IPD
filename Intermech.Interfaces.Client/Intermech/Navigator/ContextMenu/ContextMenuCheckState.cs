// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.ContextMenuCheckState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>Состояние переключателя у элемента контекстного меню</summary>
public enum ContextMenuCheckState
{
  /// <summary>У элемента контекстного меню нет переключателя</summary>
  Default,
  /// <summary>
  /// Элемент отображается с переключателем состояния. Значение переключателя - "[ ]"
  /// </summary>
  Unchecked,
  /// <summary>
  /// Элемент отображается с переключателем состояния. Значение переключателя - "[x]"
  /// </summary>
  Checked,
}
