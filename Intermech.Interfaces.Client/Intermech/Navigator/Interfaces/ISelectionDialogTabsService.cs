// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISelectionDialogTabsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Сервис дополнительных закладок для формы выборки</summary>
public interface ISelectionDialogTabsService
{
  /// <summary>
  /// Событие возникает на запрос закладок из формы выборки.
  /// Подписчики должны вернуть ISelectionDialogTab
  /// </summary>
  event SelectionDialogTabCreateHandler SelectionDialogTabEvent;

  /// <summary>
  /// Получить все дополнительные закладки для формы выборки
  /// </summary>
  ISelectionDialogTab[] Tabs { get; }
}
