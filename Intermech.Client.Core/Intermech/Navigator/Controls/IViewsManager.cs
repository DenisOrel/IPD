
// Type: Intermech.Navigator.Controls.IViewsManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Controls;

/// <summary>Интерфес менеджера закладок навигатора.</summary>
public interface IViewsManager
{
  /// <summary>
  /// Обновляет коллекцию видимых закладок навигатора в соответствии с выбранными
  /// элементами навигации.
  /// </summary>
  /// <param name="items">Объект, описывающий выбранные элементы навигации</param>
  void UpdateViews(ISelectedItems items, bool throwExceptions = true);

  /// <summary>
  /// Заставляет активную закладку сохранить все сделанные изменения.
  /// </summary>
  void SaveChanges();

  /// <summary>Закрывает все закладки навигатора.</summary>
  void CloseViews();

  /// <summary>
  /// Возвращает коллекцию закладок навигатора, оторбражаемых менеджером.
  /// </summary>
  ViewPages ViewPages { get; }

  /// <summary>
  /// Возвращает или устанавливает активную закладку навигатора.
  /// </summary>
  IViewPage ActiveViewPage { get; set; }

  /// <summary>
  /// Срабатывает, если меняется активная закладка навигатора.
  /// </summary>
  event EventHandler ActiveViewPageChanged;

  /// <summary>Срабатывает после обновления закладок</summary>
  event EventHandler ViewsUpdated;
}
