
// Type: Intermech.Navigator.Controls.ISelectedItemsHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Интерфейс элемента управления, который содержит коллекцию элементов навигации
/// </summary>
public interface ISelectedItemsHost
{
  /// <summary>Коллекция элементов навигации</summary>
  ISelectedItems SelectedItems { get; }

  event EventHandler SelectedItemsChanged;
}
