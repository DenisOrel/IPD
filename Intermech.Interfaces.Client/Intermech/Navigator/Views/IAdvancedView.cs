// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.IAdvancedView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Xml;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Интерфейс закладки, которая умеет сохранять/восстанавливать текущую колллекцию выделенных элементов
/// </summary>
public interface IAdvancedView : IView
{
  /// <summary>
  /// Свойство позволяет получать/устанавливать выделенные элементы в закладке
  /// </summary>
  iFocusAndSelection FocusAndSelection { get; set; }

  /// <summary>
  /// Свойство позволяет получать/устанавливать выделенные элементы в закладке,
  /// включая выделенные элементы во вложенных закладках
  /// </summary>
  iFocusAndSelection FullFocusAndSelection { get; set; }

  /// <summary>
  /// Перечитать содержимое грида. Если задано состояние state, грид попробует восстановить выделенные строки
  /// </summary>
  /// <param name="state">Состояние или null, если требуется только перечитать грид</param>
  void Reload(iFocusAndSelection state);

  /// <summary>Запретить выделять первую строку в гриде</summary>
  bool DisableAutoselectFirstRow { get; set; }

  /// <summary>Запретить или разрешить отложенное обновление</summary>
  bool DisableDelayedUpdates { get; set; }

  /// <summary>Спрятать хинт, если он в данный момент отображается</summary>
  void HideHint();

  /// <summary>
  /// Получить текущее состояние закладки в виде XML-документа
  /// </summary>
  /// <returns>Текущее состояние закладки в виде XML-документа</returns>
  XmlDocument GetState();

  /// <summary>Восстановить состояние закладки из XML-документа</summary>
  /// <param name="xmlDoc">XML-документ с состоянием закладки</param>
  void RestoreState(XmlDocument xmlDoc);
}
